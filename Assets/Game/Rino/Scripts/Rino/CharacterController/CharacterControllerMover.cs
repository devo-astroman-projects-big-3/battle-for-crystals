using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;

    [Header("Jump")]
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _characterController;
    private Vector3 _velocity;

    private bool _isGrounded;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical);

        if (move.sqrMagnitude > 1f)
            move.Normalize();

        // Rotate toward movement direction
        if (move != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(move);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    _rotationSpeed * Time.deltaTime
                );
        }

        _characterController.Move(
            move * _moveSpeed * Time.deltaTime
        );
    }

    private void HandleJump()
    {
        _isGrounded = _characterController.isGrounded;

        // Small downward force when grounded
        if (_isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        // Jump input
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("JUMP INPUT!");
            // Calculate jump velocity
            _velocity.y = Mathf.Sqrt(
                _jumpHeight * -2f * _gravity
            );
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(
            _velocity * Time.deltaTime
        );
    }
}