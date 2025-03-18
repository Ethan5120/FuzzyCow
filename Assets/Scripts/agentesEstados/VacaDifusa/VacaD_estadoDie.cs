using UnityEngine;

public class VacaD_estadoDie : State<VacaDifusa>
{
    public static VacaD_estadoDie instance = null;
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
        Debug.Log("Adios Mundo Cruel");
        entidad.agente.speed = 0;
        entidad.cowState.text = "";
        entidad.Die();
    }

    public override void Execute(VacaDifusa entidad)
    {

    }
}