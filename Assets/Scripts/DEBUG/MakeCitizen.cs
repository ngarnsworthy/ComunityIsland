using UnityEngine;

public class MakeCitizen : MonoBehaviour
{
    public GameObject citizen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(citizen);
        }
    }
}
