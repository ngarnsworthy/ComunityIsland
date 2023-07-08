using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopBarResizer : MonoBehaviour
{
    Vector2 firstLocation;
    public RectTransform[] elements;
    public float spacing;
    public int updateInterval = 5;
    public bool autoUpdate = true;
    private void Awake()
    {
        firstLocation = elements[0].anchoredPosition;
    }
    void Update()
    {
        if (Time.frameCount % updateInterval == 0&&autoUpdate)
        {
            Recalculate();
        }
    }
    public void Recalculate()
    {
        float sum = firstLocation.x;
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].anchoredPosition = new Vector2(sum, firstLocation.y);
            sum += spacing + elements[i].rect.width;
        }
    }
}
