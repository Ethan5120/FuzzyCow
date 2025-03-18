using UnityEngine;

public class ag1_estadoMilk : State<agenteUno>
{
    float tiempo;
    int idxObj;
    float dist;

    public static ag1_estadoMilk instance = null;
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
        Debug.Log("entra Patrulla Agente1");
        entidad.agente.speed = 3;
        entidad.cowState.text = "Odeñando";
        idxObj = Random.Range(0, entidad.objetivosMilk.Count);
        entidad.agente.destination = entidad.objetivosMilk[idxObj].position;
    }

    public override void Execute(agenteUno entidad)
    {
        CheckDstanceFromTarget(entidad); 
        
        
        if(entidad.lactancy > 0 && dist < 2)
        {
            entidad.lactancy -= 15f * Time.deltaTime;
        }

        if(entidad.hunger > 0 && dist < 2)
        {
            entidad.hunger -= 6f * Time.deltaTime;
        }

        tiempo += Time.deltaTime;

        foreach(Transform enemy in entidad.amenazas)
        {
            float distanciaAmenza = Vector3.Distance(entidad.transform.position, enemy.position);
            if (distanciaAmenza < entidad.distanciaEscape)
            {
                entidad.currentThreat = enemy;
                entidad.getFSM().SetCurrentState(ag1_estadoEscapa.instance);
            }  
        }

        if(entidad.hunger < 30 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoPastar.instance);
        }

        if(entidad.lactancy < 10)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoPatrulla.instance);
        }
    }

    public void CheckDstanceFromTarget(agenteUno entidad)
    {
        dist = Vector3.Distance(entidad.transform.position, entidad.agente.destination);
    }
}