using UnityEngine;
using Unity.Netcode;
using System.Runtime.CompilerServices;

public class PlayerNetwork : NetworkBehaviour
{

    [SerializeField] private GameObject spawnObjPrefab;
    private GameObject spawnObj;

    private void Update()
    {
        if (!IsOwner) return;

        TestMovement();



        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DestroyTowerServerRpc();
        }


    }

    private void TestMovement()
    {
        Vector3 movDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) movDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) movDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) movDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) movDir.x = +1f;

        float movespeed = 3f;
        transform.position += movDir * movespeed * Time.deltaTime;
    }


    



    [ServerRpc]
    private void DestroyTowerServerRpc()
    {
        spawnObj.GetComponent<NetworkObject>().Despawn();
    }

    private bool TryGetMouseWorldPosition(out Vector3 worldPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            worldPos = hit.point;
            return true;
        }

        worldPos = Vector3.zero;
        return false;
    }


}
