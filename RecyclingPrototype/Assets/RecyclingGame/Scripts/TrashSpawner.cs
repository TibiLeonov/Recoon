using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{

    public GameObject throwablePrefab;
    public ThrowablesScriptableObject[] trashDatas;

    public Vector3 spawnAreaCorner1;
    public Vector3 spawnAreaCorner2;

    public float timeToSpawn = 5;

    public ThrowablesScriptableObject lastTrash1;
    public ThrowablesScriptableObject lastTrash2;


    public float chanceForPileDrop = 0.05f;
    public GameObject pileDrop;

    public bool spawning;

    public int trashNumber;


    void Start()
    {


    }

    public void StartSpawning()
    {
        spawning = true;
        StartCoroutine(RepeatedSpawning(timeToSpawn));

    }

    public void StopSpawning()
    {
        spawning = false;

        StopAllCoroutines();

    }

    IEnumerator RepeatedSpawning(float _timer)
    {
        float time = _timer;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }


        SpawnTrash(GetRandomLocation(), NextRandomTrashType());
        StartCoroutine(RepeatedSpawning(timeToSpawn));

        yield return null;
    }

    //gets next trash spawn location
    public Vector3 GetRandomLocation()
    {
        float randomX, randomY, randomZ;
        if (spawnAreaCorner1.x < spawnAreaCorner2.x)
        {
            randomX = Random.Range(spawnAreaCorner1.x, spawnAreaCorner2.x);
        }
        else
        {
            randomX = spawnAreaCorner1.x;
        }
        if (spawnAreaCorner1.y < spawnAreaCorner2.y)
        {
            randomY = Random.Range(spawnAreaCorner1.y, spawnAreaCorner2.y);
        }
        else
        {
            randomY = spawnAreaCorner1.y;
        }
        if (spawnAreaCorner1.z < spawnAreaCorner2.z)
        {
            randomZ = Random.Range(spawnAreaCorner1.z, spawnAreaCorner2.z);
        }
        else
        {
            randomZ = spawnAreaCorner1.z;
        }


        return new Vector3(randomX, randomY, randomZ);
    }

    //gets next trash spawn type
    public ThrowablesScriptableObject NextRandomTrashType()
    {



        ThrowablesScriptableObject pickedType = trashDatas[Random.Range(0, trashDatas.Length)];

        if (trashDatas.Length > 2)
        {



            while (pickedType == null || pickedType == lastTrash1 || pickedType == lastTrash2)
            {
                pickedType = trashDatas[Random.Range(0, trashDatas.Length)];
            }


            lastTrash2 = lastTrash1;
            lastTrash1 = pickedType;
        }

        return pickedType;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((spawnAreaCorner1 + spawnAreaCorner2) / 2, new Vector3((spawnAreaCorner1.x - spawnAreaCorner2.x), 1, (spawnAreaCorner1.z - spawnAreaCorner2.z)));
    }

    //spawns trash at location
    public void SpawnTrash(Vector3 location, ThrowablesScriptableObject trashType)
    {
        if ( trashNumber<9 )
        {
            //not the drop
            Quaternion qua = Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
            GameObject spawnedTrash = Instantiate(throwablePrefab, location, qua);
            spawnedTrash.GetComponentInChildren<Throwable>().throwableData = trashType;
            spawnedTrash.GetComponentInChildren<Throwable>().LoadData();
            trashNumber++;
        }
        else if( trashNumber==9  )
        {

            foreach (Transform child in pileDrop.transform)
            {
                Quaternion qua = Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
                GameObject spawnedTrash = Instantiate(throwablePrefab, location + child.transform.position, qua);
                spawnedTrash.GetComponentInChildren<Throwable>().throwableData = NextRandomTrashType();
                spawnedTrash.GetComponentInChildren<Throwable>().LoadData();
                trashNumber++;
            }

        }else if(Random.value > chanceForPileDrop)
        {
            //not the drop
            Quaternion qua = Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
            GameObject spawnedTrash = Instantiate(throwablePrefab, location, qua);
            spawnedTrash.GetComponentInChildren<Throwable>().throwableData = trashType;
            spawnedTrash.GetComponentInChildren<Throwable>().LoadData();
            trashNumber++;
        }
        else
        {
            foreach (Transform child in pileDrop.transform)
            {
                Quaternion qua = Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
                GameObject spawnedTrash = Instantiate(throwablePrefab, location + child.transform.position, qua);
                spawnedTrash.GetComponentInChildren<Throwable>().throwableData = NextRandomTrashType();
                spawnedTrash.GetComponentInChildren<Throwable>().LoadData();
                trashNumber++;
            }
        }
    }
}
