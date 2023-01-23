using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxItemSpawner : MonoBehaviour
{
    public List<GameObject> spawnablePrefabs;
    public bool destroyAfterSpawn;
    public int spawnQuantity = 1;
    public UIManager uimanager;

    public void SpawnItem()
    {
        if(spawnablePrefabs.Count > 0)
        {
            for (int i = 0; i < spawnQuantity; i++)
            {
                int n = Random.Range(0, spawnablePrefabs.Count);
                GameObject spawned = Instantiate(spawnablePrefabs[n], transform.position, spawnablePrefabs[n].transform.rotation);
                //if (spawned.GetComponent<Healer>() == null)
                //    GetComponent<Collectible>().CollectResponse.AddListener(uimanager.AddOneDiamond);
            }
            if (destroyAfterSpawn)
                Destroy(this.gameObject);
        }
            
    }
}
