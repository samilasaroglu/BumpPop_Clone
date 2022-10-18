using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInputScript  : MonoBehaviour , IDragHandler,IBeginDragHandler,IEndDragHandler
{
    Vector3 firstPos, lastPos;
    private GameObject[] MenuUIObjects;
    [SerializeField] private GameObject ReplayButton;
    private bool menuClose;


    public void OnBeginDrag(PointerEventData data)
    {
        if (FırlatmaScript.instance.isThrowable)
        {
            FırlatmaScript.instance.checkSleep = false;
            firstPos = Camera.main.ScreenToViewportPoint(data.position);
        }
        MenuUIObjects = GameObject.FindGameObjectsWithTag("Menu");
    }
    public void OnDrag(PointerEventData data)
    {
        if (FırlatmaScript.instance.isThrowable)
        {
            lastPos = Camera.main.ScreenToViewportPoint(data.position);

            float angle = (lastPos.x - firstPos.x) * Mathf.Rad2Deg;

            FırlatmaScript.instance.throwObject.transform.rotation = Quaternion.Euler(FırlatmaScript.instance.throwObject.transform.eulerAngles.x, angle, 0);

            FırlatmaScript.instance.arrow.SetActive(true);
            if (!menuClose)
            {
                for(int i =0;i<MenuUIObjects.Length; i++)
                {
                    MenuUIObjects[i].SetActive(false);
                }
                ReplayButton.SetActive(true);
                menuClose = true;
            }
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (FırlatmaScript.instance.isThrowable)
        {
            Rigidbody rb = FırlatmaScript.instance.throwObject.GetComponent<Rigidbody>();
            rb.AddForce(FırlatmaScript.instance.throwObject.transform.forward * FırlatmaScript.instance.throwForce, ForceMode.Force);
            FırlatmaScript.instance.lastThrowObject = FırlatmaScript.instance.throwObject;
            FırlatmaScript.instance.isThrowable = false;
            FırlatmaScript.instance.arrow.SetActive(false);
            FırlatmaScript.instance.checkSleep = true;
        }
    }


}
