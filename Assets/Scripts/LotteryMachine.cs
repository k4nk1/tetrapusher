using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryMachine : MonoBehaviour
{
    [SerializeField]
    private float delay;
    [SerializeField]
    private GameObject diceTetrapodPF;

    private GameObject[] dice;
    private Rigidbody[] diceRb;
    private int lotteries;
    private float timer;

    void Start()
    {
        dice = new GameObject[3];
        diceRb = new Rigidbody[3];
        for (int i = 0; i < 3; i++){
            dice[i] = Instantiate(diceTetrapodPF, new Vector3(3.5f, 2.4f, 1 + i), Quaternion.identity);
            diceRb[i] = dice[i].GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) QueueLottery(1);
        if(lotteries > 0 && !AreDiceMoving()){
            timer -= Time.deltaTime;
            if(timer < 0){
                int wins = 0;
                foreach(GameObject die in dice){
                    if((die.transform.rotation * Vector3.up - Vector3.up).sqrMagnitude < 0.5f) wins++;
                }
                Debug.Log(wins);
                if(lotteries-- > 1) StartLottery();
            }
        }
    }

    void StartLottery(){
        System.Random random = new System.Random();
        for (int i = 0; i < 3; i++){
            dice[i].SetActive(true);
            dice[i].transform.position = new Vector3(3.5f, 5f, 1 + i);
            diceRb[i].velocity = new Vector3((float)random.NextDouble()*2-1, (float)random.NextDouble()*2-1, (float)random.NextDouble()*2-1);
            diceRb[i].angularVelocity = new Vector3((float)random.NextDouble()*4-2, (float)random.NextDouble()*4-2, (float)random.NextDouble()*4-2);
        }
        timer = delay;
    }

    public void QueueLottery(int n){
        if(lotteries == 0) StartLottery();
        lotteries += n;
    }

    private bool AreDiceMoving(){
        foreach(Rigidbody dieRb in diceRb){
            if(dieRb.velocity.sqrMagnitude > 0.1f) return true;
        }
        return false;
    }
}
