using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionOnPlaceObjects : MonoBehaviour
{
    [SerializeField] private J_Inventory inventory;
    [SerializeField] private bool useCheckInPlaceObject;
    [System.Serializable]
    public class SpawnerItem
    { 
        [System.Serializable]
        public class Item
        {
            public GameObject itemObject;
            public J_Item item;
            public GameObject transparentObject;
            public int id_item;
            public bool itemSpawned = false;
        }

        public bool itemPlace;
        public SpawnedItem spawnedItem;

        public class SpawnedItem
        {
            public GameObject objectItem;
            public int id;
        }


        private int currentIdItem;
        public int compareIdItem;
        
        public Item[] items;

        private J_Inventory inventoryPlayer;

        public void SetInventoryPlayer(J_Inventory value) => inventoryPlayer = value;

        public Transform transformSpawnItem;

        public void SpawnObject(int id, GameObject itemPlayer)
        {
            for(int i = 0; i < items.Length; i++)
            {
                if (id == items[i].id_item)
                {
                    items[i].transparentObject.SetActive(false);
                    spawnedItem.objectItem = Instantiate(items[i].itemObject, transformSpawnItem.position, transformSpawnItem.rotation, transformSpawnItem);
                    spawnedItem.objectItem.transform.localScale = new Vector3(1, 1, 1);
                    spawnedItem.id = id;
                    itemPlayer.SetActive(false);

                    inventoryPlayer.RemoveItem(items[i].item);

                    items[i].itemSpawned = true;
                    currentIdItem = items[i].id_item;
                    itemPlace = true;
                    i = items.Length;
                }
            }
        }

        public void DestroyObject(int id, GameObject itemPlayer)
        {
            if (spawnedItem != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (id == items[i].id_item)
                    {
                        items[i].transparentObject.SetActive(true);

                        itemPlayer.SetActive(true);
                        inventoryPlayer.AddItem(items[i].item);

                        items[i].itemSpawned = false;
                        itemPlace = false;
                        i = items.Length;
                    }
                }
                Destroy(spawnedItem.objectItem);
            }
        }
    }

    [System.Serializable]
    public class ItemPlayer
    {
        public GameObject go;
        public int id;
    }

    public SpawnerItem[] spawnersItems;
    public ItemPlayer[] itemsPlayer;

    [SerializeField] private int[] answerPuzzleIds;

    [SerializeField] private UnityEvent eventOnCorrectPlaceObjects;

    private void Start()
    {
        for (int i = 0; i < spawnersItems.Length; i++)
        {
            spawnersItems[i].SetInventoryPlayer(inventory);
            spawnersItems[i].spawnedItem = new SpawnerItem.SpawnedItem();
        }
    }

    public void AddOrRemoveObject(int indexSpawnItems)
    {
        int indexItemPlayer = -1;

        for (int i = 0; i < itemsPlayer.Length; i++)
        {
            if (itemsPlayer[i].go.activeSelf)
            {
                indexItemPlayer = i;
                i = itemsPlayer.Length;
            }
        }

        if(spawnersItems[indexSpawnItems].itemPlace)
        {
            for (int i = 0; i < itemsPlayer.Length; i++)
            {
                if (itemsPlayer[i].id == spawnersItems[indexSpawnItems].spawnedItem.id)
                {
                    for (int j = 0; j < itemsPlayer.Length; j++)
                    {
                        itemsPlayer[j].go.SetActive(false);
                    }
                    spawnersItems[indexSpawnItems].DestroyObject(itemsPlayer[i].id, itemsPlayer[i].go);
                    i = itemsPlayer.Length;
                }
            }
        }

        if (indexItemPlayer == -1)
            return;

        for (int i = 0; i < spawnersItems[indexSpawnItems].items.Length; i++)
        {
            if (itemsPlayer[indexItemPlayer].id == spawnersItems[indexSpawnItems].items[i].id_item)
            {
                if (!spawnersItems[indexSpawnItems].items[i].itemSpawned)
                {
                    spawnersItems[indexSpawnItems].SpawnObject(itemsPlayer[indexItemPlayer].id, itemsPlayer[indexItemPlayer].go);
                    i = spawnersItems[indexSpawnItems].items.Length;
                }
               
            }
        }

        if (useCheckInPlaceObject)
        {
            CheckEventOnPlaceObject();
        }

    }

    public void CheckEventOnPlaceObject()
    {
        bool enableEvent = true;
        for (int i = 0; i < answerPuzzleIds.Length; i++)
        {
            if (answerPuzzleIds[i] != spawnersItems[i].spawnedItem.id)
            {
                enableEvent = false;
                i = answerPuzzleIds.Length;
            }
        }

        if (enableEvent)
        {
            Debug.Log("Llegue al final del puzzle solo falta incorporar una funcion perri");
            eventOnCorrectPlaceObjects?.Invoke();
            enabled = false;
        }

    }

}
