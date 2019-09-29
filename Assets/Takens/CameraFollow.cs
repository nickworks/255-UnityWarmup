using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takens{


    public class CameraFollow : MonoBehaviour
    {
        public GameObject Rocket;
        public float YOffset = 6.1f;
        public float ZOffset = -19.49f;
        // Update is called once per frame
        void Update()
        {
            if(Rocket != null)
            transform.position = new Vector3(Rocket.transform.position.x, Rocket.transform.position.y + YOffset, Rocket.transform.position.z + ZOffset);
        }
    }
}
