using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    public void SaveGridInfo(Transform grid)
    {
        foreach (Transform slot in grid)
        {
            if (slot.childCount != 0)
            {
                Debug.Log("Save" + grid.name + slot.GetSiblingIndex());
                PlayerPrefs.SetInt(grid.name + slot.GetSiblingIndex(), slot.GetSiblingIndex());
            }
            if (slot.childCount == 0 && PlayerPrefs.HasKey(grid.name + slot.GetSiblingIndex()))
            {
                PlayerPrefs.DeleteKey(grid.name + slot.GetSiblingIndex());
            }
        }
    }

    public void DeleteGridInfo(Transform grid)
    {
        foreach (Transform slot in grid)
        {
            if (PlayerPrefs.HasKey(grid.name + slot.GetSiblingIndex()))
            {
                Debug.Log("Delete");
                PlayerPrefs.DeleteKey(grid.name + slot.GetSiblingIndex());
            }
        }
    }

    public void LoadGridInfo(Transform grid, out bool check)
    {
        check = false;
        foreach (Transform slot in grid)
        {
            if (PlayerPrefs.HasKey(grid.name + slot.GetSiblingIndex()))
            {
                Debug.Log("Load " + grid.name + slot.GetSiblingIndex());
                if (slot.childCount == 0)
                {
                    Debug.Log("Load 2");
                    _spawner.SpawnTarget(slot);
                }
                check = true;
            }
        }
    }
}
