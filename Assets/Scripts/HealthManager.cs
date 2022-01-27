using System;
using UnityEngine;

public sealed class HealthManager : MonoBehaviour
{
    
    public event Action OnDied = delegate {  };
    public event Action OnTakeDamage = delegate {  };
    public event Action<float> OnHealthChange = delegate(float f) {  };
    
    [SerializeField] private float initialHealth = 100f;

    private float health;

    public float Health
    {
        get => health;
        private set => health = value;
    }

    private void OnEnable()
    {
        Health = initialHealth;
        OnHealthChange?.Invoke(Health);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        OnTakeDamage?.Invoke();
        OnHealthChange?.Invoke(Health);
        
        if (Health <= 0 )
            OnDied?.Invoke();
    }
}