using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    [SerializeField]
    private int speed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, speed, 0));
    }
}
