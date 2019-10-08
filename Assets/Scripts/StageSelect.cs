using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    void Start()
    {
        GameManager.m_instanceGM.dungeonStage = this.gameObject;
        this.gameObject.SetActive(false);
    }
    public void Stage(int map)
    {
        switch (map)
        {
            case 1:
                SceneManager.LoadScene("Dungeon");
                break;
            case 2:
                break;
        }
        //GameManager.m_instanceGM.Change();
    }
}
