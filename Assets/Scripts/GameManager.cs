using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tetrapod;

    public ObjectPool<GameObject> tetrapodPool;
    void Start()
    {
        Application.targetFrameRate = 30;
        tetrapodPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(tetrapod),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false)
        );
    }

    void Update()
    {
        
    }
}
