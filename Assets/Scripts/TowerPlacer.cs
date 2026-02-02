using Unity.Netcode;
using UnityEngine;

public class TowerPlacer : NetworkBehaviour
{
    //[SerializeField] GameObject Tower1;


    //[SerializeField] GameObject Tower1Prefab;
    private GameObject previewTower;


    private TowerChecker towerChecker; // reference til TowerChecker
    GameObject chosenTower;
    public bool placed;

    [SerializeField] TowerDataBase towerDatabase;

    int selectedTowerIndex = -1;
    
    

    void Update()
    {


        if (previewTower != null && !placed)
        {
            FollowMouse();

        }


        //if (Input.GetKeyDown(KeyCode.Mouse0) && previewTower != null && towerChecker.placeAble)
        //{
        //    if (TryGetMouseWorldPosition(out Vector3 spawnPos))
        //    {
        //        SpawnTowerServerRpc(spawnPos);
        //        Destroy(previewTower);
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.Mouse0) && previewTower != null && towerChecker.placeAble)
        {
            if (TryGetMouseWorldPosition(out Vector3 spawnPos))
            {
                SpawnTowerServerRpc(selectedTowerIndex, spawnPos);
                Destroy(previewTower);
                selectedTowerIndex = -1;
            }
        }


    }

    public void SelectTower(int index)
    {
        if (previewTower != null)
            Destroy(previewTower);

        selectedTowerIndex = index;

        previewTower = Instantiate(towerDatabase.towers[index].previewPrefab);
        

        towerChecker = previewTower.GetComponent<TowerChecker>();
    }




    [ServerRpc(RequireOwnership = false)]
    //private void SpawnTowerServerRpc(Vector3 spawnPos)
    //{
    //    GameObject tower = Instantiate(Tower1, spawnPos, Quaternion.identity);

    //    NetworkObject netObj = tower.GetComponent<NetworkObject>();
    //    netObj.Spawn();
    //}
    void SpawnTowerServerRpc(int towerIndex, Vector3 spawnPos)
    {
        TowerData data = towerDatabase.towers[towerIndex];

        GameObject tower = Instantiate(
            data.networkPrefab,
            spawnPos,
            Quaternion.identity
        );

        tower.GetComponent<NetworkObject>().Spawn();
    }




    

    void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            hit.point = new Vector3(hit.point.x, 0.5f, hit.point.z);
            //chosenTower.transform.position = hit.point;
            previewTower.transform.position = hit.point;
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
