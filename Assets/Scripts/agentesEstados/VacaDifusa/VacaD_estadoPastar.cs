using UnityEngine;

public class VacaD_estadoPastar : State<VacaDifusa>
{
    int idxObj;
    float dist;

    public static VacaD_estadoPastar instance = null;
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
        entidad.cowState.text = "Pastando";
        idxObj = Random.Range(0, entidad.objetivosPastar.Count);
        entidad.agente.destination = entidad.objetivosPastar[idxObj].position;
    }

    public override void Execute(VacaDifusa entidad)
    {
        CheckDstanceFromTarget(entidad);   

        if(entidad.hunger > 0 && dist < 2)
        {
            entidad.hunger -= 25f * Time.deltaTime;
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

        if(entidad.hunger < 15)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoPatrulla.instance);
        }

        if(entidad.lactancy > 75 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoMilk.instance);
        }
    }

    public void CheckDstanceFromTarget(VacaDifusa entidad)
    {
        dist = Vector3.Distance(entidad.transform.position, entidad.agente.destination);
    }
}