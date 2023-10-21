using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class HUDManager : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject gameOverPanel;
    public IntVariable gameScore;
    public IntVariable gameMana;
    public GameObject manaText;
    
    // Start is called before the first frame update
    
    void Start()
    {
        gameScore.Value = 0;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GameStart()
    {
        Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SetScore(int score)
    {   
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + gameScore.Value.ToString();
    }

    public void SetMana(int mana)
    {
        manaText.GetComponent<TextMeshProUGUI>().text = "Mana: " + gameMana.Value.ToString();
    }
    
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
