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
        AI = new CitizenAI(this);
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        TerrainGen.terrainGen.meshGenerater.AddChunks(TerrainGen.world.CreateChunks(transform.position));
        AI.Update();
        if (pathfinder != null)
        {
            rigidbody.MovePosition(pathfinder.NextPosition(Time.deltaTime * speed));
        }
    }
}
