using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public CinemachineVirtualCamera virtualCamera;

    private CinemachineBasicMultiChannelPerlin virtualCameraNoise; 
   
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float frequencey, float amplitude, float duration)
    {
        StopAllCoroutines();
        virtualCameraNoise.m_FrequencyGain = frequencey;
        virtualCameraNoise.m_AmplitudeGain = amplitude;
        StartCoroutine(DelayedShake(duration));
    }

    private IEnumerator DelayedShake(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        virtualCameraNoise.m_FrequencyGain = 0;
        virtualCameraNoise.m_AmplitudeGain = 0;
    }
}
