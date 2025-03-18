using UnityEngine;

public class ag1_estadoPatrulla : State<agenteUno>
{
    float tiempo;
    int idxObj;

    public static ag1_estadoPatrulla instance = null;
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
        entidad.cowState.text = "Idle";
    }

    public override void Execute(agenteUno entidad)
    {
        if(entidad.stress <= 100)
        {
            entidad.stress += 0.5f * Time.deltaTime;
        }

        if(entidad.hunger > 0)
        {
            entidad.hunger -= 6f * Time.deltaTime;
        }

        if(entidad.hunger > 70 && entidad.lactancy <= 100)
        {
            entidad.lactancy += 5f * Time.deltaTime;
        }
        else if(entidad.hunger < 70 && entidad.hunger > 40 && entidad.lactancy <= 100)
        {
            entidad.lactancy += 2.5f * Time.deltaTime;
        }

        if (tiempo > 5f)
        {
            tiempo = 0;
            idxObj = Random.Range(0, entidad.objetivosIdleJuego.Count);
            entidad.agente.destination = entidad.objetivosIdleJuego[idxObj].position;
        }
        tiempo += Time.deltaTime;

        foreach(Transform enemy in entidad.amenazas)
        {
            float distanciaAmenza = Vector3.Distance(entidad.transform.position, enemy.position);
            if (distanciaAmenza < entidad.distanciaEscape)
            {
                entidad.currentThreat = enemy;
                Debug.Log($"Chasing Wolf is {enemy.name}");
                entidad.getFSM().SetCurrentState(ag1_estadoEscapa.instance);
            }  
        }

        if(entidad.hunger < 30 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoPastar.instance);
        }

        if(entidad.lactancy > 70 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoMilk.instance);
        }

        if(entidad.stress >= 50 && entidad.energy > 30)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoJuego.instance);
        }

        if(entidad.energy < 30)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoRest.instance);
        }

        
    }
}