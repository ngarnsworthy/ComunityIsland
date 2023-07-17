using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool lockCursor = false;

    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
        if (lockCursor)
            Cursor.lockState = CursorLockMode.None;
    }

    public void Hide()
    {
        transform.gameObject.SetActive(false);
        if (lockCursor)
            Cursor.lockState = CursorLockMode.Locked;
    }
}
