using UnityEngine;

public class Menu : MonoBehaviour
{
    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
    }

    public void Hide()
    {
        transform.gameObject.SetActive(false);
    }
}
