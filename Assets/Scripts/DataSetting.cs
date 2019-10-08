using System.Collections;
using System.Data;
using UnityEngine;
using System;
using System.IO;
using System.Data.SqlTypes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Xml;
using System.Web;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataSetting : MonoBehaviour
{
    public static GameManager m_instanceGM { get; private set; } = null;
    public static DataSetting m_instraintDS { get; private set; } = null;
    static MySqlConnection sqlconn = null;
    private string sqlDBip = "testdbminiproject.cnqdptoslmij.ap-northeast-2.rds.amazonaws.com";
    private string sqlDBname = "testdbminiproject";
    private string sqlDBid = "admin";
    private string sqlDBpw = "admin123";
    private string userIP = "";
    public Text idText;
    public InputField pwText;
    public string id;
    public string pw;

    void Awake()
    {
        if (m_instraintDS == null)
        {
            m_instraintDS = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        //IPv4 172.30.1.52
        userIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork).ToString();
        Debug.Log(userIP);
    }

    public string Getid()
    {
        return id;
    }

    private void sqlConnect()
    {
        //DB정보 입력
        string sqlDatabase = "Server=" + sqlDBip + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw;

        //접속 확인하기
        try
        {
            sqlconn = new MySqlConnection(sqlDatabase);
            sqlconn.Open();
            Debug.Log("SQL의 접속 상태 : " + sqlconn.State); //접속이 되면 OPEN이라고 나타남
        }
        catch (Exception msg)
        {
            Debug.Log(msg); //기타다른오류가 나타나면 오류에 대한 내용이 나타남
        }
    }

    //업데이트와 추가할 때 사용.
    public void sqlcmdall(string allcmd) //함수를 불러올때 명령어에 대한 String을 인자로 받아옴
    {
        MySqlCommand dbcmd = new MySqlCommand(allcmd, sqlconn); //명령어를 커맨드에 입력
        dbcmd.ExecuteNonQuery(); //명령어를 SQL에 보냄
    }

    //선택해서 리턴받을 때
    public DataTable selsql(string sqlcmd)  //리턴 형식을 DataTable로 선언함
    {
        DataTable dt = new DataTable(); //데이터 테이블을 선언함

        MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcmd, sqlconn);
        adapter.Fill(dt); //데이터 테이블에  채워넣기를함
        
        return dt; //데이터 테이블을 리턴함
    }
        
    private void sqldisConnect()
    {
        sqlconn.Close();
        Debug.Log("SQL의 접속 상태 : " + sqlconn.State);
    }

    public void MainStart()
    {
        //화면 fade Out
        SceneManager.LoadScene("Main");
        DontDestroyOnLoad(this);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NewMember()
    {
        sqlConnect();
        id = idText.text.ToString();
        pw = pwText.text;
        sqlcmdall("insert into testdbminiproject.Users values('" + id + "','" + pw + "');");
        sqldisConnect();
    }

    public void Login()
    {
        sqlConnect();
        if (selsql("Select idUsers from testdbminiproject.Users where idUsers ='" + idText.text.ToString() + "';").Rows.Count == 0)
        {
            Debug.Log("접속 실패 아이디가 없습니다.");
            return;
        }
        id = selsql("Select * from testdbminiproject.Users where idUsers ='" + idText.text.ToString() + "';").Rows[0].ItemArray[0].ToString();
        pw = selsql("Select * from testdbminiproject.Users where idUsers ='" + idText.text.ToString() + "';").Rows[0].ItemArray[1].ToString();

        if(idText.text.ToString().Equals(id) && pwText.text.ToString().Equals(pw))
        {
            MainStart();
        }
        sqldisConnect();
    }
}


