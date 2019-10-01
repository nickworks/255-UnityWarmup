using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreuPickupController : MonoBehaviour
{

    public float spinSpeed = 1;
    public float rotationSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed, spinSpeed, rotationSpeed) * Time.deltaTime);
    }
}
