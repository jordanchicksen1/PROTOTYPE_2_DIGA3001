using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [Space(5)]

    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed = 8f; // Speed at which the player moves
    public float lookSpeed; // Sensitivity of the camera movement
    public Transform playerCamera; // Reference to the player's camera
    // Private variables to store input values and the character controller
    private Vector2 _moveInput; // Stores the movement input from the player
    private Vector2 _lookInput; // Stores the look input from the player
    private float _verticalLookRotation = 0f; // Keeps track of vertical camera rotation for clamping
    private Vector3 _velocity; // Velocity of the player
    public CharacterController _characterController; // Reference to the CharacterController component
    public float crouchSpeed = 1.5f; //short speed
    public bool isCrouching = false; //if short or normal

    private void OnEnable()
    {

        // Create a new instance of the input actions
        var playerInput = new Controller();

        // Enable the input actions
        playerInput.Player.Enable();

        // Subscribe to the movement input events
        playerInput.Player.Movement.performed += ctx => _moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        playerInput.Player.Movement.canceled += ctx => _moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

        // Subscribe to the look input events
        playerInput.Player.Aim.performed += ctx => _lookInput = ctx.ReadValue<Vector2>(); // Update lookInput when look input is performed

        //playerInput.Player.LookAround.performed += ctx => currentScheme = ctx.control;
        playerInput.Player.Aim.canceled += ctx => _lookInput = Vector2.zero; // Reset lookInput when look input is canceled

        // Subscribe to the light fire input event
        playerInput.Player.Boost.performed += ctx => Boost(); // Call the PickUpObject method when pick-up input is performed

        //Subscribe to the sprint
        playerInput.Player.Shoot.performed += ctx => Shoot(); // sprint

        //Subscribe to the UseFuel
        playerInput.Player.Bomb.performed += ctx => Bomb(); // use fuel

        //Subscribe to the pause
        playerInput.Player.Pause.performed += ctx => Pause(); // use fuel

       
    }

    public void Move()
    {
        // Create a movement vector based on the input
        Vector3 move = new Vector3(_moveInput.x, _moveInput.y, 0);

        // Transform direction from local to world space
        move = transform.TransformDirection(move);

        var currentSpeed = isCrouching ? crouchSpeed : moveSpeed;

        // Move the character controller based on the movement vector and speed
        _characterController.Move(move * currentSpeed * Time.deltaTime);
    }

    private void LookAround()
    {
        
        
            // Get horizontal and vertical look inputs and adjust based on sensitivity
            var lookX = _lookInput.x * lookSpeed;
            var lookY = _lookInput.y * lookSpeed;

            // Horizontal rotation: Rotate the player object around the y-axis 
            transform.Rotate(0, lookX, 0);

            // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
            _verticalLookRotation -= lookY;
            _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -15f, 20f);

            // Apply the clamped vertical rotation to the player camera
            //playerCamera.localEulerAngles = new Vector3(_verticalLookRotation, 0, 0);
        


    }

    public void Update()
    {
        Aim();
        Move();
    }
    public void Aim()
    {
        Debug.Log("should aim");
    }
    public void Boost()
    {
        Debug.Log("should boost");
    }

    public void Shoot()
    {
        Debug.Log("should shoot");
    }

    public void Bomb()
    {
        Debug.Log("should bomb");
    
    }

    public void Pause()
    {
        Debug.Log("should pause");
    }
}
