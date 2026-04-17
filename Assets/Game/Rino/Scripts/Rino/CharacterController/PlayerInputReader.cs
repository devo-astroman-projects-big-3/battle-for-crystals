using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputReader : MonoBehaviour
{
    #region Fields    
    #endregion

    #region Private Fields
    private InputAction _move;
    private InputAction _jump;
    private InputAction _look;
    private InputAction _aim;
    private InputAction _shoot;

    #endregion

    #region Public Fields
    public Vector2 MoveValue { get; private set; }
    public bool JumpPressed { get; private set; }
    public Vector2 LookValue { get; private set; }
    public bool IsAiming { get; private set; }
    public bool ShootPressed { get; private set; }
    
    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();

        _move = playerInput.actions["Move"];
        _jump = playerInput.actions["Jump"];
        _look = playerInput.actions["Look"];        
        _aim = playerInput.actions["Aim"];
        _shoot = playerInput.actions["Shoot"];

        _jump.performed += OnJump;
        _aim.performed += OnAimPerformed;
        _shoot.performed += OnShoot;
    }

    void OnDestroy()
    {
        _jump.performed -= OnJump;
        _aim.performed -= OnAimPerformed;
        _shoot.performed -= OnShoot;
    }

    void Update()
    {
        MoveValue = _move.ReadValue<Vector2>();
        LookValue = _look.ReadValue<Vector2>();
    }

    void LateUpdate()
    {
        JumpPressed = false;
        ShootPressed = false;
    }
    #endregion

    #region Private Methods 
    private void OnJump(InputAction.CallbackContext ctx)
    {
        JumpPressed = true;
    }

    private void OnAimPerformed(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        IsAiming = !IsAiming;
    }

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        ShootPressed = true;
    }
    #endregion
}