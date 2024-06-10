using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    public float spawnRate = 1f;
    public float moveSpeed = 2f;
    public float xRange = 7.5f;

    private void Start()
    {
        InvokeRepeating("SpawnFood", 2f, spawnRate);
    }

    void Update()
    {
        MoveDispenser();
    }

    void MoveDispenser()
    {
        float newX = Mathf.PingPong(Time.time * moveSpeed, xRange * 2) - xRange;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void SpawnFood()
    {
        int index = Random.Range(0, foodPrefabs.Length);
        Instantiate(foodPrefabs[index], transform.position, Quaternion.identity);
    }
}