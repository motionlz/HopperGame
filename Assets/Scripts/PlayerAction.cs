using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AttackModule attackModule;
    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float jumpDistance = 1f;
    [SerializeField] private float jumpDuration = 0.3f;
    
    [Header("Shield Settings")]
    [SerializeField] private GameObject shield;
    
    private PlayerControl playerControl;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime;
    private PlayerState playerState = PlayerState.OnTile;
    
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnBlock;
    public event Action OnLanded;

    private void Awake()
    {
        playerControl = new PlayerControl();
    }
    public void Init()
    {
        
    }
    void Update()
    {
        if (playerState == PlayerState.Jumping)
        {
            Jump();
        }

        SetShield();
    }
    private void OnEnable()
    {
        playerControl.Player.Jump.performed += OnJumpInput;
        playerControl.Player.Attack.performed += OnAttackInput;
        playerControl.Player.Block.performed += OnBlockInput;
        playerControl.Enable();
    }
    private void OnDisable() 
    {
        playerControl.Player.Jump.performed -= OnJumpInput;
        playerControl.Player.Attack.performed -= OnAttackInput;
        playerControl.Player.Block.performed -= OnBlockInput;
        playerControl.Disable();
    }
    
    private void OnJumpInput(InputAction.CallbackContext _context)
    {
        if (playerState == PlayerState.Jumping) return;
        JumpAction();
        OnJump?.Invoke();
    }
    private void OnAttackInput(InputAction.CallbackContext _context)
    {
        if (playerState == PlayerState.Jumping) return;
        AttackAction();
        OnAttack?.Invoke();
    }
    private void OnBlockInput(InputAction.CallbackContext _context)
    {
        if (playerState == PlayerState.Jumping) return;
        BlockAction();
        OnBlock?.Invoke();
    }
    
    private void JumpAction()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(jumpDistance, 0, 0);
        elapsedTime = 0;
        playerState = PlayerState.Jumping;
        
        animator.SetBool("Jump", true);
    }
    private void AttackAction()
    {
        playerState = PlayerState.OnTile;
        attackModule.Attack();
        animator.SetTrigger("Attack");
    }
    private void BlockAction()
    {
        playerState = PlayerState.Blocking;
        Debug.Log("Blocking");
    }
    private void Jump()
    {
        elapsedTime += Time.deltaTime;
        float _progress = elapsedTime / jumpDuration;

        float _x = Mathf.Lerp(startPosition.x, targetPosition.x, _progress);
        float _y = startPosition.y + (4 * jumpHeight * _progress * (1 - _progress));
        transform.position = new Vector3(_x, _y, transform.position.z);
    }

    private void SetShield()
    {
        shield.SetActive(playerState == PlayerState.Blocking);
    }
    public bool IsBlock()
    {
        return playerState == PlayerState.Blocking;
    }
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag(GlobalTag.GROUND_TAG))
        {
            playerState = PlayerState.OnTile;
            animator.SetBool("Jump", false);
            
            var _pos = transform.position;
            _pos.x = Mathf.RoundToInt(_pos.x);
            transform.position = _pos;
            
            OnLanded?.Invoke();
        }

        if (_col.TryGetComponent<IInteractable>(out IInteractable _object))
        {
            _object?.OnInteract();
        }
    }
    private void OnTriggerExit2D(Collider2D _col)
    {
        if (_col.CompareTag(GlobalTag.GROUND_TAG))
        {
            playerState = PlayerState.Jumping;
        }
    }
    private enum PlayerState { OnTile, Jumping, Blocking }
}
