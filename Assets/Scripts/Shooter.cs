using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private float launchSpeed;
    [SerializeField]
    private float launchAngle;
    [SerializeField]
    private TetrapodManager tm;
    
    public void Shoot(bool doubleShot = false)
    {
        float angle = (Mathf.Clamp(Input.mousePosition.x, 740, 1180) - 960) / 16; // / 320 * 20
        GameObject tetrapod = tm.tetrapodPool.Get();
        tetrapod.transform.position = transform.position;
        tetrapod.GetComponent<Rigidbody>().velocity = Quaternion.Euler(new Vector3(-launchAngle, angle, 0)) * Vector3.forward * launchSpeed;
        if(doubleShot){
            tetrapod = tm.tetrapodPool.Get();
            tetrapod.transform.position = transform.position + Vector3.up;
            tetrapod.GetComponent<Rigidbody>().velocity = Quaternion.Euler(new Vector3(-launchAngle, angle, 0)) * Vector3.forward * launchSpeed;
        }
    }
}
