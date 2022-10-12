using UnityEngine;

public class AppManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private void OnApplicationPause(bool pause)
    {
        UpdateDataSaves(pause);
    }

    private void OnApplicationFocus(bool focus)
    {
        UpdateDataSaves(!focus);
    }

    private void UpdateDataSaves(bool isGameOnPaused)
    {
        if (isGameOnPaused && _gameManager.HasSave)
        {
            _gameManager.DataSL.SaveGridInfo(_gameManager._gridUI);
            _gameManager.DataSL.SaveGridInfo(_gameManager._pocketsUI);
        }
    }
}
