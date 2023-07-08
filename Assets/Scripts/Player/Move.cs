using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference xy;
    public InputActionReference jump;

    [Header("Collitions")]
    public Transform camera;

    [Header("Movement")]
    public float speed;
    public float jumpForce;

    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;

    public bool running
    {
        get
        {
            return pRunning;
        }
        set
        {
            pRunning = value;
        }
    }
    bool pRunning = true;

    bool touchingGround = false;

    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        xy.action.Enable();
        jump.action.Enable();
    }

    void Update()
    {
        if (!running) {
            return;
        }
        Vector2 movement = xy.action.ReadValue<Vector2>();
        movement *= (Time.deltaTime * speed);

        float rotation = camera.transform.rotation.eulerAngles.y * -1;

        Vector3 movementChange;

        if (!(movement == Vector2.zero))
        {
            movementChange = new Vector3(
                movement.x * Mathf.Cos(rotation * Mathf.Deg2Rad) - movement.y * Mathf.Sin(rotation * Mathf.Deg2Rad),
                0,
                movement.y * Mathf.Cos(rotation * Mathf.Deg2Rad) + movement.x * Mathf.Sin(rotation * Mathf.Deg2Rad));
        }
        else
        {
            movementChange = new Vector3();
        }

        Vector3 pos = transform.position;
        float height = TerrainGen.world[new Vector2Int((int)pos.y, (int)pos.x)];
        if (transform.position.y < height - 10)
        {
            pos.y = height + 10;
            transform.position = pos;
        }

        if (jump.action.ReadValue<float>() != 0 && touchingGround)
        {
            Vector3 velocity = rigidbody.velocity;
            velocity.y = jumpForce;
            rigidbody.velocity = velocity;
        }

        rigidbody.MovePosition(movementChange + transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {

        touchingGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {

        touchingGround = false;
    }

    private void OnEnable()
    {
        virtualCamera.enabled = true;
    }

    private void OnDisable()
    {
        virtualCamera.enabled = false;
    }
}
