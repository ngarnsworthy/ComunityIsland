using UnityEngine;
using UnityEngine.InputSystem;

public class DEBUG : MonoBehaviour
{
    [Header("Spawn")]
    public InputActionReference spawnInput;
    public string spawnActionName;
    public GameObject citizen;

    private void Start()
    {
        spawnInput.action.Enable();
    }
    void Update()
    {
        if (spawnInput.action.triggered)
            Instantiate(citizen);
    }

}
