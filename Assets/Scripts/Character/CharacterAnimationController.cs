using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int IsClimbing = Animator.StringToHash("IsClimbing");
    private static readonly int MoveLeft = Animator.StringToHash("MoveLeft");
    private static readonly int MoveRight = Animator.StringToHash("MoveRight");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Die = Animator.StringToHash("Die");

    private PlayerRoot playerRoot;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        playerRoot = GameController.gameController.playerRoot;
    }

    public void PlayIdle()
    {
        animator.SetTrigger(Idle);
    }

    public void SetClimbing(bool value)
    {
        animator.SetBool(IsClimbing, value);
    }

    public void PlayLaneChange(int direction)
    {
        //Direction: -1 = esquerda, 1 = direita
        if (direction < 0)
        {
            animator.ResetTrigger(MoveRight);
            animator.SetTrigger(MoveLeft);
        }
        else if (direction > 0)
        {
            animator.ResetTrigger(MoveLeft);
            animator.SetTrigger(MoveRight);
        }
    }

    public void PlayLaneChangeLeft()
    {
        animator.ResetTrigger(MoveRight);
        animator.SetTrigger(MoveLeft);
    }

    public void PlayLaneChangeRight()
    {
        animator.ResetTrigger(MoveLeft);
        animator.SetTrigger(MoveRight);
    }

    public void PlayAttack()
    {
        animator.SetTrigger(Attack);
    }

    public void PlayHit()
    {
        animator.SetTrigger(Hit);
    }

    public void PlayDeath()
    {
        animator.SetBool(IsClimbing, false);
        animator.SetTrigger(Die);
    }

    public void PlayEndRun()
    {
        if (playerRoot == null)
        {
            playerRoot = GameController.gameController.playerRoot;
        }

        playerRoot.DeathByAnimationEvent();
    }
}
