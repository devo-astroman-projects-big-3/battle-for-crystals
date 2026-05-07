using Fusion;
using UnityEngine;
using System;

public class HeroMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _firePressed;
    //private CharacterController _controller;

    private float _coyoteTimeCounter;
    private float _jumpBufferCounter;

    private bool _inputsBlocked = false;

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

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private ThirdPersonCameraPivot _thirdPersonCameraPivot;
    [SerializeField] private FireShoot _fireShoot;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private ThirdPersonAim _thirdPersonAim;
    [SerializeField] private Transform _buddyTransform;


    private Vector2 _moveInput;
private bool _jumpPressed;
//private bool _firePressed;
private Vector3 _cachedAimPoint;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!Object.HasInputAuthority || _inputsBlocked)
            return;

        _moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        _moveInput = Vector2.ClampMagnitude(_moveInput, 1f);

        if (Input.GetButtonDown("Jump"))
            _jumpPressed = true;

        if (Input.GetMouseButtonDown(0))
        {
            _firePressed = true;
            _cachedAimPoint = _thirdPersonAim.GetAimPoint();
        }
    }


    public override void FixedUpdateNetwork()
    {
        if (_inputsBlocked)
            return;

        if (!Object.HasStateAuthority)
            return;

        if (_firePressed)
        {
            Vector3 fireDirection = (_cachedAimPoint - _muzzle.position).normalized;
            _fireShoot.Fire(_muzzle.position, fireDirection);

            _firePressed = false;
        }

        bool isGrounded = _characterController.isGrounded;

        if (isGrounded)
        {
            _coyoteTimeCounter = CoyoteTime;

            if (_velocity.y < 0f)
                _velocity.y = -1f;
        }
        else
        {
            _coyoteTimeCounter -= Runner.DeltaTime;
        }

        if (_jumpPressed)
        {
            _jumpBufferCounter = JumpBufferTime;
            _jumpPressed = false;
        }

        if (_jumpBufferCounter > 0f)
            _jumpBufferCounter -= Runner.DeltaTime;

        Transform camRoot = _thirdPersonCameraPivot != null
            ? _thirdPersonCameraPivot.CameraRoot
            : null;

        Vector3 forward = camRoot != null
            ? camRoot.forward
            : _buddyTransform.forward;

        Vector3 right = camRoot != null
            ? camRoot.right
            : _buddyTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection =
            (forward * _moveInput.y + right * _moveInput.x).normalized;

        Vector3 move = moveDirection * PlayerSpeed * Runner.DeltaTime;

        if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f)
        {
            _velocity.y = JumpForce;
            _jumpBufferCounter = 0f;
            _coyoteTimeCounter = 0f;
        }

        _velocity.y += GravityValue * Runner.DeltaTime;

        _characterController.Move(move + _velocity * Runner.DeltaTime);

        if (moveDirection.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            _buddyTransform.rotation = Quaternion.Slerp(
                _buddyTransform.rotation,
                targetRotation,
                RotationSpeed * Runner.DeltaTime
            );
        }
    }

    public override void Spawned()
    {
        if (!Object.HasInputAuthority)
            return;

        /* Camera = Camera.main;

        CameraManager cameraManager =
            Camera != null
            ? Camera.GetComponent<CameraManager>()
            : null;

        if (cameraManager != null && _cameraPivotController != null)
        {
            cameraManager.SetVirtualTargets(
                _cameraPivotController.CameraPivot,
                _cameraPivotController.CameraPivot
            );
        } */

        JustSpawnAction?.Invoke();
    }

    #region Input Control Methods

    public void BlockInputs()
    {
       _firePressed = false;
       _jumpPressed = false;
       _moveInput = Vector2.zero;
    }

    public void UnblockInputs()
    {
        if (!Object.HasInputAuthority)
            return;

        _inputsBlocked = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #endregion
}
