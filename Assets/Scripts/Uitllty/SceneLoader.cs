using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private Canvas LoadingCanvas;
    private void Start()
    {
        if (Instance == null)
            Instance = this;
        //else
        //{
        //    Destroy(Instance.gameObject);
        //    Instance = this;
        //}
    }

    public void LoadScene(int value)
    {
        LoadingCanvas.enabled = true;
        SceneManager.LoadSceneAsync(value);
    }
    public void LoadScene(string value)
    {
        LoadingCanvas.enabled = true;
        SceneManager.LoadSceneAsync(value);
    }
}
