using UnityEngine;

public class VacaD_estadoRest : State<VacaDifusa>
{
    float tiempo;
    int idxObj;

    public static VacaD_estadoRest instance = null;
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
        entidad.agente.speed = 1f;
        entidad.cowState.text = "Descansando";
    }

    public override void Execute(VacaDifusa entidad)
    {
        if(entidad.energy < 100)
        {
            entidad.energy += 10 * Time.deltaTime;
        }

        if(entidad.stress > 0)
        {
            entidad.stress -= 1f * Time.deltaTime;
        }

        if(entidad.hunger <= 100)
        {
            entidad.hunger += 6f * Time.deltaTime;
        }



        if (tiempo > 5f)
        {
            tiempo = 0;
            idxObj = Random.Range(0, entidad.objetivosIdleJuego.Count);
            entidad.agente.destination = entidad.objetivosIdleJuego[idxObj].position;
        }
        tiempo += Time.deltaTime;

        if(entidad.energy > 90)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoPatrulla.instance);
        }

        if(entidad.stress > 50 && entidad.energy > 70)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoJuego.instance);
        }

        if(entidad.lactancy > 75 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoMilk.instance);
        }

        if(entidad.hunger > 85 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoPastar.instance);
        }

        foreach(Transform enemy in entidad.amenazas)
        {
            float distanciaAmenza = Vector3.Distance(entidad.transform.position, enemy.position);
            if (distanciaAmenza < entidad.distanciaEscape)
            {
                entidad.currentThreat = enemy;
                Debug.Log($"Chasing Wolf is {enemy.name}");
                entidad.getFSM().SetCurrentState(VacaD_estadoEscapa.instance);
            }  
        }
    }
}