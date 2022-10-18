using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCreator : MonoBehaviour
{
    [SerializeField] private GameObject topPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("throwableBall") || collision.gameObject.CompareTag("ball"))
        {
            if (this.gameObject.CompareTag("ballCreator"))
            {
                TopUret(collision.gameObject);
                this.gameObject.tag = "throwableBall";
            }

        }
    }

    public void TopUret(GameObject carpanTop)
    {
        for(int i = 0; i < GameManager.instance.cloneCount; i++)
        {
            GameObject top = Instantiate(topPrefab,transform.position,carpanTop.transform.rotation);
            top.transform.eulerAngles = Vector3.zero;
            top.GetComponent<Rigidbody>().AddExplosionForce(200f, gameObject.transform.position, 2f);
            top.GetComponent<Rigidbody>().AddForce(transform.forward * 2350, ForceMode.Force);
            top.tag = "throwableBall";

            GameManager.instance.TotalBallCount();
            
        }
    }
}
