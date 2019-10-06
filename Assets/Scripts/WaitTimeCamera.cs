using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTimeCamera : Singleton<WaitTimeCamera>
{

    CinemachineVirtualCamera waitTimeCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCameraPriority(int priority)
    {
        GetComponent<CinemachineVirtualCamera>().Priority = priority;
    }
}
