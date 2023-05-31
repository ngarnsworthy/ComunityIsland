using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Building : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        public Mesh mesh;
    }
    public Level[] levels;
    public Vector2 footprint;
}
