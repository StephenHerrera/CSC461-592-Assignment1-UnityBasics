using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // How fast the player moves
    public float speed = 5f;

    // Gravity force (negative value to pull down)
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    private void Awake()
    {
        // Get the CharacterController component on the same GameObject
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // A/D or Left/Right arrows = Horizontal
        float moveX = Input.GetAxisRaw("Horizontal");

        // W/S or Up/Down arrows = Vertical
        float moveZ = Input.GetAxisRaw("Vertical");

        // Move along the X/Z plane (no vertical movement from input)
        Vector3 move = new Vector3(moveX, 0f, moveZ);

        // Normalize so diagonal isn't faster
        if (move.magnitude > 1f)
        {
            move = move.normalized;
        }

        // Apply movement (speed * deltaTime so it’s frame-rate independent)
        controller.Move(move * speed * Time.deltaTime);

        // If we’re on the ground and falling, keep a small downward force
        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; // keeps the character "stuck" to the ground
        }

        // Apply gravity over time
        velocity.y += gravity * Time.deltaTime;

        // Move with gravity
        controller.Move(velocity * Time.deltaTime);
    }
}
