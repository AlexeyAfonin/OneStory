using OneStory.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static OneStory.Core.Utils.Enums;
using UCharacterController = UnityEngine.CharacterController;

public sealed class PlayerController : CharacterController
{
    [Space(10f)]
    [Header ("Camera")]
    [SerializeField] private Transform cinemachineCamera;

    private UCharacterController _controller;

    private bool _isMoving;

    private readonly float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private float _targetAngle;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        LockMouse();

        _controller = GetComponent<UCharacterController>();

        if (cinemachineCamera == null)
        {
            cinemachineCamera = Camera.main.transform;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape)) UnlockMouse();
        if (Input.GetKeyDown(KeyCode.Space)) TakeDamage(10);

        if (!IsWaitAnim)
        {
            if (Input.GetAxisRaw("Fire1") == 1) Attack();
        }
    }

    protected override void FixedUpdate()
    {
        if (!IsWaitAnim)
        {
            Move();
        }
    }

    protected override void Attack()
    {
        _characterAnimator.PlayAnimation(CharacterAnimations.Attack);
    }

    protected override void Move()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");

        var direction = new Vector3(horizontal, 0, vertical).normalized;

        _isMoving = direction.magnitude >= 0.1f;

        if (_isMoving)
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Walk);
            Rotate(direction);
            Vector3 moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDirection.normalized * Speed * Time.deltaTime);
        }
        else
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Idle);
        }
    }

    protected override void Rotate(Vector3 direction)
    {
        _targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cinemachineCamera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(animator.transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
        animator.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    protected override void Dead()
    {
        _characterAnimator.PlayAnimation(CharacterAnimations.Dying);
        base.Dead();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _characterAnimator.PlayAnimation(CharacterAnimations.Hit);
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
