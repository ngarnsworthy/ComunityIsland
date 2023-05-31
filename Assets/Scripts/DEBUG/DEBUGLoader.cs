using UnityEngine;
using System.Collections;

public class DEBUGLoader : MonoBehaviour
{
    public DEBUGItem[] DEBUGItems;

    // Update is called once per frame
    void Update()
    {
        foreach(DEBUGItem item in DEBUGItems)
        {
            if (Input.GetKeyDown((KeyCode)item.key))
            {
                item.RunDebug();
            }
        }
    }
}
