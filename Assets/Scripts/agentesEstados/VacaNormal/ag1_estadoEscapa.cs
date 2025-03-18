using UnityEngine;


public class ag1_estadoEscapa : State<agenteUno>
{


    public static ag1_estadoEscapa instance = null;
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

    public override void Enter(agenteUno entidad)
    {
        Debug.Log("entra Edo Escape");
        entidad.agente.speed = 5;
        entidad.cowState.text = "Escaping";
    }

    public override void Execute(agenteUno entidad)
    {
        entidad.stress += 7.5f * Time.deltaTime;
        if(entidad.energy > 0)
        {
            entidad.energy -= 7.5f * Time.deltaTime;
        }

        float distancia = Vector3.Distance(entidad.currentThreat.position, entidad.transform.position);

        if( distancia < entidad.distanciaEscape)
        {
            entidad.agente.destination = entidad.objetivoSeguro.position;
        }
            

        float zonaSeguraDis = Vector3.Distance(entidad.objetivoSeguro.position, entidad.transform.position);
        if(zonaSeguraDis < 1.1f || distancia > 12f)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoRest.instance);
        }

        if(entidad.stress > 95)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoDie.instance);
        }
        else if(entidad.stress > 75 && entidad.hunger < 30)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoDie.instance);
        }
        
        


    }



}
