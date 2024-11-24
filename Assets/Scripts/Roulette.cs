using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Spin();
        }
        if(time > 0){
            transform.Rotate(0, time * time * speed * Time.deltaTime, 0);
            time -= Time.deltaTime;
            if(time < 0){
                Debug.Log((int)transform.rotation.eulerAngles.y / 90);  
            }
        }
    }

    public void Spin(){
        time = (float)new System.Random().NextDouble() + 9;
    }
}
