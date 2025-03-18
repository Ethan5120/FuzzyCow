using UnityEngine;

public class VacaD_estadoMilk : State<VacaDifusa>
{
    int idxObj;
    float dist;

    public static VacaD_estadoMilk instance = null;
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
    public override void Enter(VacaDifusa entidad)
    {
        Debug.Log("entra Patrulla Agente1");
        entidad.agente.speed = 3;
        entidad.cowState.text = "Odeñando";
        idxObj = Random.Range(0, entidad.objetivosMilk.Count);
        entidad.agente.destination = entidad.objetivosMilk[idxObj].position;
    }

    public override void Execute(VacaDifusa entidad)
    {
        CheckDstanceFromTarget(entidad); 
        
        
        if(entidad.stress <= 100 && dist < 2)
        {
            entidad.stress += 15f * Time.deltaTime;
        }

        if(entidad.hunger <= 100 && dist < 2)
        {
            entidad.hunger += 10f * Time.deltaTime;
        }


        foreach(Transform enemy in entidad.amenazas)
        {
            float distanciaAmenza = Vector3.Distance(entidad.transform.position, enemy.position);
            if (distanciaAmenza < entidad.distanciaEscape)
            {
                entidad.currentThreat = enemy;
                entidad.getFSM().SetCurrentState(VacaD_estadoEscapa.instance);
            }  
        }

        if(entidad.hunger > 85)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoPastar.instance);
        }

        if(entidad.lactancy < 30)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoPatrulla.instance);
        }
    }

    public void CheckDstanceFromTarget(VacaDifusa entidad)
    {
        dist = Vector3.Distance(entidad.transform.position, entidad.agente.destination);
    }
}