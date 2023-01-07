using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardPoolController : MonoBehaviour
{
    public static Action<int> OnCardCountChanged;
    public static CardPoolController Instance;
    public List<CardControl> Cards { get; private set; }

    [SerializeField] private PlacingUnitInPosition _placingUnitInPosition;
    [SerializeField] private List<Sprite> _sprite;

    [SerializeField] private CardControl _cardPrefab;
    [SerializeField] private Transform _cardContainer;
    private OurUnitsSpawner _ourUnitsSpawner;
    private int _cardCount = 16;
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
        _ourUnitsSpawner = OurUnitsSpawner.Instance;
        Cards = new List<CardControl>();
        for (int i = 0; i < MaxCardInPool; i++)
        {
            CreateCard();
        }
    }

    private void CreateCard()
    {
        var card = Instantiate(_cardPrefab, _cardContainer);
        Cards.Add(card);
        ChangeCardCount();
        RandomInitializeCard(card);
    }

    private void ChangeCardCount()
    {
        _cardCount--;
        OnCardCountChanged?.Invoke(_cardCount);
    }
    private void RandomInitializeCard(CardControl cardControl)
    {

        cardControl.gameObject.GetComponent<Image>().sprite = _sprite[Random.Range(0, _sprite.Count)];
        cardControl.CardPoolController = this;
    }

    public void OnUnit()
    {
        _ourUnitsSpawner.CreateUnit();
        foreach (var ourUnit in _ourUnitsSpawner.OurUnits)
        {
            if (!ourUnit.gameObject.activeSelf)
            {
                _placingUnitInPosition.SetUnit(ourUnit.gameObject);
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