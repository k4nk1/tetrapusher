using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class DropDetector : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private TetrapodManager tm;
    [SerializeField]
    private Jackpot jp;


    void OnCollisionEnter(Collision col){
        String tag = col.gameObject.tag;
        if(tag == tm.tetrapodPrefabs.normal.tag){
            gm.Credit++;
            tm.tetrapodPool.Release(col.gameObject);
        }else if(tag == tm.tetrapodPrefabs.big.tag){
            gm.Credit += 20;
            Destroy(col.gameObject);
        }else if(tag == tm.tetrapodPrefabs.gold.tag){
            gm.Credit += 50;
            Destroy(col.gameObject);
        }else if(tag == tm.tetrapodPrefabs.jackpot.tag){
            jp.hasFallen = true;
        }
    }
}
