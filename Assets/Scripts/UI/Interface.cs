using System;
using TMPro;
using UI.Card.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Interface : MonoBehaviour
    {
        public static Action<bool> OnAttack;
        [SerializeField] private Button _attackButton;
        [SerializeField] private TMP_Text _theRestOfTheCards;

        private void Awake()
        {
            UnitCardPoolController.OnCardCountChanged += ChangeCardCountText;
        }

        private void Start()
        {
            _attackButton.onClick.AddListener(StartAttack);
        }

        private void ChangeCardCountText(int cardCount)
        {
            _theRestOfTheCards.text = "x" + cardCount;
        }

        private void StartAttack()
        {
            OnAttack?.Invoke(true);
        }

        private void OnDestroy()
        {
            UnitCardPoolController.OnCardCountChanged -= ChangeCardCountText;

        }
    }
}