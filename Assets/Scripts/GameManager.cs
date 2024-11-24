using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Launcher launcher;
    [SerializeField]
    private TMP_Text creditText;
    [SerializeField]
    private TMP_Text creditTextS;

    [SerializeField]
    private int credit;
    public int Credit{
        get { return credit; }
        set { 
            credit = value;
            UpdateCreditText();
        }
    }
    private bool doubling;
    void Start()
    {
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(credit > 0){
                launcher.Launch();
                Credit -= 2;
            }
        }
    }

    private void UpdateCreditText(){
        creditText.text = Credit.ToString();
        creditTextS.text = creditText.text;
    }
}
