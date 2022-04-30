using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;
    private Text tooltipText;
    private RectTransform bgRectTransform;
    [SerializeField]
    //private Camera camera;
    private void Awake()
    {
        instance = this;
        bgRectTransform = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<Text>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        //Vector2 localPoint;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Mouse.current.position.ReadValue(), camera, out localPoint);
        //transform.localPosition = localPoint;
    }
    public void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float textPaddingSize = 1f;
        Vector2 bgSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2);
        bgRectTransform.sizeDelta = bgSize;
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    //public static void ShowTooltip_Static(string tooltipString)
    //{
    //    instance.ShowTooltip(tooltipString);
    //}
    //public static void HideTooltip_Static()
    //{
    //    instance.HideTooltip();
    //}
}
