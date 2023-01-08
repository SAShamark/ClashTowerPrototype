using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Unit
{
    public class OurUnitsSpawner : MonoBehaviour
    {
        [SerializeField] private List<BaseUnit> _allUnitsType;
        [SerializeField] private Transform _ourContainer;
        [SerializeField] private GameObject _ourMainTarget;
        [HideInInspector] public List<BaseUnit> OurUnits;

        private void Start()
        {
            OurUnits = new List<BaseUnit>();
        }

        public void CreateUnit()
        {
            var unit = Instantiate(_allUnitsType[0], _ourContainer);
            unit.MainTarget = _ourMainTarget;
            Interface.OnAttack += unit.GoAttack;
            AddOurUnit(unit);
            unit.gameObject.SetActive(false);
        }

        private void AddOurUnit(BaseUnit unit)
        {
            OurUnits.Add(unit);
        }

        public void RemoveOurUnit(BaseUnit unit)
        {
            OurUnits.Remove(unit);
            Interface.OnAttack -= unit.GoAttack;
        }
    }
}