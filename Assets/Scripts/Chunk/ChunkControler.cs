using UnityEngine;

public class ChunkControler : MonoBehaviour
{
    public Vector2Int location;
    public World world;
    public Transform player;
    public Chunk chunk
    {
        get
        {
            return world.chunks[location];
        }
    }
}
