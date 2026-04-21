using Fusion;
using UnityEngine;
using Cinemachine;
using System;

public class MyPlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _jumpPressed;
    private bool _firePressed;
    private CharacterController _controller;

    private float _coyoteTimeCounter;
    private float _jumpBufferCounter;

    public Camera Camera;

    public Action JustSpawnAction;

    [Header("Movement")]
    public float PlayerSpeed = 2f;
    public float RotationSpeed = 12f;

    [Header("Jump / Gravity")]
    public float JumpForce = 5f;
    public float GravityValue = -9.81f;

    [Header("Jump Feel")]
    public float CoyoteTime = 0.12f;
    public float JumpBufferTime = 0.12f;

    [Header("Camera")]
    [SerializeField] private ThirdPersonCameraPivot _cameraPivotController;
    [SerializeField] private FireShoot _fireShoot;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private ThirdPersonAim _thirdPersonAim;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!HasStateAuthority)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
            _jumpBufferCounter = JumpBufferTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _firePressed = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
            return;

        if (_firePressed)
        {
            Vector3 aimPoint = _thirdPersonAim.GetAimPoint();
            Vector3 fireDirection = (aimPoint - _muzzle.position).normalized;

            _fireShoot.Fire(_muzzle.position, fireDirection);
            _firePressed = false;
        }

        bool isGrounded = _controller.isGrounded;

        if (isGrounded)
        {
            _coyoteTimeCounter = CoyoteTime;

            if (_velocity.y < 0f)
            {
                _velocity.y = -1f;
            }
        }
        else
        {
            _coyoteTimeCounter -= Runner.DeltaTime;
        }

        if (_jumpBufferCounter > 0f)
        {
            _jumpBufferCounter -= Runner.DeltaTime;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Transform camRoot = _cameraPivotController != null ? _cameraPivotController.CameraRoot : null;

        Vector3 forward = camRoot != null ? camRoot.forward : transform.forward;
        Vector3 right = camRoot != null ? camRoot.right : transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;
        Vector3 move = moveDirection * PlayerSpeed * Runner.DeltaTime;

        if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f)
        {
            _velocity.y = JumpForce;
            _jumpBufferCounter = 0f;
            _coyoteTimeCounter = 0f;
            _jumpPressed = false;
        }

        _velocity.y += GravityValue * Runner.DeltaTime;

        _controller.Move(move + _velocity * Runner.DeltaTime);

        if (moveDirection.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                RotationSpeed * Runner.DeltaTime
            );
        }

        _jumpPressed = false;
    }

    public override void Spawned()
    {
        if (!HasStateAuthority)
            return;

        Camera = Camera.main;

        CameraManager cameraManager = Camera != null ? Camera.GetComponent<CameraManager>() : null;

        if (cameraManager != null && _cameraPivotController != null)
        {
            cameraManager.SetVirtualTargets(
                _cameraPivotController.CameraPivot,
                _cameraPivotController.CameraPivot
            );
        }

        JustSpawnAction?.Invoke();
    }
}