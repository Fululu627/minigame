using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLight : MonoBehaviour {

    private float rotateSpeed = 10.0f;

    void Update()
    {
        transform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed);
    }
}
