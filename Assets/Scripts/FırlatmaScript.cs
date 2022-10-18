using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FırlatmaScript : MonoBehaviour
{
    public static FırlatmaScript instance;
    public GameObject throwObject, arrow, lastThrowObject;
    [SerializeField] private GameObject[] jettisonableObject,finalBalls;
    [SerializeField] private GameObject finisLine1;
    public float throwForce;
    Vector3 firstPos, lastPos;
    public bool isThrowable=true,checkSleep,finalPart;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        throwObject = GameObject.FindGameObjectWithTag("throwableBall");
        Physics.gravity = new Vector3(0, 0, 0);

    }

    void Update()
    {
        IsThrowable();
        ChooseThrowableObject();

        if(throwObject != null)
        {
            Vector3 pos = throwObject.transform.position;
            pos.y -= 1.152641f;
            transform.position = pos;
            transform.rotation = throwObject.transform.rotation;
        }
        if (finalPart)
        {
            FinalBallsCheck();
        }
    }

    void IsThrowable()
    {
        if (!finalPart)
        {
            Rigidbody rb = throwObject.GetComponent<Rigidbody>();

            if (rb.velocity.magnitude < 1f)
            {
                Physics.gravity = new Vector3(0, 0, 0);
                isThrowable = true;
                arrow.SetActive(true);
            }
            else
            {
                Physics.gravity = new Vector3(0, -90, 0);
                isThrowable = false;
                arrow.SetActive(false);
            }
            if (lastThrowObject != null)
            {
                if (lastThrowObject.CompareTag("throwableBall"))  
                {
                    StartCoroutine(ChangeLastThrowsTag());
                }
            }
        }

    } 

    void ChooseThrowableObject()
    {
        jettisonableObject = GameObject.FindGameObjectsWithTag("throwableBall"); 

        if (jettisonableObject.Length != 0)
        {
            if (!finalPart)
            {
                for (int i = 0; i < jettisonableObject.Length; i++)
                {
                   if (throwObject.CompareTag("throwableBall"))  
                    {
                       if (Vector3.Distance(throwObject.transform.position, finisLine1.transform.position) > Vector3.Distance(jettisonableObject[i].transform.position, finisLine1.transform.position)) //Bu karşılaştırmayı sonradan finishe yakınlık olarak değiştir
                       {
                           throwObject = jettisonableObject[i];
                       }
                   }
                   else
                   {
                       throwObject = jettisonableObject[0];
                       if (Vector3.Distance(throwObject.transform.position, finisLine1.transform.position) > Vector3.Distance(jettisonableObject[i].transform.position, finisLine1.transform.position)) //Bu karşılaştırmayı sonradan finishe yakınlık olarak değiştir
                       {
                           throwObject = jettisonableObject[i];
                       }

                    }
                }
            }

            if (checkSleep)
            {
               StartCoroutine(SetUnVisible(jettisonableObject));
            }
        }
        else
        {
            if (!finalPart)
            {
                GameManager.instance.LevelFailed();
            }
        }
    }

    void FinalBallsCheck()
    {
        finalBalls = GameObject.FindGameObjectsWithTag("finalBall");
        float f=0;
        for(int i = 0; i < finalBalls.Length; i++)
        {
            Rigidbody rb = finalBalls[i].GetComponent<Rigidbody>();
            f += rb.velocity.magnitude;
        }
        if(f/finalBalls.Length < 2)
        {
            GameManager.instance.LevelCompleted();
        }
    }

    IEnumerator SetUnVisible(GameObject[] throwableObj)
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < throwableObj.Length; i++)
        {
            Rigidbody rb = throwableObj[i].GetComponent<Rigidbody>();
            if (rb.velocity.magnitude<1.5f)
            {
                if (throwableObj[i] != throwObject)
                {
                    throwableObj[i].tag = "Untagged";
                }
            }
        }
    }
    IEnumerator ChangeLastThrowsTag()
    {
        yield return new WaitForSeconds(.2f);
        Rigidbody rb1 = lastThrowObject.GetComponent<Rigidbody>();
        if (rb1.velocity.magnitude < 2f)
        {
            if (lastThrowObject != null)
            {
                lastThrowObject.tag = "ball";
            }
        }
    }
}
