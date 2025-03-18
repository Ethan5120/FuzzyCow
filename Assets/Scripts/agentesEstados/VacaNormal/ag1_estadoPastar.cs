using UnityEngine;

public class ag1_estadoPastar : State<agenteUno>
{
    int idxObj;
    float dist;

    public static ag1_estadoPastar instance = null;
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
        entidad.cowState.text = "Pastando";
        idxObj = Random.Range(0, entidad.objetivosPastar.Count);
        entidad.agente.destination = entidad.objetivosPastar[idxObj].position;
    }

    public override void Execute(agenteUno entidad)
    {
        CheckDstanceFromTarget(entidad);   

        if(entidad.hunger <= 100 && dist < 2)
        {
            entidad.hunger += 15f * Time.deltaTime;
        }
        
        if(entidad.stress > 0 && dist < 2)
        {
            entidad.stress -= 2.5f * Time.deltaTime;
        }

        if(entidad.hunger > 70 && entidad.lactancy <= 100 && dist < 2)
        {
            entidad.lactancy += 5f * Time.deltaTime;
        }
        else if(entidad.hunger < 70 && entidad.hunger > 40 && entidad.lactancy <= 100)
        {
            entidad.lactancy += 2.5f * Time.deltaTime;
        }

        


        foreach(Transform enemy in entidad.amenazas)
        {
            float distanciaAmenza = Vector3.Distance(entidad.transform.position, enemy.position);
            if (distanciaAmenza < entidad.distanciaEscape)
            {
                entidad.currentThreat = enemy;
                entidad.getFSM().SetCurrentState(ag1_estadoEscapa.instance);
            }  
        }

        if(entidad.hunger > 85)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoPatrulla.instance);
        }

        if(entidad.lactancy > 70 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoMilk.instance);
        }
    }

    public void CheckDstanceFromTarget(agenteUno entidad)
    {
        dist = Vector3.Distance(entidad.transform.position, entidad.agente.destination);
    }
}