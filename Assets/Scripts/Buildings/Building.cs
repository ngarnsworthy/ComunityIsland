using UnityEngine;

[CreateAssetMenu]
public class Building : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        public Mesh mesh;
        public Vector3 rotation;
        public int maxWorkers;
        public bool createsItems;
        public Item createdItem;
        public float itemsPerSecond;
    }
    public Level[] levels;
    public Vector2 footprint;
    public Sprite sprite;
}
