using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float posY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = new Vector3(0f, 0f, -30f) + new Vector3(target.position.x, posY, 0f);


    }
}
