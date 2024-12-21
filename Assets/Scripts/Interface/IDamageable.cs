
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void Die();
        public IDamageable Target { get; set; }

        float MaxHealth {get; set;}
        float CurrentHealth {get; set;}
    }
