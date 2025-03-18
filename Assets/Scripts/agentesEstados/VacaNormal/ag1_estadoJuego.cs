using UnityEngine;

public class ag1_estadoJuego : State<agenteUno>
{
    float tiempo;
    int idxObj;

    public static ag1_estadoJuego instance = null;
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
        Debug.Log("entra Juego Vaca");
        entidad.agente.speed = 4;
        entidad.cowState.text = "Jugando";
    }

    public override void Execute(agenteUno entidad)
    {
        if(entidad.stress > 0)
        {
            entidad.stress -= 5f * Time.deltaTime;
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
                entidad.getFSM().SetCurrentState(ag1_estadoEscapa.instance);
            }  
        }

        if(entidad.stress < 10)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoPatrulla.instance);
        }

        if(entidad.energy < 15)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoRest.instance);
        }

        if(entidad.hunger < 30 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoPastar.instance);
        }

        if(entidad.lactancy > 70 && entidad.stress < 50)
        {
            entidad.getFSM().SetCurrentState(ag1_estadoMilk.instance);
        }
    }
}