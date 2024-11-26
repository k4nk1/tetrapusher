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
    private int width;

    void Start(){
        width = Screen.width;
    }

    public void Shoot(bool doubleShot = false)
    {
        float angle = (Mathf.Clamp(Input.mousePosition.x, width/3, width/3*2) - width/2) * 60 / width; // / 320 * 20
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
