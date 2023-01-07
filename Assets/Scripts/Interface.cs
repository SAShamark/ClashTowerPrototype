using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public static Action<bool> OnAttack;
    public static Action<bool> OnBarn;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _barnButton;
    [SerializeField] private TMP_Text _theRestOfTheCards;

    private void Awake()
    {
        CardPoolController.OnCardCountChanged += ChangeCardCountText;
    }

    private void Start()
    {
        _attackButton.onClick.AddListener(StartAttack);
        _barnButton.onClick.AddListener(BuildBarn);
    }

    private void ChangeCardCountText(int cardCount)
    {
        _theRestOfTheCards.text = "x" + cardCount;
    }

    private void StartAttack()
    {
        OnAttack?.Invoke(true);
    }
    private void BuildBarn()
    {
        OnBarn?.Invoke(true);
    }

    private void OnDestroy()
    {
        CardPoolController.OnCardCountChanged -= ChangeCardCountText;

    }
}