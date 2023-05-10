using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] public float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] Transform cameraTransform;
    [SerializeField] internal float Souls;
    [SerializeField] Slider soulslider;

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
    InputAction fadeAction;
    InputAction tpAction;
    InputAction fireAction;

    [SerializeField] internal AudioHandler audioHandler;

    internal AbilityController ac;

    [SerializeField] private GameObject body;
    [SerializeField] private Renderer rend;


    [SerializeField] private Material material;
    [SerializeField] private Material FadingMaterial;

    bool wasGrounded;
    public bool isFading;
    public bool canTeleport;
    public bool YcamEnabled;
    bool teleporting = false;
    public bool canMove = true;

    private void Awake()
    {
        canMove = true;
        Controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        ac = GetComponent<AbilityController>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        fadeAction = playerInput.actions["fade"];
        tpAction = playerInput.actions["teleport"];
        fireAction = playerInput.actions["fire"];

    }
    // Start is called before the first frame update
    void Start()
    {
        rend = body.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material;
        Cursor.lockState = CursorLockMode.Locked;
        initialPositionAndRotation = (transform.position, transform.rotation);
    }
    [SerializeField] Text Confirmationtext;
    // Update is called once per frame
    void Update()
    {

        soulslider.value = Souls;

        UpdateLook();
        if (canMove)
        {
            UpdateGround();
            UpdateMovement();
            UpdateGravity();
            CheckBounds();
        }


        var fadeInput = fadeAction.WasPressedThisFrame();

        if (fadeInput && !isFading && Souls > 0)
        {
            audioHandler.PlayFadeSound();
            isFading = true;
        }
        else if (fadeInput && isFading)
        {
            isFading = false;
            audioHandler.PlayFadereverseSound();
        }
        if (Souls <= 0 && isFading)
        {
            isFading = false;
        }
        if (isFading)
        {
            rend.sharedMaterial = FadingMaterial;
            movementSpeed = 8;
            Souls -= 0.01f;
        }
        else if (!isFading)
        {
            movementSpeed = 5;
            rend.sharedMaterial = material;
        }

        var tpInput = tpAction.WasPerformedThisFrame();
        if (tpInput && canTeleport)
        {
            YcamEnabled = YcamEnabled ? false : true;
            teleporting = teleporting ? false : true;
        }

        if (teleporting)
        {
            Confirmationtext.gameObject.SetActive(true);
            ac.tpTarget.gameObject.SetActive(true);
        }
        else
        {
            Confirmationtext.gameObject.SetActive(false);
            ac.tpTarget.gameObject.SetActive(false);
        }

        var fireInput = fireAction.WasPressedThisFrame();
        if (fireInput && teleporting && Souls > 0)
        {
            audioHandler.PlayTeleportSound();
            StartCoroutine(ac.Teleport());
            teleporting = false;
            Souls -= 10;

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
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
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
        if (YcamEnabled)
        {
            look.y += lookInput.y * mouseSensitivity;


            look.y = Mathf.Clamp(look.y, -89f, 89f);
        }
        else if (!YcamEnabled)
        {
            look.y = 0f;
        }

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }
}
