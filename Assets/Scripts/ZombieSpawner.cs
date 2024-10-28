using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZombieSpawner : MonoBehaviour
{
    // Khai bao thuoc tinh 
    public Transform player; // lay vi cua player 
    public Vector3 spawnLeft; // lay vi tri ben trai nguoi choi de spawn zombie
    public Vector3 spawnRight; // lay vi tri ben phai nguoi choi de spawn
    public Vector3[] spawnPositions;

    public float spawnTime = 5f;
    public float spawnTimmer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        spawnLeft = new Vector3(player.position.x - 10f, player.position.y);
        spawnRight = new Vector3(player.position.x + 10f, player.position.y);
        Vector3[] spawnPositions = new Vector3[] { spawnLeft, spawnRight };

        transform.position = player.position;

        if (spawnTimmer >= spawnTime)
        {
            SpawnZombie(spawnPositions);
            spawnTimmer = 0f;
        }

        spawnTimmer += Time.deltaTime;
    } 

    public void SpawnZombie(Vector3[] spawnPositions)
    {
        int randomIndex = Random.Range(0, spawnPositions.Length);

        Vector3 spawnPos = spawnPositions[randomIndex];

        GameObject zombie = ZombiePool.instance.GetPooledObject();

        if (zombie != null)
        {
            zombie.transform.position = spawnPos;
            zombie.SetActive(true);
        }
    }
}
