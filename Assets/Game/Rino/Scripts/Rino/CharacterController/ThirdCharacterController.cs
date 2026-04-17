using Fusion;
using UnityEngine;

public class ThirdCharacterController : NetworkBehaviour
{
    #region Fields
    [SerializeField] private PlayerInputReader _playerInputReader;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _aimDebugTransform;
    #endregion

    #region Private Fields
    private float _currentSpeed = 0f;
    private Vector3 _velocity;
    private float _verticalVelocity;

    private bool _isGrounded;
    private bool _wasGrounded;

    private bool _isDead = false;
    #endregion

    #region Public Fields
    [Header("Movement - Run")]
    public float runSpeed = 5f;

    [Header("Movement - Walk")]
    public float walkSpeed = 2f;
    public bool isWalking = false;

    [Header("Rotation")]
    public float rotationSpeed = 10f;

    [Header("Jump")]
    public float jumpHeight = 1.5f;
    public float gravity = -20f;

    [Header("Aim")]
    public float aimRayDistance = 1000f;
    public LayerMask aimLayerMask = Physics.DefaultRaycastLayers;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        _currentSpeed = runSpeed;

        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    /* private void Update()
    {
        HandleMovement();
        HandleJumpAndGravity();
        UpdateAnimator();
    } */
    public override void FixedUpdateNetwork()
    {
        HandleMovement();
        HandleJumpAndGravity();        
    }
    #endregion
    #region Public Methods
    public void MakeHeroDead()
    {
        _isDead = true;
    }

    public override void Spawned()
    {
        if (HasStateAuthority == false)
            return;

        Debug.Log("Spawned!");
    }

    #endregion

    #region Private Methods
    private void HandleMovement()
    {
        float x = _playerInputReader.MoveValue.x;
        float z = _playerInputReader.MoveValue.y;

        _currentSpeed = isWalking ? walkSpeed : runSpeed;

        Vector3 move = GetCameraRelativeMove(x, z);

        if (move.sqrMagnitude > 1f)
        {
            move.Normalize();
        }

        if (!_isDead)
        {
            if (_playerInputReader.IsAiming)
            {
                RotateTowardsAimPoint();
            }
            else
            {
                RotateTowardsMovement(move);
            }

        }
        else
        {
            _animator.SetBool("DeadMode", true);
        }

        

        _velocity.x = move.x * _currentSpeed;
        _velocity.z = move.z * _currentSpeed;
    }

    private Vector3 GetCameraRelativeMove(float x, float z)
    {
        if (_cameraTarget == null)
        {
            return new Vector3(x, 0f, z);
        }

        Vector3 cameraForward = _cameraTarget.forward;
        Vector3 cameraRight = _cameraTarget.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * z + cameraRight * x;
    }

    private void RotateTowardsMovement(Vector3 move)
    {
        if (move.sqrMagnitude <= 0.0001f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(move);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void RotateTowardsAimPoint()
    {
        Vector3 aimPoint = GetAimPoint();
        Vector3 direction = aimPoint - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.0001f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private Vector3 GetAimPoint()
    {
        if (_mainCamera == null)
        {
            return transform.position + transform.forward * 10f;
        }

        Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, aimRayDistance, aimLayerMask))
        {
            _aimDebugTransform.position = hit.point;
            return hit.point;
        }

        return ray.GetPoint(aimRayDistance);
    }

    private void HandleJumpAndGravity()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f;
        }

        if (_playerInputReader.JumpPressed && _isGrounded)
        {
            Debug.Log("Jump");
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

            //_animator.SetTrigger("Jump");

        }

        _verticalVelocity += gravity * Time.deltaTime;
        _velocity.y = _verticalVelocity;

        _characterController.Move(_velocity * Time.deltaTime);

        _isGrounded = _characterController.isGrounded;
    }

    #endregion
}