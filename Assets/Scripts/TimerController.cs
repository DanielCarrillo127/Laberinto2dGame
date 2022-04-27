using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Ref: https://www.youtube.com/watch?v=qc7J0iei3BU

public class TimerController : MonoBehaviour
{ 
    public static TimerController Instance;
    public Text Timertext;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;
    public static string timePlayingStr;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        Timertext.text = "Time: 00:00.00";
        timerGoing = false;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while(timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            Timertext.text = timePlayingStr;

            yield return null;  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
