using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNetwork : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> walkPoints;
  



    private int currentWalkPointIndex = 0;

    private void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {


        if (walkPoints.Count > 0)
        {
            agent.SetDestination(walkPoints[0].position);
        }
    }

    private void Update()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (walkPoints.Count == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= 0.2f)
        {
            currentWalkPointIndex++;

            if (currentWalkPointIndex >= walkPoints.Count)
            {
                ReachedEnd();
                return;
            }

            agent.SetDestination(walkPoints[currentWalkPointIndex].position);
        }
    }

    private void ReachedEnd()
    {

        Console.WriteLine("Nået til enden");
        // TODO:
        // - skade spilleren
        // - reducér liv
        // - despawn enemy

        //Destroy(gameObject);
    }
}
