using UnityEngine;

public class EntitiesHealth : MonoBehaviour
{
    [SerializeField] private float _health = 40;
    private float _maxHealth;
    private const float MinHealth = 0;
    void Start()
    {
        _maxHealth = _health;

    }
    public void Heal(float healthValue)
    {
        _health += healthValue;
        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;
        if (_health <= MinHealth)
        {
            _health = MinHealth;
            Death();
        }
    }

    public void Death()
    {
        _health = MinHealth;
        gameObject.SetActive(false);
    }
}
