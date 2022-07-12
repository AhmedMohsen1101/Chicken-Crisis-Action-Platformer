using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Quality
{
    Low = 0,
    Medium = 1,
    High = 2,
}
public class QualityHandler : MonoBehaviour
{
    public List<QualityToggle> toggles = new List<QualityToggle>();

    public void Select(QualityToggle toggle)
    {
        foreach (QualityToggle t in toggles)
        {
            t.toggle.isOn = false;

            if(toggle == t)
            {
                t.toggle.isOn = true;
                ChangeQuality(t.quality);
            }
        }
    }
    public void Resgister(QualityToggle qualityToggle)
    {
        toggles.Add(qualityToggle);
    }
    
    private void ChangeQuality(Quality selectedQuality)
    {
        QualitySettings.SetQualityLevel((int)selectedQuality);
    }
}
