using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class TetrapodPrefabs{
    public GameObject normal;
    public GameObject big;
    public GameObject gold;
    public GameObject die;
    public GameObject jackpot;
}

public class TetrapodManager : MonoBehaviour
{
    public TetrapodPrefabs tetrapodPrefabs;
    public ObjectPool<GameObject> tetrapodPool;
    void Start()
    {
        tetrapodPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(tetrapodPrefabs.normal),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false)
        );
    }

    public void GiveTetrapod(GameObject prefab = null, int amount = 1){
        if(prefab == null){
            for(int i=0;i<amount;i++){
                GameObject tetrapod = tetrapodPool.Get();
                tetrapod.transform.position = new Vector3(0, 5+i, 4);
                tetrapod.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }else{
            for (int i=0; i<amount; i++){
                GameObject tetrapod = Instantiate(prefab, new Vector3(0, 5+i, 4), Quaternion.identity);
                tetrapod.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        
    }


}
