using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatLength = 50;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < startPos.z - repeatLength)
        {
            transform.position = startPos;
        }
    }
}
