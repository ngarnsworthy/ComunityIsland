using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using static UnityEngine.Rendering.DebugUI;

public class Pathfinder
{
    public class AStarPoint : IComparable<AStarPoint>{
        public AStarPath path;
        public AStarPoint parent;
        public Vector3 position;

        public int CompareTo(AStarPoint other)
        {
            if (other == null) return 1;

            return f.CompareTo(other.f);
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

        public float f
        {
            get
            {
                return g + h;
            }
        }

        public float g
        {
            get
            {
                AStarPoint point = this;
                float cost = 0;
                while (point.parent != null)
                {
                    cost += Vector3.Distance(point.position, point.parent.position);
                    point = point.parent;
                }
                return cost;
            }
        }

        public float h
        {
            get
            {
                return Vector3.Distance(position, path.end);
            }
        }

        public AStarPoint(AStarPath path, AStarPoint parent, Vector3 position)
        {
            this.path = path;
            this.parent = parent;
            this.position = position;
        }
    }
    public class AStarPath
    {
        public Vector3 start;
        public Vector3 end;

        public PriorityQueue<AStarPoint, AStarPoint> openSet = new PriorityQueue<AStarPoint, AStarPoint>();
        public HashSet<AStarPoint> closedSet = new HashSet<AStarPoint>();

        public List<AStarPoint> Path
        {
            get
            {
                if(_path == null)
                {
                    _path = new List<AStarPoint>();
                    AStarPoint point = new AStarPoint(this, null, start);
                    Path.Add(point);
                    closedSet.Add(point);

                    while(Vector3.Distance(point.position, end) >= 5)
                    {
                        List<AStarPoint> neighbors = new List<AStarPoint>();

                        neighbors.Add(new AStarPoint(this, point, new Vector3(point.position.x+1, point.position.y, point.position.z)));
                        neighbors.Add(new AStarPoint(this, point, new Vector3(point.position.x, point.position.y+1, point.position.z)));
                        neighbors.Add(new AStarPoint(this, point, new Vector3(point.position.x - 1, point.position.y, point.position.z)));
                        neighbors.Add(new AStarPoint(this, point, new Vector3(point.position.x, point.position.y-1, point.position.z)));

                        foreach (var item in neighbors)
                        {
                            if(openSet.)
                        }
                    }
                }
                return _path;
            }
        }
        List<AStarPoint> _path;

        public float segmentPrecentMoved;
        public float trackLegnth;
        public int currentPointIndex;

        /// <summary>
        /// Must be run every frame;
        /// </summary>
        public Vector3 NextPosition(float movement)
        {
            segmentPrecentMoved += movement / trackLegnth / Vector3.Distance(Path[currentPointIndex].position, Path[currentPointIndex + 1].position);

            while (segmentPrecentMoved >= 1)
            {
                segmentPrecentMoved--;
                currentPointIndex++;
            }

            return Vector3.Lerp(Path[currentPointIndex].position, Path[currentPointIndex + 1].position, segmentPrecentMoved);
        }
    }
}
