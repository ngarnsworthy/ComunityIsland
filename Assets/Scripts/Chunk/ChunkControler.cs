using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkControler : MonoBehaviour
{
    public Vector2Int location;
    public World world;
    public Transform player;

    private void Update()
    {
        //Only check every 10 frames
        if (Time.frameCount % 10 == 0)
        {
            if (Mathf.Abs(transform.position.x - player.position.x) > world.loadingDistance|| Mathf.Abs(transform.position.y - player.position.y) > world.loadingDistance)
            {
                Destroy(this.gameObject);
                world.chunks[location].gameObject = null;
            }
        }   
    }
}
