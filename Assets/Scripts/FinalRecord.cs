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
        int levels = BoardManager.Instance.GetLevel();
        string timestr = BoardManager.Instance.GetTime();
        string ftime = timestr.Substring(6, 8);
        FinalTime.text = "Your final time was:  " + ftime;
        if(levels == 5){
            Header.text = "Congratulations!!, you escape the cave and passed through the enemies";
            levels = levels-1;
            FinalLayer.text = "you reached the layer " +  levels.ToString()+ " of the cave" ;
        }else if(levels < 4)
        {  
            FinalLayer.text = "you reached the layer " +  levels.ToString()+ " of the cave" ;
            Header.text = "Sorry you lost, keep trying!"; 
        }else{
            Header.text = "Sorry you lost, keep trying!"; 
            FinalLayer.text = "you reached the last layer of the cave, but you die sorry" ;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
