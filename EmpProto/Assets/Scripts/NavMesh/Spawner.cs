using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnPool;
    public GameObject quad;

    [SerializeField]
    float spawnTime;
    [SerializeField]
    float spawnDelay;
    [SerializeField]
    float spawnRadius;

    void Start()
    {
        Invoke("spawnObjects", 1);
        //this.transform.position = Random.insideUnitCircle * spawnRadius;
    }
    private void Update()
    {
        if (ShouldSpawn())
        {
            spawnObjetcs();
        }
    }

    /*public void spawnObjects()
    {
       for(int i = 0;i < spawnPool.Count; i++)
        {
            for(int j = 0; j< numberToSpawn; j++)
            {
                Instantiate(spawnPool[j], quad.transform, true);
            }
        }
    }*/
    private bool ShouldSpawn()
    {
        return Time.deltaTime >= spawnTime;
    }
    public void spawnObjetcs()
    {
        spawnTime = Time.deltaTime + spawnDelay;
        for (int i = 0; i < spawnPool.Count; i++) {
           
            Instantiate(spawnPool[i], this.transform.position, Quaternion.identity);

        }
    }
    private void destroyObjects()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Spawnable"))
        {
            Destroy(o);
        }
    }


}
