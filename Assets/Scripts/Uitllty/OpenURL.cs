using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenUrl(string targetURL)
    {
        Application.OpenURL(targetURL);  
    }
}
