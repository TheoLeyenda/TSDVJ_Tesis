using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class J_PlayerData
{
    public float[] playerPosition;
    public float[] playerRotation;
    public int[] itemIDS;

    public J_PlayerData(GameObject playerGO)
    {
        playerPosition = new float[3];

        playerPosition[0] = playerGO.transform.position.x;
        playerPosition[1] = playerGO.transform.position.y;
        playerPosition[2] = playerGO.transform.position.z;

        playerRotation = new float[4];

        playerRotation[0] = playerGO.transform.rotation.x;
        playerRotation[1] = playerGO.transform.rotation.y;
        playerRotation[2] = playerGO.transform.rotation.z;
        playerRotation[3] = playerGO.transform.rotation.w;

        itemIDS = J_inventoryManager.instance.GetItemsIDS();
    }
}
