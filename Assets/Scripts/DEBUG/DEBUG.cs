using UnityEngine;
using UnityEngine.InputSystem;

public class DEBUG : MonoBehaviour
{
    public InputActionAsset inputActions;
    [Header("Spawn")]
    public string spawnActionName;
    public GameObject citizen;
    void Update()
    {
        if(inputActions.FindAction(spawnActionName).triggered)
        Instantiate(citizen);
    }
    
}
