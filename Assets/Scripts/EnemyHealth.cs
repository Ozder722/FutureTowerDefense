using UnityEngine;
using Unity.Netcode;
using System;

public class EnemyHealth : NetworkBehaviour
{
    public event Action OnEnemyRemoved;

    [SerializeField] private int maxHp = 10;
    private NetworkVariable<int> hp = new NetworkVariable<int>();
   

    public override void OnNetworkSpawn()
    {
        if (IsServer)
            hp.Value = maxHp;
    }

    public void TakeDamage(int damage)
    {
        if (!IsServer) return;

        hp.Value -= damage;

        if (hp.Value <= 0)
        {
            RemoveEnemy();
        }
    }

    public void RemoveEnemy()
    {
        OnEnemyRemoved?.Invoke();
        GetComponent<NetworkObject>().Despawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;

        if (other.CompareTag("EndPoint"))
        {
            GetComponent<EnemyHealth>().RemoveEnemy();
        }
    }


}
