using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : SingletonMonoBehaviour<CameraShaker>
{
    [SerializeField] private float intensity = 2;
    [SerializeField] private float time = 1;

    CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        noise = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake()
    {
        noise.m_AmplitudeGain = intensity;
        StartCoroutine(ShakeTimer());
    }

    private IEnumerator ShakeTimer()
    {
        yield return new WaitForSeconds(time);
        noise.m_AmplitudeGain = 0;
    }
}
