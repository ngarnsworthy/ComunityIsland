using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditBuilding : MonoBehaviour
{
    [Header("Raycast")]
    public Camera camera;
    [Header("Input")]
    public InputActionReference openBuilding;
    [Header("UI")]
    public Menu editMenu;
    [Header("Player")]
    public Move player;

    PlacedBuilding building;

    private void Start()
    {
        openBuilding.action.Enable();
    }

    void Update()
    {
        if (openBuilding.action.ReadValue<float>() != 0)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(ray, out hit))
            {
                PlacedBuildingComponent component = hit.collider.gameObject.GetComponent<PlacedBuildingComponent>();
                if (component!=null)
                {
                    PlacedBuilding building = component.placedBuilding;
                    if (building != null)
                    {
                        this.building = building;
                        editMenu.Show();
                        player.enabled = false;
                    }
                }
            }
        }
    }
}
