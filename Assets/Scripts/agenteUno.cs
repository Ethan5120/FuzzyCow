using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class agenteUno : MonoBehaviour
{

    public float distanciaEscape = 5f;

    public List<Transform> objetivosIdleJuego;
    public List<Transform> objetivosPastar;
    public List<Transform> objetivosMilk;

    public Transform[] amenazas;
    public Transform currentThreat = null;
    public Transform objetivoSeguro;

    public NavMeshAgent agente;
    [Header("CowStats")]
    public float stress = 0;
    public float hunger = 100;
    public float lactancy = 0;
    public float energy = 100;
    public TextMeshPro cowState;

    [Header("Cow Death")]
    [SerializeField] AudioSource deadSound;
    [SerializeField] ParticleSystem[] explotionFX;

    [SerializeField] StateMachine<agenteUno> maquinaEstados;

    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        //agente.destination = objetivos[0].position;

        maquinaEstados = new StateMachine<agenteUno>(this);
        maquinaEstados.SetCurrentState(ag1_estadoPatrulla.instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        maquinaEstados.Updating();
    }

    public StateMachine<agenteUno> getFSM()
    {
        return maquinaEstados;
    }

    public void TakeDamage()
    {
        stress += 15;
    }

    public void Die()
    {
        foreach(ParticleSystem parts in explotionFX)
        {
            parts.Play();
        }
        deadSound.Play();
        Invoke("Despawn", 1f);
        
    }

    private void Despawn()
    {
        gameObject.SetActive(false);
    }
}
