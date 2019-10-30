using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instanceGM { get; private set; } = null;
    public HitManager hitManager;
    public FloatingManager floatingManager;
    public DataManager dataManager;
    public UIManager uiManager;
    public PlayerControl playerControl;
    public GameObject playerCamera;
    public GameObject dungeonStage;     
    public bool playerDie = false;
    public bool playerSpecialSkill = false;
    
    void Awake()
    {
        if (m_instanceGM == null)
        {
            m_instanceGM = this;
            DontDestroyOnLoad(this.gameObject);
        }
 
    }

    void Update()
    {
        if(playerControl != null)
        {
            if (playerDie && playerControl.myPlayerAnimationState.playerNowState != PlayerAnimationState.CharacterState.Die)
            {
                Debug.Log(playerControl.myPlayerAnimationState.playerNowState + " NowState");
                Debug.Log(PlayerAnimationState.CharacterState.Die + " State.Die");
                playerControl.myPlayerAnimationState.AnimationChange(PlayerAnimationState.CharacterState.Die);
            }
        }
    }
}
