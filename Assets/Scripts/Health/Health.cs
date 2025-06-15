using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    public float CurrentHealth { get; private set; }
    private bool dead;
    private Animator animator;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        TryGetComponent<Animator>(out animator);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            animator.SetTrigger(AnimationConstants.ElementWithHealth.TransitionHurt);
        }
        else
        {
            Die();
        }
    }

    public void Heal(float health)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + health, 0, startingHealth);
    }

    public bool IsDead() => dead;

    private void Die()
    {
        if (dead)
            return;

        dead = true;
        animator.SetTrigger(AnimationConstants.ElementWithHealth.TransitionDie);
    }
}