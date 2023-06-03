using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinder
{
    public class AStarPoint
    {
        public AStarPath path;
        public Vector3 position;
        public AStarPoint parent;

        World.ChunkLocation chunkLocation;

        public AStarPoint(AStarPath path, Vector3 position, AStarPoint parent)
        {
            this.path = path;

            this.position = position;

            chunkLocation = World.ChunkLocationFromPoint(position);

            this.parent = parent;

            if (parent != null)
            {
                gCost = parent.gCost + Vector3.Distance(parent.position, position);
            }
            else
            {
                gCost = 0;
            }
            hCost = Vector3.Distance(position, path.end);
        }

        public float fCost
        {
            get
            {
                return hCost + gCost;
            }
        }

        public float gCost;

        public float hCost;

        public bool walkable
        {
            get
            {
                return chunkLocation.chunk.walkable[chunkLocation.x, chunkLocation.y];
            }
        }

        public AStarPoint[] neighbours
        {
            get
            {
                if(pNeighbours == null)
                {
                    AStarPoint[] newNeighbours = new AStarPoint[4];

                    World.ChunkLocation neighboursChunkLocation = World.ChunkLocationFromPoint(new Vector2Int(chunkLocation.x+1, chunkLocation.y));
                    newNeighbours[0] = new AStarPoint(path, TerrainGen.world.Vector3FromChunkLocation(neighboursChunkLocation), this);
                    neighboursChunkLocation = World.ChunkLocationFromPoint(new Vector2Int(chunkLocation.x - 1, chunkLocation.y));
                    newNeighbours[1] = new AStarPoint(path, TerrainGen.world.Vector3FromChunkLocation(neighboursChunkLocation), this);
                    neighboursChunkLocation = World.ChunkLocationFromPoint(new Vector2Int(chunkLocation.x, chunkLocation.y + 1));
                    newNeighbours[2] = new AStarPoint(path, TerrainGen.world.Vector3FromChunkLocation(neighboursChunkLocation), this);
                    neighboursChunkLocation = World.ChunkLocationFromPoint(new Vector2Int(chunkLocation.x, chunkLocation.y - 1));
                    newNeighbours[3] = new AStarPoint(path, TerrainGen.world.Vector3FromChunkLocation(neighboursChunkLocation), this);

                    for (int i = 0; i < newNeighbours.Length; i++)
                    {
                        AStarPoint item = newNeighbours[i];
                        if (path.openSet.Contains(item))
                        {
                            newNeighbours[i] = path.openSet.Find((e)=>item==e);
                        }
                        else if (path.closedSet.Contains(item))
                        {
                            newNeighbours[i] = null;
                        }
                    }
                    pNeighbours = newNeighbours;
                }
                return pNeighbours;
            }
        }

        public AStarPoint[] pNeighbours;

        public void AddNeighbours()
        {
            foreach (AStarPoint neighbour in neighbours)
            {
                if (!neighbour.walkable) continue;

                float newMovementCostToNeighbour = gCost + Vector3.Distance(position, neighbour.position);
                if (newMovementCostToNeighbour < neighbour.gCost || !path.openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.parent = this;

                    if (!path.openSet.Contains(neighbour))
                        path.openSet.Add(neighbour);
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is AStarPoint point &&
                   position.Equals(point.position);
        }

        public override int GetHashCode()
        {
            return 1206833562 + position.GetHashCode();
        }
    }
    public class AStarPath
    {
        public Vector3 start;
        public Vector3 end;
        public AStarPoint startPoint;
        public AStarPoint endPoint;
        public List<AStarPoint> openSet = new List<AStarPoint>();
        public HashSet<AStarPoint> closedSet = new HashSet<AStarPoint>();

        public List<AStarPoint> path
        {
            get
            {
                if (pPath == null)
                {
                    startPoint = new AStarPoint(this, start, null);
                    openSet.Add(startPoint);

                    while (openSet.Count > 0)
                    {
                        AStarPoint currentNode = openSet[0];
                        for (int i = 1; i < openSet.Count; i++)
                        {
                            if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                            {
                                currentNode = openSet[i];
                            }
                        }

                        openSet.Remove(currentNode);
                        closedSet.Add(currentNode);

                        if (currentNode == endPoint)
                        {
                            pPath = ReconstructPath(startPoint, endPoint);
                            trackLegnth = 0;
                            for (int i = 0; i < pPath.Count-1; i++)
                            {
                                AStarPoint point = pPath[i];
                                AStarPoint nextPoint = pPath[i+1];
                                trackLegnth += Vector3.Distance(point.position, nextPoint.position);
                            }
                            currentPointIndex = 0;
                            segmentPrecentMoved = 0;
                        }

                        currentNode.AddNeighbours();
                    }
                }
                return pPath;
            }
        }
        private List<AStarPoint> pPath;
        public void ClearPath() { pPath = null; }

        public static List<AStarPoint> ReconstructPath(AStarPoint cameFrom, AStarPoint current)
        {
            List<AStarPoint> path = new List<AStarPoint>();

            AStarPoint currentNode = current;
            while (currentNode != cameFrom)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
            return path;
        }

        public float segmentPrecentMoved;
        public float trackLegnth;
        public int currentPointIndex;

        /// <summary>
        /// Must be run every frame;
        /// </summary>
        public Vector3 NextPosition(float movement)
        {
            segmentPrecentMoved += movement / trackLegnth / Vector3.Distance(path[currentPointIndex].position, path[currentPointIndex + 1].position);

            while(segmentPrecentMoved >= 1) 
            {
                segmentPrecentMoved--;
                currentPointIndex++;
            }

            return Vector3.Lerp(path[currentPointIndex].position, path[currentPointIndex + 1].position, segmentPrecentMoved);
        }
    }
}
