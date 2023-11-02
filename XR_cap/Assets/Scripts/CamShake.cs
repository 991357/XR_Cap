using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    CinemachineVirtualCamera Cam;
    public float ShakeIntensity;
    public float ShakeTime;

    float Timer;
    CinemachineBasicMultiChannelPerlin _cbmcp;

    public bool IsShake;

    private void Awake()
    {
        Cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        StopShake();
    }

    void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = Cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;

        Timer = 0;
    }

    void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = Cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;

        Timer = ShakeTime;
    }

    private void Update()
    {
        if(IsShake)
        {
            ShakeCamera();
        }

        if(Timer > 0)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                StopShake();
            }
        }
    }
}
