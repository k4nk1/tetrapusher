using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LotteryMachine : MonoBehaviour
{
    [SerializeField]
    private float delay;

    [SerializeField]
    private Roulette r;
    [SerializeField]
    private TetrapodManager tm;
    [SerializeField]
    private DataManager dm;
    [SerializeField]
    private TMP_Text lotteriesText;
    [SerializeField]
    private TMP_Text lotteriesTextS;

    private GameObject[] dice;
    private Rigidbody[] diceRb;
    private int lotteries;
    private float timer;
    private bool pausing;

    void Start()
    {
        dice = new GameObject[3];
        diceRb = new Rigidbody[3];
        for (int i = 0; i < 3; i++){
            dice[i] = Instantiate(tm.tetrapodPrefabs.die, new Vector3(3.5f, 2.4f, 1 + i), Quaternion.identity);
            diceRb[i] = dice[i].GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if(timer > 0 && !AreDiceMoving()){
            timer -= Time.deltaTime;
            if(timer < 0){
                int wins = 0;
                foreach(GameObject die in dice){
                    if((die.transform.rotation * Vector3.up - Vector3.up).sqrMagnitude < 0.5f) wins++;
                }
                switch(wins){
                    case 0: break;
                    case 1: 
                        tm.GiveTetrapod(amount: 5); break;
                    case 2:
                        tm.GiveTetrapod(prefab: tm.tetrapodPrefabs.big);
                        break;
                    case 3:
                        r.Spin();
                        pausing = true;
                        break;
                }
                dm.data.statistics.lottery[wins]++;
                if(--lotteries > 0 && !pausing){
                    StartLottery();
                    SetLotteriesText(lotteries-1);
                }
            }
        }
    }

    void StartLottery(){
        timer = delay;
        System.Random random = new System.Random();
        for (int i = 0; i < 3; i++){
            dice[i].SetActive(true);
            dice[i].transform.position = new Vector3(3.5f, 5f, 1 + i);
            diceRb[i].velocity = new Vector3((float)random.NextDouble()*2-1, (float)random.NextDouble()*2-1, (float)random.NextDouble()*2-1);
            diceRb[i].angularVelocity = new Vector3((float)random.NextDouble()*4-2, (float)random.NextDouble()*4-2, (float)random.NextDouble()*4-2);
        }
    }

    public void QueueLottery(int n){
        lotteries += n;
        if(pausing){
            SetLotteriesText(lotteries);
        }else{
            if(lotteries - n == 0) StartLottery();
            SetLotteriesText(lotteries-1);
        }
    }

    private bool AreDiceMoving(){
        foreach(Rigidbody dieRb in diceRb){
            if(dieRb.velocity.sqrMagnitude > 0.1f) return true;
        }
        return false;
    }

    private void SetLotteriesText(int n){
        lotteriesText.text = n.ToString();
        lotteriesTextS.text = n.ToString();
    }

    public void ResumeLottery(){
        pausing = false;
        if(lotteries > 0){
            StartLottery();
            SetLotteriesText(lotteries-1);
        }
    }
}
