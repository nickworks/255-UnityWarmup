using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DannyMyles
{
    public class ParticleSpawner : MonoBehaviour
    {
        public GameObject prefab;

        float countdownToSpawn = 0; // seconds left before spawning
        
        void Start()
        {

        }

        
        void Update()
        {
            countdownToSpawn -= Time.deltaTime;
            if (countdownToSpawn <= 0)
            {
                GameObject newObj = Instantiate(prefab, transform.position, Quaternion.identity);
                newObj.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
                countdownToSpawn = Random.Range(.5f, 1f);
            }
        }
    }
}
