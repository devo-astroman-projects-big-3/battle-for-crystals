using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleMoverCharacterController : NetworkBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _gravity = -20f;

    private float _verticalVelocity;

    private void Reset()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        if (_characterController == null)
            _characterController = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority)
            return;

        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        input = Vector2.ClampMagnitude(input, 1f);

        Vector3 horizontalMove =
            transform.right * input.x +
            transform.forward * input.y;

        if (_characterController.isGrounded && _verticalVelocity < 0f)
            _verticalVelocity = -2f;

        _verticalVelocity += _gravity * Runner.DeltaTime;

        Vector3 velocity = horizontalMove * _moveSpeed;
        velocity.y = _verticalVelocity;

        _characterController.Move(velocity * Runner.DeltaTime);
    }
}