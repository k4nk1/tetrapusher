using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropDetector : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private TetrapodManager tm;


    void OnCollisionEnter(Collision col){
        String tag = col.gameObject.tag;
        if(tag == tm.tetrapodPrefabs.normal.tag){
            gm.Credit++;
            tm.tetrapodPool.Release(col.gameObject);
        }else if(tag == tm.tetrapodPrefabs.big.tag){
            gm.Credit += 20;
            Destroy(col.gameObject);
        }
    }
}
