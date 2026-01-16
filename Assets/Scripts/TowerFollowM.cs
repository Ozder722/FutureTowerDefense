using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class TowerFollowM : NetworkBehaviour
{
    [SerializeField] GameObject testTower;

    private TowerChecker towerChecker; // reference til TowerChecker
    GameObject chosenTower;
    public bool placed;

    void Update()
    {
        if (!IsOwner) return; // kun lokal spiller

        if (chosenTower != null && !placed)
        {
            FollowMouse();
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RequestSpawnTowerServerRpc();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && towerChecker.placeAble)
        {
            if (TryGetMouseWorldPosition(out Vector3 spawnPos))
            {
                SpawnTowerServerRpc(spawnPos);
            }
        }

        
    }

    


    [ServerRpc]
    private void SpawnTowerServerRpc(Vector3 spawnpos)
    {
        GameObject tower = Instantiate(chosenTower, spawnpos, Quaternion.identity);

        NetworkObject netObj = tower.GetComponent<NetworkObject>();
        netObj.SpawnWithOwnership(OwnerClientId);
    }



    [ServerRpc]
    public void RequestSpawnTowerServerRpc()
    {
        GameObject tower = Instantiate(testTower);
        tower.GetComponent<NetworkObject>().Spawn();

        chosenTower = tower;
        towerChecker = chosenTower.GetComponent<TowerChecker>();
        Debug.Log("tower er sat");
    }

    void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit))
        {
            hit.point = new Vector3(hit.point.x, 0.5f, hit.point.z);
            chosenTower.transform.position = hit.point;
        }
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
