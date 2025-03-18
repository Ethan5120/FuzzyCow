using FuzzyLogicSystem;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class agenteArmas : MonoBehaviour
{
    public GameObject bala;

    public Transform target = null;

    public Transform source = null;

    public TextAsset fuzzyLogicData = null;
    public TextAsset fuzzyLogicDataArmas = null;

    private FuzzyLogic fuzzyLogic = null;
    private FuzzyLogic fuzzyLogicArmas = null;

    private int municion = 40;
    
    private float intervalo = 0.4f;
    private float tiempo = 0f;

    float intervaloRecarga = 2f;
    float tiempoRecarga = 0.0f;

    private void Start()
    {
        fuzzyLogic = FuzzyLogic.Deserialize(fuzzyLogicData.bytes, null);
        fuzzyLogicArmas = FuzzyLogic.Deserialize(fuzzyLogicDataArmas.bytes, null);
    }

    private void Update()
    {
        fuzzyLogic.evaluate = true;
        float distancia = Vector3.Distance(target.position, source.position); 
        fuzzyLogic.GetFuzzificationByName("distance").value = distancia;

        float speed = fuzzyLogic.Output() * fuzzyLogic.defuzzification.maxValue;

        source.position = Vector3.MoveTowards(source.position, target.position, speed * Time.deltaTime);

        fuzzyLogicArmas.GetFuzzificationByName("distanciaObjetivo").value = Vector3.Distance(target.position, source.position);
        fuzzyLogicArmas.GetFuzzificationByName("municion").value = municion;
        float usarCanion = fuzzyLogicArmas.Output();
        Debug.Log(usarCanion);
        Vector3 dirTarget = Vector3.Normalize(target.position - source.position);
        GameObject bal = null;

        if (tiempo > intervalo)
        {
            
            bal = Instantiate(bala, transform.position + dirTarget, Quaternion.identity);
            bal.GetComponent<Rigidbody>().velocity = dirTarget * 110.0f;
            tiempo = 0f;
            if(usarCanion > 0.32f)
            {
                intervalo = 0.73f;
                bal.transform.localScale = Vector3.one*1.5f;
                
                municion--;
            }
            else
            {
                intervalo = 0.4f;
            }

        }


        if(tiempoRecarga > intervaloRecarga && municion <40)
        {
            tiempoRecarga = 0f;
            municion++;
        }
        
        if(municion<0)
        {
            municion = 0;
        }
        

        transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z); 
        tiempo += Time.deltaTime;
        tiempoRecarga += Time.deltaTime;
    }
}
