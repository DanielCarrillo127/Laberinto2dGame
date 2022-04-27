using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalRecord : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] private Text FinalTime;
    [SerializeField] private Text FinalLayer;
    
    void Start()
    {
        FinalLayer.text = "Llegaste hasta la " + BoardManager.level;
        // FinalTime.text = "tu tiempo fue:  " + TimerController.timePlayingStr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
