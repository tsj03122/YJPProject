using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    GameManager playerDie;
    public GameObject deadscrean;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerDie = FindObjectOfType<GameManager>();
        if (playerDie.playerDie == true)
        {
            deadscrean.SetActive(true);
        }
    }

    public void dead()
    {
        SceneManager.LoadScene("Main");
    }
}
