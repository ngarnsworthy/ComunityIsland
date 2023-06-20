using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), Serializable]
public class Citizen : MonoBehaviour
{
    public float speed;
    public CitizenAI AI;
    public Pathfinder.AStarPath pathfinder;
    [NonSerialized] Rigidbody rigidbody;

    private void Awake()
    {
        AI = new CitizenAI();
        AI.citizen = this;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        AI.Update();
        if (pathfinder != null)
        {
            rigidbody.MovePosition(pathfinder.NextPosition(Time.deltaTime * speed));
        }
    }
}
