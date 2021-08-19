using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_SaveManager : MonoBehaviour
{
    public GameObject playerGO;

    public void Save()
    {
        J_SaveSystem.SavePlayer(playerGO);
    }

    public void Load()
    {
        J_PlayerData data = J_SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.playerPosition[0];
        position.y = data.playerPosition[1];
        position.z = data.playerPosition[2];
        playerGO.transform.position = position;

        Quaternion rotation;
        rotation.x = data.playerRotation[0];
        rotation.y = data.playerRotation[1];
        rotation.z = data.playerRotation[2];
        rotation.w = data.playerRotation[3];
        playerGO.transform.rotation = rotation;

        for (int i = 0; i < data.itemIDS.Length; i++)
        {
            J_inventoryManager.instance.AddItem(data.itemIDS[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
