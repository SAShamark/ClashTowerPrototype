using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Card.Unit
{
    public class UnitCardPoolController : MonoBehaviour
    {
        public static Action<int> OnCardCountChanged;
        public static UnitCardPoolController Instance;
        public List<UnitCardControl> Cards { get; private set; }

        [SerializeField] private List<Sprite> _sprite;

        [SerializeField] private PlacingUnitInPosition _placingUnitInPosition;
        [SerializeField] private UnitCardControl _unitCardPrefab;
        [SerializeField] private Transform _cardContainer;
        [SerializeField] private OurUnitsSpawner _ourUnitsSpawner;
        private int _cardCount = 50;
        private const int MaxCardInPool = 4;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Cards = new List<UnitCardControl>();
            for (int i = 0; i < MaxCardInPool; i++)
            {
                CreateCard();
            }
        }

        private void CreateCard()
        {
            var card = Instantiate(_unitCardPrefab, _cardContainer);
            Cards.Add(card);
            ChangeCardCount();
            RandomInitializeCard(card);
        }

        private void ChangeCardCount()
        {
            _cardCount--;
            OnCardCountChanged?.Invoke(_cardCount);
        }

        private void RandomInitializeCard(UnitCardControl unitCardControl)
        {
            unitCardControl.gameObject.GetComponent<Image>().sprite = _sprite[Random.Range(0, _sprite.Count)];
            unitCardControl.UnitCardPoolController = this;
        }

        public void OnUnit()
        {
            _ourUnitsSpawner.CreateUnit();
            foreach (var ourUnit in _ourUnitsSpawner.OurUnits)
            {
                if (!ourUnit.gameObject.activeSelf)
                {
                    _placingUnitInPosition.SetObject(ourUnit.gameObject);
                }
            }
        }

        public void AddNewCard()
        {
            if (_cardCount != 0)
            {
                foreach (var card in Cards)
                {
                    if (!card.gameObject.activeSelf)
                    {
                        card.gameObject.SetActive(true);
                        RandomInitializeCard(card);
                        ChangeCardCount();
                    }
                }
            }
        }
    }
}