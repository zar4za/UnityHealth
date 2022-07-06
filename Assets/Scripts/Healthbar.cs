using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Healthbar : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed = 1f;
    [SerializeField] private Player _player;

    private Slider _slider;
    private Coroutine _runningCoroutine = null;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _player.MaxHealth;
        _player.HealthChanged.AddListener(OnHealthChanged);
    }

    private void OnHealthChanged(int newHealth)
    {
        if (_runningCoroutine != null)
            StopCoroutine(_runningCoroutine);

        _runningCoroutine = StartCoroutine(ChangeSliderValue(newHealth));
    }

    private IEnumerator ChangeSliderValue(int target)
    {
        while (target != _slider.value)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target, _transitionSpeed);
            yield return null;
        }
    }
}
