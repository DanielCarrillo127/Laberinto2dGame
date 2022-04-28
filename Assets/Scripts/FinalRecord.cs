using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalRecord : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text FinalTime;
    [SerializeField] private Text FinalLayer;
    [SerializeField] private Text Header;
    
    void Start()
    {
        FinalLayer.text = "you reached the layer " + BoardManager.Instance.GetLevel().ToString() + " of the cave" ;
        string timestr = BoardManager.Instance.GetTime();
        string ftime = timestr.Substring(6, 8);
        FinalTime.text = "Your final time was:  " + ftime;
        if(BoardManager.Instance.GetLevel() == 4){
            Header.text = "Congratulations!!, you escape the cave and passed through the enemies";
        }else{
            Header.text = "Sorry you lost, keep trying!"; 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
