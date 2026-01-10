using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    private void Update()
    {
        if (!IsOwner) return;
        Vector3 movDir = new Vector3(0,0, 0);
        if (Input.GetKey(KeyCode.W)) movDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) movDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) movDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) movDir.x = +1f;

        float movespeed = 3f;
        transform.position += movDir * movespeed * Time.deltaTime;
    }
}
