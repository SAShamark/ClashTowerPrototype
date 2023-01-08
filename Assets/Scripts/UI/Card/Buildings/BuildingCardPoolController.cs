using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Card.Buildings
{
    public class BuildingCardPoolController : MonoBehaviour
    {
        public static BuildingCardPoolController Instance;

        public List<BuildingCardControl> Cards { get; private set; }
        [SerializeField] private List<Sprite> _sprite;
        [SerializeField] private PlacingBuildInPosition _placingBuildInPosition;
        [SerializeField] private BuildingCardControl _buildCardPrefab;
        [SerializeField] private Transform _cardContainer;
        [SerializeField] private BuildingsSpawner _buildingsSpawner;
        private int _cardCount = 1;
        private const int MaxCardInPool = 1;

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
            Cards = new List<BuildingCardControl>();
            for (int i = 0; i < MaxCardInPool; i++)
            {
                CreateCard();
            }
        }

        private void CreateCard()
        {
            var card = Instantiate(_buildCardPrefab, _cardContainer);
            Cards.Add(card);
            ChangeCardCount();
            RandomInitializeCard(card);
        }

        private void ChangeCardCount()
        {
            _cardCount--;
        }

        private void RandomInitializeCard(BuildingCardControl unitCardControl)
        {
            unitCardControl.gameObject.GetComponent<Image>().sprite = _sprite[Random.Range(0, _sprite.Count)];
            unitCardControl.BuildingCardPoolController = this;
        }

        public void OnBuild()
        {
            _buildingsSpawner.SpawnBarn();
            _placingBuildInPosition.SetObject(_buildingsSpawner.Burn);
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