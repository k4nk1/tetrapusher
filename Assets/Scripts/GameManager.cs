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
    public float defaultShootCoolTime;
    private float shootCoolTime;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        shootCoolTime -= Time.deltaTime;
        if(doubleShotRemaining > 0) doubleShotRemaining -= Time.deltaTime;
        if((Input.GetMouseButton(0) || Input.touchCount > 0) && shootCoolTime < 0 && credit > 1 && !pausingShot){
            shooter.Shoot(doubleShotRemaining > 0);
            Credit -= 2;
            dm.data.statistics.shots++;
            shootCoolTime = defaultShootCoolTime;
        }
        if((Input.GetKeyDown(KeyCode.H) || Input.touchCount > 0 && Input.GetTouch(0).position.y <= 50) && Credit < 2) Credit += 100;
    }

    private void UpdateCreditText(){
        creditText.text = Credit.ToString();
        creditTextS.text = creditText.text;
    }
}
