using UnityEngine;

public class MakeCitizen : MonoBehaviour
{
    public GameObject citizen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(citizen);
        }
    }
}
