using Fusion;
using UnityEngine;
using Cinemachine;

public class MyPlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _jumpPressed;
    private CharacterController _controller;

    public Camera Camera;

    [Header("Movement")]
    public float PlayerSpeed = 2f;
    public float RotationSpeed = 12f;

    [Header("Jump / Gravity")]
    public float JumpForce = 5f;
    public float GravityValue = -9.81f;

    [Header("Camera")]
    [SerializeField] private ThirdPersonCameraPivot _cameraPivotController;

    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!HasStateAuthority)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
            return;

        if (_controller.isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -1f;
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

        _velocity.y += GravityValue * Runner.DeltaTime;

        if (_jumpPressed && _controller.isGrounded)
        {
            _velocity.y = JumpForce;
        }

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

        //_cameraPivotController = cameraManager.GetThirdPersonCameraPivot();

        if (cameraManager != null && _cameraPivotController != null)
        {
            cameraManager.SetVirtualTargets(
                _cameraPivotController.CameraPivot,
                _cameraPivotController.CameraPivot
            );

            _virtualCamera = cameraManager.GetVirtualCamera();
        }
        else
        {
            Debug.Log("FLAG_1" + cameraManager);
            Debug.Log("FLAG_1" + _cameraPivotController);
        }
    }
}