using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject bombPrefab;
    public float respawnTime = 1.0f;

    public float bombSpawnRate = 0.2f;

    public float initialSpeed = 10.0f;
    public float currentSpeed;
    public float speedUpRate = 0.2f;

    private Vector2 screenBounds;

    public bool gameActive = true;

    public static FishSpawner instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    public void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        currentSpeed = initialSpeed;
        StartCoroutine(fishLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnFish()
    {
        GameObject a;

        float randomNumber = Random.Range(0.0f, 1.0f);

        currentSpeed += speedUpRate;
        
        if (randomNumber > bombSpawnRate)
        {
            Debug.Log("Spawning fish");
            a = Instantiate(fishPrefab) as GameObject;
            FishScript.instance.speed = currentSpeed;
        } else
        {
            Debug.Log("Spawning bomb");
            a = Instantiate(bombPrefab) as GameObject;
            BombScript.instance.speed = currentSpeed;
        }
        
        a.transform.position = new Vector2(Random.Range(-4.5f, 4.5f), screenBounds.y * 2);
    }

    IEnumerator fishLoop()
    {
        while (gameActive)
        {
            spawnFish();
            yield return new WaitForSeconds(respawnTime);
        }
    }

    


}
