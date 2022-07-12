using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FrameRate targetFrameRate;
    [SerializeField] private CursorLockMode mouseLockMode;
    [SerializeField] private float timeScale;
    private void Awake()
    {
        Application.targetFrameRate = (int)targetFrameRate;
        Time.timeScale = timeScale;
        Cursor.lockState = mouseLockMode;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
public enum FrameRate
{
    FPS_30 = 30,
    FPS_60 = 60,
    FPS_90 = 90,
    FPS_144 = 144,
    FPS_250 = 250,
    UnLimited = 9999,
}