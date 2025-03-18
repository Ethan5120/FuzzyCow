using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ag2_Descansito : State<agenteDos>
{
    // Start is called before the first frame update
    public static ag2_Descansito instance = null;
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
        Debug.Log("Descansar");
    }

    public override void Execute(agenteDos entidad)
    {
        entidad.energia += 8.5f*Time.deltaTime;
        if(entidad.energia >= 95.0f)
        {
            entidad.getFSM().ChangeState(ag2_estadoPatrulla.instance);
        }
    }

}
