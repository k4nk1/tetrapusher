using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Shooter shooter;
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
    public float doubleShotRemaining;
    void Start()
    {
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        if(doubleShotRemaining > 0) doubleShotRemaining -= Time.deltaTime; 
        if(Input.GetMouseButtonDown(0)){
            if(credit > 0){
                shooter.Shoot(doubleShotRemaining > 0);
                Credit -= 2;
            }
        }
    }

    private void UpdateCreditText(){
        creditText.text = Credit.ToString();
        creditTextS.text = creditText.text;
    }
}
