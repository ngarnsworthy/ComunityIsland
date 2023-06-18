using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference xy;
    public InputActionReference jump;

    [Header("Collitions")]
    public GameObject terrain;
    public Transform camera;

    [Header("Movement")]
    public float speed;
    public float jumpForce;

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
        Vector2 movement = xy.action.ReadValue<Vector2>();
        movement *= (Time.deltaTime * speed);

        float rotation = camera.transform.rotation.eulerAngles.y * -1;

        Vector3 velocity;

        if (!(movement == Vector2.zero))
        {
            velocity = new Vector3(
                movement.x * Mathf.Cos(rotation * Mathf.Deg2Rad) - movement.y * Mathf.Sin(rotation * Mathf.Deg2Rad),
                0,
                movement.y * Mathf.Cos(rotation * Mathf.Deg2Rad) + movement.x * Mathf.Sin(rotation * Mathf.Deg2Rad));
        }
        else
        {
            velocity = new Vector3();
        }

        velocity.y = TerrainGen.world.ChunkLocationToHeight(World.ChunkLocationFromPoint(velocity));

        if (jump.action.ReadValue<float>() != 0 && touchingGround)
        {
            velocity.y += jumpForce;
        }

        rigidbody.MovePosition(velocity + transform.position); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.parent == terrain)
        {
            touchingGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.parent == terrain)
        {
            touchingGround = false;
        }
    }
}
