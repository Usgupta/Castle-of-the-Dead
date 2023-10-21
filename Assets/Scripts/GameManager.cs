using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // events
    public UnityEvent gameStart;
    public UnityEvent<int> scoreChange;
    public UnityEvent<int> manaChange;
    // scriptable object variables
    public IntVariable gameScore;
    public IntVariable gameMana;

    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseScore(int increment)
    {   

        gameScore.ApplyChange(1);
        SetScore(gameScore.Value);
    }

    public void SetScore(int score)
    {   
        scoreChange.Invoke(gameScore.Value);
    }

    public void IncreaseMana(int increment)
    {
        
        gameMana.ApplyChange(increment);
        SetMana();
    }
    
    public void SetMana()
    {   
        manaChange.Invoke(gameMana.Value);
    }
    
}