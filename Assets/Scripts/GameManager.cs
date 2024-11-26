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
    private DataManager dm;
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
            dm.data.credits = value;
            UpdateCreditText();
        }
    }
    public float doubleShotRemaining;
    public bool pausingShot;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if(doubleShotRemaining > 0) doubleShotRemaining -= Time.deltaTime;
        if(Input.GetMouseButtonDown(0)){
            if(credit > 1 && !pausingShot){
                shooter.Shoot(doubleShotRemaining > 0);
                Credit -= 2;
                dm.data.statistics.shots++;
            }
        }
        if(Input.GetKeyDown(KeyCode.H) && Credit < 2) Credit += 100;
    }

    private void UpdateCreditText(){
        creditText.text = Credit.ToString();
        creditTextS.text = creditText.text;
    }
}
