using FuzzyLogicSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agenteNoDifuso : MonoBehaviour
{
    public GameObject bala;

    public Transform target = null;

    public Transform source = null;

    public TextAsset fuzzyLogicData = null;

    private FuzzyLogic fuzzyLogic = null;

    private int municion = 40;


    private float intervalo = 0.5f;
    private float tiempo = 0f;

    private float intervaloRecarga = 3.0f;
    private float tiempoRecarga = 0.0f;


    bool usandoCanion = true;


    private void Start()
    {
        //el movimiento sigue usando Fuzzy Logic
        fuzzyLogic = FuzzyLogic.Deserialize(fuzzyLogicData.bytes, null);

    }

    private void Update()
    {
        fuzzyLogic.evaluate = true;
        float distancia = Vector3.Distance(target.position, source.position);
        fuzzyLogic.GetFuzzificationByName("distance").value = distancia;

        float speed = fuzzyLogic.Output() * fuzzyLogic.defuzzification.maxValue;

        source.position = Vector3.MoveTowards(source.position, target.position, speed * Time.deltaTime);


        Vector3 dirTarget = Vector3.Normalize(target.position - source.position);
        GameObject bal = null;

        if (tiempo > intervalo)
        {

            bal = Instantiate(bala, transform.position + dirTarget, Quaternion.identity);
            bal.GetComponent<Rigidbody>().velocity = dirTarget * 110.0f;
            tiempo = 0f;

            if (distancia > 13.0f && !usandoCanion)
            {
                usandoCanion = true;
            }
            if (distancia > 25 && municion < 15 && usandoCanion)
            {
                usandoCanion = false;
            }
            if (distancia < 11.0f && usandoCanion)
            {
                usandoCanion = false;
            }


            if (usandoCanion)
            {
                intervalo = 0.75f;
                bal.transform.localScale = Vector3.one * 1.45f;
                municion--;
            }
            else
            {
                intervalo = 0.47f;
            }

            if (tiempoRecarga > intervaloRecarga && municion < 40)
            {
                tiempoRecarga = 0f;
                municion++;
            }

            if (municion < 0)
            {
                municion = 0;
            }

            tiempo += Time.deltaTime;
            tiempoRecarga += Time.deltaTime;

        }

        transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
        tiempo += Time.deltaTime;
    }
}