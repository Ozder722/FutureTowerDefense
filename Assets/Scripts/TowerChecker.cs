using Unity.Netcode;
using UnityEngine;

public class TowerChecker : NetworkBehaviour
{
    [SerializeField] public bool placeAble=false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlaceAble"))
        {
            placeAble = true;
        }
        else
        {
            placeAble = false;
        }

    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("PlaceAble"))
    //    {
    //        placeAble = false;
    //    }

    //}
}
