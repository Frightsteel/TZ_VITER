using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _targetPF;

    public void SpawnTarget(Transform parent)
    {
        Transform currentTarget = Instantiate(_targetPF);
        currentTarget.SetParent(parent);
        currentTarget.localPosition = Vector3.zero;
    }
}
