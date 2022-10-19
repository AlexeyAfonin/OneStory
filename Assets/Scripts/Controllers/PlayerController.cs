using Cinemachine;
using DialogueSystem;
using DialogueSystem.SO;
using DialogueSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OneStory.Core.Utils.Enums;
using UCharacterController = UnityEngine.CharacterController;

[RequireComponent(typeof(UCharacterController))]
public sealed class PlayerController : CharacterController
{
    [Header("UI")]
    [SerializeField] private Image healthbarFillSprite;
    [Header("Points")]
    [SerializeField] private Transform dialogueCameraPosition;

    private Camera _mainCamera;
    private CinemachineFreeLook _playerCamera;
    private CinemachineVirtualCamera _dialogueCamera;

    private UCharacterController _controller;

    private readonly float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private float _targetAngle;

    public NPCController InteractebleNPC { get; set; }

    public DialogueContainerSO ActiveDialogue { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        _controller = GetComponent<UCharacterController>();

        _mainCamera = CameraController.Instance.MainCamera;
        _playerCamera = CameraController.Instance.PlayerCinemachineCamera;
        _dialogueCamera = CameraController.Instance.DialogueCinemachineCamera;

        _playerCamera.enabled = true;
        _dialogueCamera.enabled = false;
    }

    protected override void Update()
    {
        if (!GameContoller.Instance.IsGamePause)
        {
            base.Update();

            Control();
        }
    }

    protected override void FixedUpdate()
    {
        if (!IsWaitingEndAnimation && !IsInteracting)
        {
            Move();
        }
    }

    protected override void Attack()
    {
        _characterAnimator.PlayAnimation(CharacterAnimations.Attack);

        if (triggerZone.CharactersInZone.Count > 0)
        {
            StartCoroutine(IEAttack());
        }
    }

    public override IEnumerator IEAttack()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        triggerZone.GetNearCharacter().TakeDamage(_damage);
    }

    protected override void Move()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");

        var direction = new Vector3(horizontal, 0, vertical).normalized;

        IsMoving = direction.magnitude >= 0.1f;

        if (IsMoving)
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
        _targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
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
        if (!GameContoller.Instance.IsGamePause)
        {
            base.TakeDamage(damage);
            healthbarFillSprite.fillAmount = (float)_health / _maxHealth;
            _characterAnimator.PlayAnimation(CharacterAnimations.Hit);
        }
    }

    public void EnableDialogueCamera()
    {
        _dialogueCamera.enabled = true;
    }

    public void DisableDialogueCamera()
    {
        _dialogueCamera.enabled = false;
    }

    private void Control()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GameContoller.Instance.PauseGame();
        }

        if (!IsWaitingEndAnimation)
        {
            if (Input.GetButtonDown("Attack") && !IsAttacking && !IsInteracting)
            {
                Attack();
            }

            if (Input.GetButtonDown("Interact") && IsCanInteract)
            {
                if (ActiveDialogue != null)
                {
                    State = CharacterState.Interact;
                    DialogueSystemController.Instance.Dialogue(ActiveDialogue, InteractebleNPC, this);
                }
            }
        }
    }
}
