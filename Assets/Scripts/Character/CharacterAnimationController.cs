using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int IsClimbing = Animator.StringToHash("IsClimbing");
    private static readonly int LaneLeft = Animator.StringToHash("LaneLeft");
    private static readonly int LaneRight = Animator.StringToHash("LaneRight");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Die = Animator.StringToHash("Die");

    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void SetClimbing(bool value)
    {
        animator.SetBool(IsClimbing, value);
    }

    public void PlayLaneChange(int direction)
    {
        // direction: -1 = esquerda, 1 = direita
        if (direction < 0)
        {
            animator.ResetTrigger(LaneRight);
            animator.SetTrigger(LaneLeft);
        }
        else if (direction > 0)
        {
            animator.ResetTrigger(LaneLeft);
            animator.SetTrigger(LaneRight);
        }
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
}
