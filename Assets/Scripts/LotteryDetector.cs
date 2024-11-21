using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryDetector : MonoBehaviour
{
    [SerializeField]
    private float frequency;
    [SerializeField]
    private LotteryMachine lm;

    void FixedUpdate()
    {
        transform.position = new Vector3(1.7f * Mathf.Sin(6.28f * frequency * Time.time), -0.3f, -0.2f);
    }

    void OnCollisionEnter(Collision col){
        lm.QueueLottery(1);
    }
}
