using UnityEngine;

public class PlacingUnitInPosition : MonoBehaviour
{
    [SerializeField] private LayerMask _objectSelectionMask;
    [SerializeField] private LayerMask _dropObjectMask;
    [SerializeField] private Camera _camera;
    private CardPoolController _cardPoolController;
    private Vector3 _startObjectPosition;
    private Vector3 _worldPosition;
    private const float MaxDistance = 100;
    private GameObject _object;
    private Ray _ray;

    private void Start()
    {
        _cardPoolController = CardPoolController.Instance;
    }

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_object != null)
        {
            if (Input.GetMouseButton(0))
            {
                MoveObject();
            }

            if (Input.GetMouseButtonUp(0))
            {
                LoseObject();
            }
        }
    }

    public void SetUnit(GameObject unit)
    {
        _object = unit;
    }

    private void MoveObject()
    {
        if (Physics.Raycast(_ray, out var hit, MaxDistance, _objectSelectionMask))
        {
            _object.SetActive(true);
            _worldPosition = hit.point;
            var nextPosition = new Vector3(_worldPosition.x, _object.transform.position.y, _worldPosition.z);
            if (_worldPosition.x > 0)
            {
                nextPosition = new Vector3(-1, _object.transform.position.y, _worldPosition.z);
            }

            _object.gameObject.transform.position = nextPosition;
        }
        else if (Physics.Raycast(_ray, MaxDistance, _dropObjectMask))
        {
            _object.SetActive(false);
        }
    }

    private void LoseObject()
    {
        _object = null;
        _cardPoolController.AddNewCard();
        CheckDropPlace();
    }

    private void CheckDropPlace()
    {
        if (Physics.Raycast(_ray, MaxDistance, _dropObjectMask))
        {
            foreach (var card in _cardPoolController.Cards)
            {
                if (!card.gameObject.activeSelf)
                {
                    card.gameObject.SetActive(true);
                    return;
                }
            }
        }
    }
}