using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ag2_estadoPatrulla : State<agenteDos>
{
    int idxObj;

    public static ag2_estadoPatrulla instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    
    public override void Enter(agenteDos entidad)
    {
        Debug.Log("agente2 patrulla");
        entidad.agente.speed = 4;
    }

    public override void Execute(agenteDos entidad)
    {
        entidad.energia-= Time.deltaTime*3f;
        entidad.hambre-= Time.deltaTime*1f;
        float distanciaObj = Vector3.Distance(entidad.transform.position, entidad.objetivos[idxObj].position);
        if (distanciaObj < 2)
        {
            idxObj = Random.Range(0, entidad.objetivos.Count);
        }
        entidad.agente.destination = entidad.objetivos[idxObj].position;


        foreach(Transform presa in entidad.Presas)
        {
            if(presa.gameObject.activeSelf)
            {
                float distanciaPresa = Vector3.Distance(entidad.transform.position, presa.position);
                if(distanciaPresa < entidad.distanciaPersigue)
                {
                    entidad.objetivoActual = presa;
                    entidad.getFSM().SetCurrentState(ag2_estadoPersigue.instance);
                }
            }
        }

        if(entidad.energia < 22f)
        {
            entidad.getFSM().ChangeState(ag2_Descansito.instance);
        }
             

    }
}