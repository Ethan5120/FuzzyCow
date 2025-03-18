using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINav : MonoBehaviour
{
    public Transform[] objectives;
    [SerializeField] private float distance;

    NavMeshAgent agent;
    [SerializeField] int currentTarget;
    int genNumber;
    [SerializeField] float cSpeed = 1;
    [SerializeField] float distanceToReach = 1f;

    float tiempo = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = objectives[Random.Range(0, objectives.Length)].position;
        currentTarget = 1;
        agent.speed = cSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, agent.destination);
        if(distance <= distanceToReach)
        {
            Debug.Log("Targetn Reached");
            do
            {
                genNumber = Random.Range(0, objectives.Length);
                Debug.Log("Gen New Target");
            }
            while(genNumber == currentTarget);
            currentTarget = genNumber;
            agent.destination = objectives[currentTarget].position;
        }
    }
}
