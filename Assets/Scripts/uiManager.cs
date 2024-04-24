using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public Text Enemytext;

    public static int enemyLeft = 11; 

    public static int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        health = 100; 
        enemyLeft = 11; 
    }


    public void UpdateEnemyCount()
    {
        Enemytext.text = "Enemies Left:  " + enemyLeft;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateEnemyCount();


        if (enemyLeft == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}