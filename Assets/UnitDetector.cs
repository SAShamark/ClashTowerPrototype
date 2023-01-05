using UnityEngine;

public class UnitDetector : MonoBehaviour
{
    [SerializeField] private BaseUnit _baseUnit;
    [SerializeField] private string _layerName;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(_layerName))
        {
            _baseUnit.SetTarget(other.gameObject);
        }
        else
        {
            
            _baseUnit.SetTarget(_baseUnit.MainTarget);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(_layerName))
        {
            _baseUnit.SetTarget(_baseUnit.MainTarget);
        }
    }
}