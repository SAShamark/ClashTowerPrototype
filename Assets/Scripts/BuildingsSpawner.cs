using UnityEngine;

public class BuildingsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _barnPrefab;
    [SerializeField] private Transform _barnTransform;
     public GameObject Burn;

    public void SpawnBarn()
    {
        Burn = Instantiate(_barnPrefab, _barnTransform);
    }
}