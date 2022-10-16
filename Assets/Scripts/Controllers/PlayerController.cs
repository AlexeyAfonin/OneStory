using OneStory.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using UCharacterController = UnityEngine.CharacterController;

[RequireComponent (typeof(UCharacterController))]
public sealed class PlayerController : CharacterController
{
    [SerializeField] private SettingsVariables settings;
    [SerializeField] private UCharacterController controller;

    private bool _isMoving;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        LockMouse();
        settings = SettingsController.Instance.Config.SettingsVariables;
        controller = GetComponent<UCharacterController>();
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) UnlockMouse();
    }

    protected override void FixedUpdate()
    {
        Move();
        Rotate();
    }

    protected override void Move()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");

        _isMoving = vertical != 0 || horizontal != 0;

        if (_isMoving)
        {
            var direction = transform.TransformDirection(new Vector3(horizontal, 0, vertical));
            controller.SimpleMove(direction * Speed);
        }
    }


    protected override void Rotate()
    {
        var cursorX = Input.GetAxis("Mouse X") * (settings.Sensitivity * 2) * Time.deltaTime;
        var playerEulerY = (transform.rotation.eulerAngles.y + cursorX) % 360;
        transform.rotation = Quaternion.Euler(0, playerEulerY, 0);
    }

    protected override void Dead()
    {
        base.Dead();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
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
