
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void Die();

        float MaxHealth {get; set;}
        float CurrentHealth {get; set;}
    }
