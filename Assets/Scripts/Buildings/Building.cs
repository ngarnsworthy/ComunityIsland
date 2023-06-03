using UnityEngine;

[CreateAssetMenu]
public class Building : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        public Mesh mesh;
        public int maxWorkers;
    }
    public Level[] levels;
    public Vector2 footprint;
}
