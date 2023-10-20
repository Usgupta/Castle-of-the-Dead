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
    // Start is called before the first frame update
    void Start()
    {
        gameTimer.Value = 30;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (gameTimer.Value > 0)
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
