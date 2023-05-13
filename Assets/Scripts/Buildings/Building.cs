using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Building : ScriptableObject
{
    public string name;
    [System.Serializable]
    public class Level
    {
        public Mesh mesh;
    }
    public Level[] levels;
    public Vector2 footprint;
}
