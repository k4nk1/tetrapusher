using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoulettePrizes{
    Jackpot,
    GoldTetrapod,
    Lotteries,
    DoubleShot
}

public class Roulette : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private TetrapodManager tm;
    [SerializeField]
    private LotteryMachine lm;

    private float time;
    private RoulettePrizes[] results;
    void Start()
    {
        results = new RoulettePrizes[4]{RoulettePrizes.Jackpot, RoulettePrizes.GoldTetrapod, RoulettePrizes.Lotteries, RoulettePrizes.DoubleShot};
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Spin();
        }
        if(time > 0){
            transform.Rotate(0, time * time * speed * Time.deltaTime, 0);
            time -= Time.deltaTime;
            if(time < 0){
                int index = (int)transform.rotation.eulerAngles.y / 90;
                RoulettePrizes result = results[index];
                switch(result){
                    case RoulettePrizes.Jackpot:
                        //Jackpot
                        results = new RoulettePrizes[4]{RoulettePrizes.Jackpot, RoulettePrizes.GoldTetrapod, RoulettePrizes.Lotteries, RoulettePrizes.DoubleShot};
                        break;
                    case RoulettePrizes.GoldTetrapod:
                        tm.GiveTetrapod(prefab:tm.tetrapodPrefabs.gold);
                        break;
                    case RoulettePrizes.Lotteries:
                        lm.QueueLottery(10);
                        break;
                    case RoulettePrizes.DoubleShot:
                        gm.doubleShotRemaining = 10;
                        break;
                }
            }
        }
    }

    public void Spin(){
        time = (float)new System.Random().NextDouble() + 9;
    }
}
