using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _addButton.onClick.AddListener(_player.AddHealth);
        _removeButton.onClick.AddListener(_player.TakeDamage);

        _player.HealthChanged.AddListener(OnPlayerChangedHealth);
        _player.Died.AddListener(OnPlayerDied);
    }

    private void OnPlayerChangedHealth(int health)
    {
        _addButton.interactable = true;

        if (health >= _player.MaxHealth)
            _addButton.interactable = false;
    }

    private void OnPlayerDied()
    {
        _addButton.interactable = false;
        _removeButton.interactable = false;

        _addButton.onClick.RemoveListener(_player.AddHealth);
        _removeButton.onClick.RemoveListener(_player.TakeDamage);
    }
}
