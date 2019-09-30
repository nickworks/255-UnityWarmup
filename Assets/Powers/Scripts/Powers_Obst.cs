using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_Obst : MonoBehaviour
{

    public enum Direction
    {
        Forward, Backward, Left, Right
    }

    public Direction direction;
    public float speed;
    public Powers_Game gameManager;

    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {  
        //set unique position based on direction
        if (direction == Direction.Forward) transform.position = new Vector3(Mathf.RoundToInt((Random.value * 10) - 5), 1.5f, -20f);
        else if (direction == Direction.Backward) transform.position = new Vector3(Mathf.RoundToInt((Random.value * 10) - 5), 1.5f, 20f);
        else if (direction == Direction.Right) transform.position = new Vector3(-20f, 1.5f, Mathf.RoundToInt((Random.value * 10) - 5));
        else if (direction == Direction.Left) transform.position = new Vector3(20f, 1.5f, Mathf.RoundToInt((Random.value * 10) - 5));

        //randomize speed
        speed *= ((Random.value / 0.5f) + 0.75f);

        //set movement vector based on chosen direction
        if (direction == Direction.Forward) movement = new Vector3(0, 0, speed);
        else if (direction == Direction.Backward) movement = new Vector3(0, 0, -speed) ;
        else if (direction == Direction.Right) movement = new Vector3(speed, 0, 0);
        else if (direction == Direction.Left) movement = new Vector3(-speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movement * Time.deltaTime;

        if (direction == Direction.Forward && transform.position.z >= 20f) Destroy(gameObject);
        else if (direction == Direction.Backward && transform.position.z <= -20f) Destroy(gameObject);
        else if (direction == Direction.Right && transform.position.x >= 20f) Destroy(gameObject);
        else if (direction == Direction.Left && transform.position.x <= -20f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameManager.playerHealth -= 20;
            gameManager.healTimer = 240;
            Destroy(gameObject);
        }
    }
}
