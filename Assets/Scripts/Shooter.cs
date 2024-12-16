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
        float x = Input.touchCount > 0 ? Input.GetTouch(0).position.x : Input.mousePosition.x;
        float angle = (Mathf.Clamp(x, Screen.width/5*2, Screen.width/5*3) - Screen.width/2) * 80 / Screen.width; // / 320 * 20
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
