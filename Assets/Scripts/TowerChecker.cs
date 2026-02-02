using Unity.Netcode;
using UnityEngine;

public class TowerChecker : NetworkBehaviour
{
    [SerializeField] public bool placeAble=false;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Road") || other.CompareTag("Tower"))
        {
            placeAble = false;
            return;
        }
        if (other.CompareTag("PlaceAble"))
        {
            Debug.Log("Placer bart område");
            placeAble = true;
        }
    }
    /*

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlaceAble"))
        {
            Debug.Log("Placer bart område");
            placeAble = true;
        }
        if(other.CompareTag("Road"))
        {
            Debug.Log("Road ramt");
            placeAble = false;
        }
        if (other.CompareTag("Tower"))
        {
            Debug.Log("tower ramt");
            placeAble=false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Road") || other.CompareTag("Tower"))
        {
            placeAble = true;
        }
    }

    */
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("PlaceAble"))
    //    {
    //        placeAble = false;
    //    }

    //}
}
