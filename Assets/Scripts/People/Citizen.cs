using UnityEngine;
using System.Collections;

public class Citizen : MonoBehaviour
{
    public CitizenAI AI;

    private void Awake()
    {
        AI = new CitizenAI();
    }

    void Update()
    {

    }
}
