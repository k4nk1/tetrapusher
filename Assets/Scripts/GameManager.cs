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
    private GameObject helpButton;

    [SerializeField]
    private int credit;
    public int Credit{
        get { return credit; }
        set {
            if(credit > 1 && value < 2) helpButton.SetActive(true);
            else if(credit < 2 && value > 1) helpButton.SetActive(false);
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
    }

    private void UpdateCreditText(){
        creditText.text = Credit.ToString();
        creditTextS.text = creditText.text;
    }

    public void Help(){
        Credit = 100;
    }
}
