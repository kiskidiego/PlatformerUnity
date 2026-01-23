using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterJumpHandler))]
[RequireComponent(typeof(CharacterDash))]
[RequireComponent(typeof(WallCling))]
[RequireComponent(typeof(WallJump))]
[RequireComponent(typeof(FastFall))]
[RequireComponent(typeof(CharacterAttack))]
[RequireComponent(typeof(CharacterDirection))]
[RequireComponent(typeof(CharacterParry))]
[RequireComponent(typeof(PlatformFallThrough))]
[RequireComponent(typeof(CharacterAnimatorController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference jumpAction;
    [SerializeField] InputActionReference attackAction;
    [SerializeField] InputActionReference dashAction;
    [SerializeField] InputActionReference parryAction;
    [SerializeField] InputActionReference lookAction;
    [SerializeField] InputActionReference fallAction;

    CharacterMovement characterMovement;
    CharacterJumpHandler characterJumpHandler;
    CharacterDash characterDash;
    WallCling wallCling;
    WallJump wallJump;
    CharacterAttack characterAttack;
    CharacterDirection characterDirection;
    CharacterParry characterParry;
    PlatformFallThrough platformFallThrough;
    CharacterAnimatorController characterAnimator;


    void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterJumpHandler = GetComponent<CharacterJumpHandler>();
        characterDash = GetComponent<CharacterDash>();
        wallCling = GetComponent<WallCling>();
        wallJump = GetComponent<WallJump>();
        characterAttack = GetComponent<CharacterAttack>();
        characterDirection = GetComponent<CharacterDirection>();
        characterParry = GetComponent<CharacterParry>();
        platformFallThrough = GetComponent<PlatformFallThrough>();
        characterAnimator = GetComponent<CharacterAnimatorController>();
    }

    void OnEnable()
    {
        characterDash.onDashEnd += OnDashEnd;

        moveAction.action.Enable();
        jumpAction.action.Enable();
        attackAction.action.Enable();
        dashAction.action.Enable();
        parryAction.action.Enable();
        lookAction.action.Enable();
        fallAction.action.Enable();

        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;

        jumpAction.action.started += OnJumpStart;
        jumpAction.action.canceled += OnJumpEnd;

        lookAction.action.performed += OnLook;
        lookAction.action.canceled += OnLook;

        dashAction.action.started += OnDashStart;

        attackAction.action.started += OnAttack;

        parryAction.action.started += OnParry;

        fallAction.action.started += OnStartFallThrough;

        fallAction.action.canceled += OnStopFallThrough;
    }

    void OnDisable()
    {
        characterDash.onDashEnd -= OnDashEnd;

        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMove;

        jumpAction.action.started -= OnJumpStart;
        jumpAction.action.canceled -= OnJumpEnd;

        lookAction.action.performed -= OnLook;
        lookAction.action.canceled -= OnLook;

        dashAction.action.started -= OnDashStart;

        attackAction.action.started -= OnAttack;

        parryAction.action.started -= OnParry;

        fallAction.action.started -= OnStartFallThrough;

        fallAction.action.canceled -= OnStopFallThrough;

        moveAction.action.Disable();
        jumpAction.action.Disable();
        attackAction.action.Disable();
        dashAction.action.Disable();
        parryAction.action.Disable();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        characterMovement.movementInput = context.ReadValue<float>();
    }

    void OnJumpStart(InputAction.CallbackContext context)
    {
        if(wallCling.IsClinging)
        {
            if(wallJump.StartWallJump())
            {
                //Debug.Log("Wall Jump started");
                characterAnimator.TriggerJump();
            }
        }
        else
        {
            if(characterJumpHandler.StartJump())
            {
                //Debug.Log("Jump started");
                characterAnimator.TriggerJump();
            }
        }
    }

    void OnJumpEnd(InputAction.CallbackContext context)
    {
        characterJumpHandler.StopJump();
        wallJump.StopWallJump();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        characterDirection.Direction = context.ReadValue<Vector2>();
    }

    void OnDashStart(InputAction.CallbackContext context)
    {
        if(characterDash.StartDash())
        {
            DisableInput();
            characterAnimator.TriggerDash();
        }
    }

    void OnDashEnd()
    {
        //Debug.Log("Dash end received in PlayerController");
        EnableInput();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if(characterAttack.Attack(EnableInput))
        {
            DisableInput();
            //Debug.Log("Attack performed");
            characterAnimator.TriggerAttack();
        }
    }

    private void OnParry(InputAction.CallbackContext context)
    {
        if(characterParry.StartParry(EnableInput, characterAnimator.TriggerParryHit))
        {
            DisableInput();
            //Debug.Log("Parry performed");
            characterAnimator.TriggerParry();
        }
    }

    private void OnStartFallThrough(InputAction.CallbackContext context)
    {
        platformFallThrough.EnableFallThrough();
    }
    private void OnStopFallThrough(InputAction.CallbackContext context)
    {
        platformFallThrough.DisableFallThrough();
    }

    public void EnableInput()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        attackAction.action.Enable();
        dashAction.action.Enable();
        parryAction.action.Enable();
        lookAction.action.Enable();
        fallAction.action.Enable();
        attackAction.action.Enable();
    }
    public void DisableInput()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        attackAction.action.Disable();
        dashAction.action.Disable();
        parryAction.action.Disable();
        lookAction.action.Disable();
        fallAction.action.Disable();
        attackAction.action.Disable();
    }
}