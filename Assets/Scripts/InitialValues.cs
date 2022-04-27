using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialValues : MonoBehaviour
{
    [SerializeField]
    private Slider sliderN;
    [SerializeField]
    private Slider sliderM;
    [SerializeField]
    private Text TextfieldN;
    [SerializeField]
    private Text TextfieldM;

    public static int Nvalue = 10 ; 
    public static int Mvalue = 5;

    // private void Awake()
    // {
    //     slider = gameObject.GetComponent<Slider>();
    // }


    // Start is called before the first frame update
    void Start()
    {
        sliderN.onValueChanged.AddListener((v) =>{
            Nvalue = (int) v;
            TextfieldN.text = v.ToString(); 
        });

        sliderM.onValueChanged.AddListener((v2) =>{
            Mvalue = (int) v2;
            TextfieldM.text = v2.ToString(); 
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
}
