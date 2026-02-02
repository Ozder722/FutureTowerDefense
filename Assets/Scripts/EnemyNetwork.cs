using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNetwork : NetworkBehaviour
{
    private NavMeshAgent agent;
    private List<Transform> walkPoints;
    private int currentWalkPointIndex;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        GivePath();
    }
    public void GivePath()
    {
        walkPoints = EnemyPath.instance.walkPoints;
        
        currentWalkPointIndex = 0;

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
        if (walkPoints.Count == 0)
            
        return;

        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
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
