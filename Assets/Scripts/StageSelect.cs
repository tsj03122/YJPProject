using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public void Stage(int map)
    {
        switch (map)
        {   
            case 1:
                GameManager.m_instanceGM.playerControl.gameObject.transform.position = new Vector3(0,0,0);
                SceneManager.LoadScene("Dungeon");
                GameManager.m_instanceGM.playerControl.dungeonStart = true;
                break;
            case 2:
                break;
        }
    }
}
