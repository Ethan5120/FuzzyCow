using UnityEngine;

public class VacaD_estadoJuego : State<VacaDifusa>
{
    float tiempo;
    int idxObj;

    public static VacaD_estadoJuego instance = null;
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
        Debug.Log("entra Juego Vaca");
        entidad.agente.speed = 4;
        entidad.cowState.text = "Jugando";
    }

    public override void Execute(VacaDifusa entidad)
    {
        if(entidad.stress > 0)
        {
            entidad.stress -= 5f * Time.deltaTime;
        }

        if(entidad.hunger <= 100)
        {
            entidad.hunger += 8f * Time.deltaTime;
        }

        if(entidad.energy > 0)
        {
            entidad.energy -= 5f * Time.deltaTime;
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
                entidad.getFSM().SetCurrentState(VacaD_estadoEscapa.instance);
            }  
        }

        if(entidad.stress < 10)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoPatrulla.instance);
        }

        if(entidad.energy < 15)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoRest.instance);
        }

        if(entidad.hunger > 85 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoPastar.instance);
        }

        if(entidad.lactancy > 75 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(VacaD_estadoMilk.instance);
        }
    }
}