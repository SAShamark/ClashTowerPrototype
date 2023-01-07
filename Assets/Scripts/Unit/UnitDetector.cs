using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class UnitDetector : MonoBehaviour
    {
        [SerializeField] private BaseUnit _baseUnit;
        [SerializeField] private string _layerName;
        private List<GameObject> _otherUnits;
        private GameObject _closest;


        private void Start()
        {
            _otherUnits = new List<GameObject>();
        }

        private void Update()
        {
            FindClosestEnemy();
        }

        private void FindClosestEnemy()
        {
            float distance = Mathf.Infinity;
            var unitPosition = transform.position;
            foreach (var otherUnit in _otherUnits)
            {
                var diff = otherUnit.transform.position - unitPosition;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    _baseUnit.SetTarget(otherUnit);
                    distance = curDistance;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(_layerName))
            {
                other.gameObject.GetComponent<EntitiesHealth>().OnDie += RemoveUnitFromList;
                _otherUnits.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(_layerName))
            {
                _otherUnits.Remove(other.gameObject);
            }
        }

        private void RemoveUnitFromList(GameObject unit)
        {
            _otherUnits.Remove(unit);
        }

        private void OnDestroy()
        {
            foreach (var unit in _otherUnits)
            {
                unit.GetComponent<EntitiesHealth>().OnDie -= RemoveUnitFromList;
            }
        }
    }
}