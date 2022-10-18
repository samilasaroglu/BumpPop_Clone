using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChainBrokerScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmpScore;
    public int score;
    [SerializeField] private GameObject[] brokenRing;
    private Animator anim;

    private void Awake()
    {
        tmpScore.text = "" + score;
        anim = gameObject.transform.parent.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("throwableBall") || other.CompareTag("ball") || other.CompareTag("finalBall")) // çarpacak olan topların tagı ?
        {
            if(score >= 1)
            {
                score--;
                tmpScore.text = "" + score;
            }
        }
    }

    public void BreakChain()
    {
        if(score < 1)
        {
            StartCoroutine(BreakRingPiece());
        }
    }
    IEnumerator BreakRingPiece()
    {
        for(int i = 0; i < brokenRing.Length; i++)
        {
            brokenRing[i].GetComponent<Rigidbody>().isKinematic = true;
            brokenRing[i].GetComponent<MeshCollider>().enabled = false;
        }
        anim.SetBool("break", true);
        yield return new WaitForSeconds(.4f);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
