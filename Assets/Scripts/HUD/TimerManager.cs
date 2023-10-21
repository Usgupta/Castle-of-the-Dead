using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    public IntVariable gameTimer;

    public TextMeshProUGUI timeText;

    public UnityEvent gameOver;

    public GameConstants gameConstants;
    // Start is called before the first frame update
    void Start()
    {
        gameTimer.Value = gameConstants.maxTimer;
        StartCoroutine(Timer());
    }
    
    public void GameStart()
    {
        Start();
    }

    IEnumerator Timer()
    {
        while (gameTimer.Value > 0 && Time.timeScale !=0.0f)
        {
            gameTimer.Value -= 1;
            timeText.text = "Time: " + gameTimer.Value.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        gameOver.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
