using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraSimpleMovement : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float rotateSpeed = 40;

    void Update()
    {
        float Z = Input.GetAxis("Vertical");
        if(Z != 0)
        {
            transform.position += transform.forward * Z * speed * Time.deltaTime;
        }

        float X = Input.GetAxis("Horizontal");
        transform.Rotate(transform.up, rotateSpeed * X * Time.deltaTime);

    }
}
