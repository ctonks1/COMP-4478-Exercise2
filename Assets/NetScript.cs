using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class NetScript : MonoBehaviour
{

    public Vector2 speed = new Vector2(50, 50);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;


    void Start()
    {
        Debug.Log("Screen Width : " + Screen.width);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");


        movement = new Vector2(
          speed.x * inputX / 5,
          0);

    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        transform.position = viewPos;
    }


    void FixedUpdate()
    {

        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.velocity = movement;

    }

    private void OnTriggerEnter2D(Collider2D item)
    {
        Debug.Log("Collision detected");
        Debug.Log("Object that collided with me: " + item.gameObject.name);
        if (item.gameObject.name.Contains("Fish"))
        {
            Destroy(item.gameObject);
            ScoreManager.instance.AddPoint();
        } else if (item.gameObject.name.Contains("Bomb"))
        {
            FishSpawner.instance.gameActive = false;
            GameObject[] fish;

            fish = GameObject.FindGameObjectsWithTag("Fish");

            foreach (GameObject fishObject in fish)
            {
                Destroy(fishObject);
            }
            Destroy(item.gameObject);
            ScoreManager.instance.showGameOver();
        }
        
    }
}
