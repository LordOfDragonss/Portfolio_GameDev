using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] public float mouseSensitivity = 3f;
    [SerializeField] internal float movementSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleration = 20f;

    [SerializeField] float worldBottomBoundary = -100f;

    CharacterController Controller;
    internal Vector2 look;
    internal Vector3 velocity;

    (Vector3, Quaternion) initialPositionAndRotation;

    public event Action OnBeforeMove;
    public event Action<bool> OnGroundStateChange;

    public bool IsGrounded => Controller.isGrounded;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction fireAction;
    InputAction ability1Action;
    InputAction ability2Action;
    InputAction ability3Action;

    [SerializeField] int maxhealth;

    [SerializeField] public HealthSystem healthSystem;
    [SerializeField] Slider healthSlider;

    [SerializeField] private GameObject body;

    [SerializeField] internal WeaponHandler.weapons EquipedWeapon;

    bool wasGrounded;

    private void Awake()
    {
        // Remake the player
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // keep the players settings

        instance = this;
        DontDestroyOnLoad(gameObject);

        Controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        fireAction = playerInput.actions["fire"];
        ability1Action = playerInput.actions["ability1"];
        ability2Action = playerInput.actions["ability2"];
        ability3Action = playerInput.actions["ability3"];

    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        initialPositionAndRotation = (transform.position, transform.rotation);
        healthSystem.maxhealth = maxhealth;
        healthSystem.currenthealth = maxhealth;
        if(instance != null)
        {
            EquipedWeapon = instance.EquipedWeapon;
        }
    }
    // Update is called once per frame
    void Update()
    {
        WeaponHandler.WeaponEquiped = EquipedWeapon;
        healthSlider.value = healthSystem.currenthealth;
        UpdateLook();
        UpdateGround();
        UpdateMovement();
        UpdateGravity();
        CheckBounds();




        if (fireAction.WasPressedThisFrame())
        {
            WeaponHandler.instance.Attack();
        }
        if (EquipedWeapon == WeaponHandler.weapons.swordnshield)
        {
            if (ability1Action.WasPressedThisFrame())
            {
                WeaponHandler.instance.Ability1();
            }
            if (ability1Action.WasReleasedThisFrame())
            {
                WeaponHandler.instance.Ability1();
            }
        }
        else
        {
            if (ability1Action.WasPressedThisFrame())
            {
                WeaponHandler.instance.Ability1();
            }
        }
        if (ability2Action.WasPressedThisFrame())
        {
            WeaponHandler.instance.Ability2();
        }
        if (ability3Action.WasPressedThisFrame())
        {
            WeaponHandler.instance.Ability3();
        }

    }
    public void CheckBounds()
    {
        if (transform.position.y < worldBottomBoundary)
        {
            var (position, rotation) = initialPositionAndRotation;
            transform.position = position;
            transform.rotation = rotation;

        }
    }

    void UpdateGround()
    {
        if (wasGrounded != IsGrounded)
        {
            OnGroundStateChange?.Invoke(IsGrounded);
            wasGrounded = IsGrounded;
        }
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = Controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    void UpdateMovement()
    {
        OnBeforeMove?.Invoke();

        var moveInput = moveAction.ReadValue<Vector2>();

        var input = new Vector3();
        input += transform.forward * moveInput.x;
        input += transform.right * moveInput.y;
        input = Vector3.ClampMagnitude(input, 1f);
        input *= movementSpeed;

        var factor = acceleration * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);


        Controller.Move(velocity * Time.deltaTime);
    }

    void UpdateLook()
    {
        var lookInput = lookAction.ReadValue<Vector2>();
        look.x += lookInput.x * mouseSensitivity;
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }
}
