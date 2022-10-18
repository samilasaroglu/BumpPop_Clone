using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    [SerializeField] private GameObject[] followableBalls;
    private GameObject finishLine2,follow;
    public bool finalPart;
    private CinemachineVirtualCamera vCam;
    [SerializeField] private GameObject thrower;
    private bool isCameraSet;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        finishLine2 = GameObject.FindGameObjectWithTag("Finish2");
        vCam = GameObject.FindGameObjectWithTag("vCam").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        FindFollowBall();
        if (follow != null)
        {
            transform.position = follow.transform.position;
            transform.rotation = follow.transform.rotation;
        }
    }
    void FindFollowBall()
    {
        if (!finalPart)
        {
            followableBalls = GameObject.FindGameObjectsWithTag("throwableBall");
            if (followableBalls.Length != 0)
            {
                follow = followableBalls[0];

                for (int i = 0; i < followableBalls.Length; i++)
                {
                    if (Vector3.Distance(follow.transform.position, finishLine2.transform.position) > Vector3.Distance(followableBalls[i].transform.position, finishLine2.transform.position))
                    {
                        follow = followableBalls[i];
                    }
                }
            }
        }
        else
        {
            if (!isCameraSet)
            {
                CameraSet();
            }
            followableBalls = GameObject.FindGameObjectsWithTag("finalBall");
            if (followableBalls.Length != 0)
            {
                follow = followableBalls[0];

                for (int i = 0; i < followableBalls.Length; i++)
                {
                    if (Vector3.Distance(follow.transform.position, finishLine2.transform.position) > Vector3.Distance(followableBalls[i].transform.position, finishLine2.transform.position))
                    {
                        follow = followableBalls[i];
                    }
                }
            }
            else
            {
                GameManager.instance.LevelCompleted();
            }
        }


    }

    void CameraSet()
    {
        FırlatmaScript.instance.finalPart = true;
        CinemachineTransposer transposer = vCam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        transposer.m_FollowOffset = new Vector3(0, 32, -49);
        transposer.m_XDamping = 5;
        transposer.m_YDamping = 5;
        transposer.m_ZDamping = 5;
        CinemachineComposer composer = vCam.GetCinemachineComponent<CinemachineComposer>();
        composer.m_HorizontalDamping = 5;
        composer.m_VerticalDamping = 5;
        isCameraSet = true;
    }
}
