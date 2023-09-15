using TMPro;
using UnityEngine;

public class TopBarResizer : MonoBehaviour
{
    Vector2 firstLocation;
    public RectTransform[] elements;
    public float spacing;
    public int updateInterval = 5;
    public bool autoUpdate = true;

    [SerializeField]
    bool text = false;
    private void Awake()
    {
        firstLocation = elements[0].anchoredPosition;
        if (text)
            foreach (var item in elements)
            {
                item.sizeDelta = new Vector2(1000, item.rect.height);
            }
    }
    void Update()
    {
        if (autoUpdate && Time.frameCount % updateInterval == 0)
        {
            Recalculate();
        }
    }
    public void Recalculate()
    {
        float sum = 0;
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].anchoredPosition = new Vector2(sum, firstLocation.y);
            if (text)
            {
                TextMeshProUGUI text = elements[i].GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    sum += spacing + text.renderedWidth;
                }
            }
            else
            {
                sum += spacing + elements[i].rect.width;
            }
        }
    }
}
