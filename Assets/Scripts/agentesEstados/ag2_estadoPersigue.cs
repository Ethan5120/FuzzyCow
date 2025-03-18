using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ag2_estadoPersigue : State<agenteDos>
{


    public static ag2_estadoPersigue instance = null;
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
        entidad.agente.destination = entidad.objetivoActual.position;
        entidad.agente.speed = 6 - (entidad.hambre / 100); 
    }

    public override void Execute(agenteDos entidad)
    {
        entidad.energia -= Time.deltaTime*6.5f;
        float distanciaPierde = Vector3.Distance(entidad.transform.position, entidad.objetivoActual.position);
        if( distanciaPierde > 8.5f)
        {
            entidad.getFSM().SetCurrentState(ag2_estadoPatrulla.instance);
        }

        entidad.agente.destination = entidad.objetivoActual.position;

        if (entidad.energia < 30)
        {
            entidad.objetivoActual = null;
            entidad.getFSM().ChangeState(ag2_Descansito.instance);
        }

                
    }
    
}
