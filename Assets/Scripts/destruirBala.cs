using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruirBala : MonoBehaviour
{
    float tiempo = 0;
    float vida = 0.78f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempo > vida)
        {
            Destroy(this);
        }
        tiempo += Time.deltaTime;
       
    }
}
