using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    void Start()
    {
        GameManager gm = GameManager.m_instanceGM;
        CameraManager cameraMg = gm.playerCamera.GetComponent<CameraManager>();
        MapColiider map = GameObject.Find("Map").gameObject.GetComponent<MapColiider>();
        cameraMg.bound = map.mapColiider;
        if(map.DungeunStageCanvas != null){gm.dungeonStage = map.DungeunStageCanvas;}
        cameraMg.CameraSetting();
        gm.playerControl.transform.position = new Vector3(0, 0, 0);
        gm.playerControl.gameObject.SetActive(true);
        GameManager.m_instanceGM.dataManager.PlayerEquipmentSetting();
        GameManager.m_instanceGM.playerControl.playerStats.ChangeStats();
        GameManager.m_instanceGM.uiManager.HpSpSetting();
    }
}
