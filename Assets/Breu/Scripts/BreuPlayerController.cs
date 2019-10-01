using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreuPlayerController : MonoBehaviour
{
    private Rigidbody RB;
    public float movementSpeed = 1;
    private int score;
    public Text scoreText;
    public int collectableNumber;
    public Text WinText;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        score = 0;
        WinText.text = "";
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    //update before physics calculations
    void FixedUpdate()
    {
        float moveHori = Input.GetAxis("Horizontal");//float between -1 & 1 based on player input
        float moveVert = Input.GetAxis("Vertical") * -1;

        Vector3 movement = new Vector3(moveVert, 0f, moveHori);//converts vector 2 form inputs into vector 3
        movement *= movementSpeed;//changes vector3 based on movementSpeed variable

        RB.AddForce (movement);

        if (transform.position.y < -10)
        {
            transform.position = startPosition;
            RB.velocity = Vector3.zero;
            RB.angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider pickUp)
    {
        if (pickUp.gameObject.CompareTag("pick up"))
        {
            score++;
            Destroy(pickUp.gameObject);
        }
        if (score >= collectableNumber)
        {
            WinText.text = "You win";
        }
    }
}
