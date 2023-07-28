using Aoiti.Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CitizenRecord
{
    public int id = -1;
    public CitizenTask task;
    public PlacedBuilding nextBuilding
    {
        get
        {
            if (_nextBuilding == null)
            {
                _nextBuilding = task.NextTaskLocation(this);
            }
            else if(path != null && currentPointIndex >= path.Count)
            {
                _nextBuilding = task.NextTaskLocation(this);
            }

            return _nextBuilding;
        }
    }

    [NonSerialized] PlacedBuilding _nextBuilding;

    public Vector3 location = new Vector3(0, 10, 0);

    [NonSerialized]
    public GameObject gameObject;
    public List<Vector3> path = new List<Vector3>();
    public Rigidbody rigidbody
    {
        get
        {
            if(_rigidbody == null)
            {
                _rigidbody = gameObject.GetComponent<Rigidbody>();
            }
            return _rigidbody;
        }
    }
    Rigidbody _rigidbody;

    public PlacedBuilding employment;

    public float segmentPrecentMoved;
    public float movementSpeed => 5;
    public int currentPointIndex;
    public float trackLegnth;

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
