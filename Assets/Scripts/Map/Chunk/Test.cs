using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float[,] testData = new float[14, 16];
        GetComponent<MeshFilter>().sharedMesh = ChunkRenderer.GetMesh(testData);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
