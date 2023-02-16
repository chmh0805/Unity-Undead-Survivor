using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    /*
     * Variable For Prefabs * 2
     * List for pooling * 2
     */
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 1. Get a idle GameObject in selected pool.
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf) // 2-1. Alloc to `select` variable if '1' success.
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null) // 2-1. if not, create new one and alloc to `select` variable
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
            // select.SetActive(true);
        }

        return select;
    }
}
