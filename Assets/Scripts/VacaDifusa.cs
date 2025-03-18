using System.Collections;
using System.Collections.Generic;
using FuzzyLogicSystem;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class VacaDifusa : MonoBehaviour
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

    [SerializeField] StateMachine<VacaDifusa> maquinaEstados;

    [Header("FuzzyLogic")]
    public TextAsset fuzzyCowData = null;
    private FuzzyLogic fuzzyCow = null;

    // Start is called before the first frame update
    void Start()
    {
        fuzzyCow = FuzzyLogic.Deserialize(fuzzyCowData.bytes, null);


        agente = GetComponent<NavMeshAgent>();
        //agente.destination = objetivos[0].position;

        maquinaEstados = new StateMachine<VacaDifusa>(this);
        maquinaEstados.SetCurrentState(VacaD_estadoPatrulla.instance);
    }

    // Update is called once per frame
    void Update()
    {
        fuzzyCow.evaluate = true;
        fuzzyCow.GetFuzzificationByName("Estres").value = stress;
        fuzzyCow.GetFuzzificationByName("Hambre").value = hunger;

        lactancy = fuzzyCow.Output() * fuzzyCow.defuzzification.maxValue;
        Debug.Log(lactancy);
        
    }


     private void FixedUpdate()
    {
        maquinaEstados.Updating();
    }

    public StateMachine<VacaDifusa> getFSM()
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
