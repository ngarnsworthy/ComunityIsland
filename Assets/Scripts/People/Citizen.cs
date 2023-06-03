using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Citizen : MonoBehaviour
{
    public float speed;
    public CitizenAI AI;
    public Pathfinder.AStarPath pathfinder;
    Rigidbody rigidbody;

    private void Awake()
    {
        AI = new CitizenAI();
        AI.citizen = this;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        AI.Update();
        rigidbody.MovePosition(pathfinder.NextPosition(Time.deltaTime* speed));
    }
}
