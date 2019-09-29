using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takens
{

    public class ExplosionDetector : MonoBehaviour
    {

        public bool hasCollided = false;
        private void OnTriggerEnter(Collider other)
        {
            hasCollided = true;
        }
    }
}
