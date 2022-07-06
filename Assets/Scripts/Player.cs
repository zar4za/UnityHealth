using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public const int MinHealth = 0;

    [SerializeField] private int _healthDelta = 10;
    [SerializeField] private int _maxHealth = 100;

    private int _health;

    public int MaxHealth => _maxHealth;
    public UnityEvent Died { get; } = new UnityEvent();
    public UnityEvent<int> HealthChanged { get; } = new UnityEvent<int>();

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void AddHealth()
    {
        _health += _healthDelta;

        if (_health > _maxHealth)
            _health = _maxHealth;

        HealthChanged.Invoke(_health);
    }

    public void TakeDamage()
    {
        _health -= _healthDelta;

        if (_health <= MinHealth)
        {
            HealthChanged.Invoke(MinHealth);
            Died.Invoke();

            HealthChanged.RemoveAllListeners();
            Died.RemoveAllListeners();
            return;
        }

        HealthChanged.Invoke(_health);
    }
}
