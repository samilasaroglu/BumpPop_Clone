using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody rb;


    private void Awake()
    {
        rb =gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("destroyer"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            gameObject.tag = "ball";
            GameManager.instance.GainMoney();
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ring"))
        {
            if (collision.gameObject.transform.parent.transform.parent.transform.parent.GetChild(0).GetComponent<ChainBrokerScript>().score <= 0)
            {
                collision.gameObject.transform.parent.transform.parent.transform.parent.GetChild(0).GetComponent<ChainBrokerScript>().BreakChain();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish1"))
        {
            gameObject.tag = "finalBall";
            AfterFinish1();
            CameraScript.instance.finalPart = true;
            GameManager.instance.finalPart = true;
        }
        if (other.CompareTag("beforeFinish"))
        {
            if (gameObject.CompareTag("finalBall"))
            {
                Destroy(gameObject);
            }
        }
    }

    void AfterFinish1()
    {
        rb.drag = 0;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        Physics.gravity = Vector3.down*90;
    }
}
