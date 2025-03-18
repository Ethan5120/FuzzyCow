using UnityEngine;

public class ag1_estadoDie : State<agenteUno>
{
    public static ag1_estadoDie instance = null;
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
        Debug.Log("Adios Mundo Cruel");
        entidad.agente.speed = 0;
        entidad.cowState.text = "";
        entidad.Die();
    }

    public override void Execute(agenteUno entidad)
    {

    }
}