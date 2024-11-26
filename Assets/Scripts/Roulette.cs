using System;
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
    [SerializeField]
    private Jackpot jp;
    [SerializeField]
    private DataManager dm;
    [SerializeField]
    private Material[] materials;

    private float time;
    private MeshRenderer[] renderers;

    void Start()
    {
        renderers = new MeshRenderer[4];
        for(int i=0; i<4; i++){
            renderers[i] = transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0){
            transform.Rotate(0, Mathf.Min(time * time * speed, speed * 20) * Time.deltaTime, 0);
            time -= Time.deltaTime;
            if(time < 0){
                int index = (int)transform.rotation.eulerAngles.y / 90;
                int result = int.Parse(renderers[index].transform.parent.name);
                dm.data.statistics.roulette[result]++;
                switch((RoulettePrizes)result){
                    case RoulettePrizes.Jackpot:
                        jp.StartJP();
                        InitRoulette();
                        break;
                    case RoulettePrizes.GoldTetrapod:
                        SetArea(index);
                        lm.ResumeLottery();
                        tm.GiveTetrapod(prefab:tm.tetrapodPrefabs.gold);
                        break;
                    case RoulettePrizes.Lotteries:
                        SetArea(index);
                        lm.ResumeLottery();
                        lm.QueueLottery(20);
                        break;
                    case RoulettePrizes.DoubleShot:
                        SetArea(index);
                        lm.ResumeLottery();
                        gm.doubleShotRemaining = 20;
                        break;
                }
            }
        }
    }

    public void Spin(){
        time = (float)new System.Random().NextDouble() + 5;
    }

    private void SetArea(int area, int to=0){
        Material[] t = renderers[area].materials;
        t[0] = materials[to];
        renderers[area].materials = t;
        renderers[area].transform.parent.name = to.ToString();
    }

    private void InitRoulette(){
        for(int i=0; i<4; i++){
            SetArea(i, i);
        }
    }
}
