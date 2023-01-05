using System;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public static Action<bool> Attack;
    [SerializeField] private Button _attackButton;

    private void Start()
    {
        _attackButton.onClick.AddListener(StartAttack);
    }

    private void StartAttack()
    {
        Attack?.Invoke(true);
    }
}