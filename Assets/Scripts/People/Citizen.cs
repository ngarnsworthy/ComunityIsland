using System;
using UnityEngine;
using Aoiti.Pathfinding;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody)), Serializable]
public class Citizen : MonoBehaviour
{
    public CitizenAI AI;
    public Pathfinder<SerializableVector2Int> pathfinder;
    public List<Vector3> path;
    [NonSerialized] Rigidbody rigidbody;

    private void Awake()
    {
        AI = new CitizenAI(this);
        rigidbody = GetComponent<Rigidbody>();
        pathfinder = new Pathfinder<SerializableVector2Int>((p1, p2) => { return Vector2Int.Distance(p1, p2); }, (SerializableVector2Int pos) =>
        {
            Dictionary<SerializableVector2Int, float> neighbours = new Dictionary<SerializableVector2Int, float>();
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    
                    if (x == 0 && y == 0) continue;
                    SerializableVector2Int dir = new SerializableVector2Int(x, y);
                    World.ChunkLocation chunkLocation = new World.ChunkLocation(TerrainGen.world.chunks[new SerializableVector2Int(0, 0)], x+pos.x, y+pos.y);
                    if (TerrainGen.world.GetWalkable(new SerializableVector2Int(pos.x+x, pos.y+y)));
                    {
                        neighbours.Add(new SerializableVector2Int(x + pos.x, y + pos.y), Vector3.Distance(TerrainGen.world.Vector3FromChunkLocation(chunkLocation), new Vector3(pos.x, TerrainGen.world[new SerializableVector2Int(pos.x,pos.y)], pos.y)));
                    }
                    
                }
            }
            return neighbours;
        }, 500);
    }

    [HideInInspector]
    public float segmentPrecentMoved;
    public float movementSpeed;
    [HideInInspector]
    public int currentPointIndex;
    [HideInInspector]
    public float trackLegnth;

    void Update()
    {
        TerrainGen.terrainGen.meshGenerater.AddChunks(TerrainGen.world.CreateChunks(transform.position, 2));
        AI.Update();
        if (path != null && path.Count>1 && currentPointIndex < path.Count-2)
        {
            
                segmentPrecentMoved += movementSpeed / trackLegnth / Vector3.Distance(path[currentPointIndex], path[currentPointIndex + 1]);

                while (segmentPrecentMoved >= 1)
                {
                    segmentPrecentMoved--;
                    currentPointIndex++;
                }

                rigidbody.MovePosition( Vector3.Lerp(path[currentPointIndex], path[currentPointIndex + 1], segmentPrecentMoved));
            
        }
    }
}
