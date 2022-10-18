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

    private bool _isMoving;

    private readonly float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private float _targetAngle;

    private List<EnemyController> _enemiesInTriggerZone = new List<EnemyController>();
    private EnemyController _currentEnemy;

    public NPCController InteractebleNPC { get; set; }

    public DialogueContainerSO ActiveDialogue { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        LockMouse();

        _controller = GetComponent<UCharacterController>();

        _mainCamera = CameraController.Instance.MainCamera;
        _playerCamera = CameraController.Instance.PlayerCinemachineCamera;
        _dialogueCamera = CameraController.Instance.DialogueCinemachineCamera;

        _playerCamera.enabled = true;
        _dialogueCamera.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        Control();
        CheckState();
    }

    protected override void FixedUpdate()
    {
        if (!IsWaitAnim && !DialogueWindow.Instance.IsVisiable)
        {
            Move();
        }
    }

    protected override void Attack()
    {
        _characterAnimator.PlayAnimation(CharacterAnimations.Attack);

        if (_enemiesInTriggerZone.Count > 0)
        {
            StartCoroutine(IAttack());
        }
    }

    protected override IEnumerator IAttack()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        GetNearestEnemy().TakeDamage(_damage);
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
        base.TakeDamage(damage);
        healthbarFillSprite.fillAmount = (float)_health / _maxHealth;
        _characterAnimator.PlayAnimation(CharacterAnimations.Hit);
    }

    public void EnableDialogueCamera()
    {
        _dialogueCamera.enabled = true;
    }

    public void DisableDialogueCamera()
    {
        _dialogueCamera.enabled = false;
    }

    private EnemyController GetNearestEnemy()
    {
        EnemyController enemyController = _currentEnemy;

        var dist = Vector3.Distance(_currentEnemy.transform.position, transform.position);

        foreach (var enemy in _enemiesInTriggerZone)
        {
            var newDist = Vector3.Distance(enemy.transform.position, transform.position);
            if (dist > newDist)
            {
                enemyController = enemy;
                dist = newDist;
            }
        }

        return enemyController;
    }

    private void Control()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockMouse();
        }

        if (!IsWaitAnim)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
            }

            if (Input.GetButtonDown("Interact") && IsCanInteract)
            {
                if (ActiveDialogue != null)
                {
                    _state = CharacterState.Interacts;
                    DialogueSystemController.Instance.Dialogue(ActiveDialogue, InteractebleNPC, this);
                }
            }
        }
    }

    private void CheckState()
    {
        if (!IsCanInteract && (_state == CharacterState.Interacts))
        {
            InteractebleNPC = null;
            _state = CharacterState.Free;
        }

        if (_enemiesInTriggerZone.Count > 0)
        {
            _state = CharacterState.Fights;
        }
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

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyController>(out var enemy))
        {
            _currentEnemy = enemy;
            _enemiesInTriggerZone.Add(enemy);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<EnemyController>(out var enemy))
        {
            _enemiesInTriggerZone.Remove(enemy);
        }
    }
}
