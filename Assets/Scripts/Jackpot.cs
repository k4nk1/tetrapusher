using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jackpot : MonoBehaviour
{
    [SerializeField]
    private TetrapodManager tm;
    [SerializeField]
    private LotteryMachine lm;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private DataManager dm;

    private int phase; //0:Not in JP, 1:Camera goes to JP, 2:Playing, 3:Camera goes back to normal
    private float time;
    public bool hasFallen;
    private List<GameObject> tetrapods;

    void Start(){
        tetrapods = new List<GameObject>();
    }

    void Update(){
        time += Time.deltaTime;
        switch(phase){
            case 0: break;
            case 1: 
                Camera.main.transform.position += Vector3.back * Time.deltaTime * 2;
                if(time > 4){
                    phase = 2;
                    time = 0;
                    hasFallen = false;
                }
                break;
            case 2:
                if(time > 5){
                    if(hasFallen){
                        time = 0;
                        phase = 3;
                        break;
                    }
                    Fall();
                    tm.GiveTetrapod(tm.tetrapodPrefabs.gold);
                    time = 0;
                }
                break;
            case 3:
                Camera.main.transform.position += Vector3.forward * Time.deltaTime * 2;
                if(time > 4){
                    phase = 0;
                    lm.ResumeLottery();
                    dm.data.statistics.jackpotResults[dm.data.statistics.jackpots++] = tetrapods.Count;
                    if(dm.data.statistics.jackpotMax < tetrapods.Count) dm.data.statistics.jackpotMax = tetrapods.Count;
                    foreach(GameObject tetrapod in tetrapods){
                        Destroy(tetrapod);
                    }
                    gm.pausingShot = false;
                }
                break;
        }
    }

    public void StartJP(){
        phase = 1;
        time = 0;
        tetrapods.Clear();
        gm.pausingShot = true;
    }

    public void Fall(){
        System.Random random = new System.Random();
        Vector3 pos = new Vector3((float)(random.NextDouble() * 2 - 1), 6, (float)(random.NextDouble() * 2 - 7));
        tetrapods.Add(Instantiate(tm.tetrapodPrefabs.jackpot, pos, Quaternion.identity));
    }
}
