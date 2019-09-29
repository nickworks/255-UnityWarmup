using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens {

    public class Control : MonoBehaviour
    {

        private Rigidbody rigid;
        public float thrust = 5;
        public float rotate = 5;
        public ParticleSystem Flames;
        public ParticleSystem Smoke;
        public ParticleSystem Explosion;
        public GameObject exploder;
        private float threshold = 35;
        private bool isCrashing = false;
        private ExplosionDetector exp;

        // Start is called before the first frame update
        void Start(){
            rigid = GetComponent<Rigidbody>();
            exp = exploder.GetComponent<ExplosionDetector>();
        }

        // Update is called once per frame
        void Update(){
            if (exp.hasCollided)
                StartCoroutine("Crash");

          

            if (Input.GetKey("space")){
                rigid.AddRelativeForce(Vector3.forward * Time.deltaTime  * thrust);
                if (Input.GetKey(KeyCode.A))
                {
                    rigid.AddTorque(transform.up * -rotate);
                    //transform.RotateAroundLocal(Vector3.forward, rotate);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    rigid.AddTorque(transform.up * rotate);
                    //transform.RotateAroundLocal(Vector3.forward, -rotate);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    rigid.AddTorque(transform.up * .6f * -rotate);
                    //transform.RotateAroundLocal(Vector3.forward, rotate);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    rigid.AddTorque(transform.up * .6f *  rotate);
                    //transform.RotateAroundLocal(Vector3.forward, -rotate);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Flames.Play();
                Smoke.Play();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Flames.Stop();
                Smoke.Stop();
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if ((transform.eulerAngles.x < (270 - threshold) || transform.eulerAngles.x > (270 + threshold)) && rigid.velocity.magnitude > 4)
            {
                if (!isCrashing)
                    StartCoroutine("Crash");
            }
            else if (rigid.velocity.magnitude > 7) {
                if (!isCrashing)
                    StartCoroutine("Crash");

            }
        }


        IEnumerator Crash()
        {
            isCrashing = true;
            Explosion.Play();
            yield return new WaitForSeconds(.2f);
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

    }


}
