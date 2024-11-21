using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private float launchSpeed;
    [SerializeField]
    private float launchAngle;
    [SerializeField]
    private GameManager gm;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            float angle = (Mathf.Clamp(Input.mousePosition.x, 640, 1280) - 960) / 16; // / 320 * 20
            GameObject tetrapod = gm.tetrapodPool.Get();
            tetrapod.transform.position = transform.position;
            tetrapod.GetComponent<Rigidbody>().velocity = Quaternion.Euler(new Vector3(-launchAngle, angle, 0)) * Vector3.forward * launchSpeed;
        }
    }
}
