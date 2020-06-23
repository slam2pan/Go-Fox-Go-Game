using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ConstantRotation : MonoBehaviour
{
    private float rpm = 100;
    

    // Rotate continuously
    void Update()
    {
        transform.Rotate(Vector3.up * rpm * Time.deltaTime);
    }
}
