using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class QualityToggle : MonoBehaviour, IPointerDownHandler
{
    public Quality quality;
    public QualityHandler qualityHandler;

    [HideInInspector] public Toggle toggle;

    private void Start()
    {
        qualityHandler.Resgister(this);
    }
    private void OnEnable()
    {
        if (toggle == null)
            toggle = GetComponent<Toggle>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        qualityHandler.Select(this);
    }
}
