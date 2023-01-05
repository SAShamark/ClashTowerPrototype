using System.Collections.Generic;
using UnityEngine;

public class UnitContainer : MonoBehaviour
{
    [SerializeField] private List<BaseUnit> _ourUnits;
    [SerializeField] private List<BaseUnit> _enemyUnits;

    private void Start()
    {
        //_ourUnits = new List<BaseUnit>();
        //_enemyUnits = new List<BaseUnit>();
        
        foreach (var unit in _ourUnits)
        {
            Interface.Attack += unit.GoAttack;
        }
    }

    public void AddOurUnit(BaseUnit unit)
    {
        _ourUnits.Add(unit);
    }

    public void AddEnemyUnit(BaseUnit unit)
    {
        _enemyUnits.Add(unit);
    }

    public void RemoveOurUnit(BaseUnit unit)
    {
        _ourUnits.Remove(unit);
        Interface.Attack -= unit.GoAttack;
    }

    public void RemoveEnemyUnit(BaseUnit unit)
    {
        _enemyUnits.Remove(unit);
    }
}