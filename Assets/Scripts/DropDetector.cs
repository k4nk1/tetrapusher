using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDetector : MonoBehaviour
{
    [SerializeField ]
    private GameManager gm;

    void OnCollisionEnter(Collision col){
        gm.tetrapodPool.Release(col.gameObject);
    }
}
