using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agenteDos : MonoBehaviour
{

    public float distanciaPersigue = 8f;
    public float energia = 100f;
    public float hambre = 100f;

    StateMachine<agenteDos> maquinaEstados;

    public List<Transform> objetivos;

    public Transform[] Presas;
    public Transform objetivoActual;
    

    public NavMeshAgent agente;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        //agente.destination = objetivos[0].position;

        maquinaEstados = new StateMachine<agenteDos>(this);
        maquinaEstados.SetCurrentState(ag2_estadoPatrulla.instance);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        maquinaEstados.Updating();
    }

    public StateMachine<agenteDos> getFSM()
    {
        return maquinaEstados;
    }
}
