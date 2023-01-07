using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class EnemyUnitsSpawner : MonoBehaviour
{
    [SerializeField] private List<BaseUnit> _allUnitsType;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private GameObject _enemyMainTarget;
    private List<BaseUnit> _enemyUnits;
    private const int MaxEnemyCount = 6;
    private int _enemyCount;

    private void Start()
    {
        _enemyUnits = new List<BaseUnit>();
        StartCoroutine(UnitsCreator());
    }

    private IEnumerator UnitsCreator()
    {
        while (_enemyCount < MaxEnemyCount)
        {
            yield return new WaitForSeconds(1);
            var unit = Instantiate(_allUnitsType[0], _enemyContainer);
            unit.MainTarget = _enemyMainTarget;
            unit.transform.position = new Vector3(Random.Range(19.5f, 1), 0.5f, Random.Range(-9.5f, 9.5f));
            AddEnemyUnit(unit);
            _enemyCount++;
        }
    }

    private void AddEnemyUnit(BaseUnit unit)
    {
        _enemyUnits.Add(unit);
    }

    public void RemoveEnemyUnit(BaseUnit unit)
    {
        _enemyUnits.Remove(unit);
    }
}