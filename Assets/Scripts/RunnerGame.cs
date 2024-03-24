using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class RunnerGame : MonoBehaviour
{
    public Item itemToSpawn;
    public float speed = 0.01f;
    public List<Item> items = new List<Item>();
    public Transform parent;
    public float maxHorizontal = 3f; // Adjust as needed
    public float minHorizontal = -3f; // Adjust as needed
    public float maxVertical = 3f; // Adjust as needed
    public float minVertical = 0f; // Adjust as needed
    public float destroyObj = -10f; // Adjust as needed

    private void Start()
    {
        InvokeRepeating(nameof(BeginSpawn), 1, 0.3f);
    }

    private void BeginSpawn()
    {
        var spawnedItem = Instantiate(itemToSpawn, parent);
        spawnedItem.itemPosition = GetRandomLocation();
        items.Add(spawnedItem);
    }

    private Vector3 GetRandomLocation()
    {
        var xRand = Random.Range(minHorizontal, maxHorizontal);
        var yRand = Random.Range(minVertical, maxVertical);
        return new Vector3(xRand, yRand, 0);
    }

    private void Update()
    {
        foreach (var item in items)
        {
            ItemMover(item);
        }

        transform.position = GetRandomLocation();
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        children.OrderBy(x => x.position.z);

        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }

    private void ItemMover(Item item)
    {
        item.itemPosition.z -= speed;
        /*if (item.itemPosition.z < destroyObj)
        {
            Destroy(item.gameObject);
            Debug.Log("Item Destroyed");
        }*/
    }
}

 