

using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class TowerFollowM : NetworkBehaviour
{
    [SerializeField] GameObject Tower1;
    

    [SerializeField] GameObject Tower1Prefab;
    private GameObject previewTower1;


    private TowerChecker towerChecker; // reference til TowerChecker
    GameObject chosenTower;
    public bool placed;

    void Update()
    {
        

        if (previewTower1 != null && !placed)
        {
            FollowMouse();

        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && previewTower1 != null && towerChecker.placeAble)
        {
            if (TryGetMouseWorldPosition(out Vector3 spawnPos))
            {
                SpawnTowerServerRpc(spawnPos);
                Destroy(previewTower1);
            }
        }



    }

    public void PreviewTower()
    {
        previewTower1 = Instantiate(Tower1Prefab);
        towerChecker = previewTower1.GetComponent<TowerChecker>();
    }


    

    [ServerRpc(RequireOwnership =false)]
    private void SpawnTowerServerRpc(Vector3 spawnPos)
    {
        GameObject tower = Instantiate(Tower1, spawnPos, Quaternion.identity);

        NetworkObject netObj = tower.GetComponent<NetworkObject>();
        netObj.Spawn();
    }




    [ServerRpc]
    public void RequestSpawnTowerServerRpc()
    {
        GameObject tower = Instantiate(Tower1);
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
            //chosenTower.transform.position = hit.point;
            previewTower1.transform.position = hit.point;
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
