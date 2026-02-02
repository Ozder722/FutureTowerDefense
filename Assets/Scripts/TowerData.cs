using UnityEngine;

[CreateAssetMenu(menuName = "TD/Tower")]
public class TowerData : ScriptableObject
{
    public GameObject networkPrefab; 
    public GameObject previewPrefab; 
    public int price;
}
