using UnityEngine;
using UnityEngine.Rendering;

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
        [Range(0, 1)]
        public float createVSCraftingWorkers;
        public ItemStack[] craftingIngredients;
        public ItemStack output;
        public float craftingSpeed;
    }
    public Level[] levels;
    public Vector2 footprint;
    public Vector2Int doorLocation;
    public Sprite sprite;
}
