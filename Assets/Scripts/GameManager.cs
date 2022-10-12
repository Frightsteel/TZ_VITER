using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string winMessage;
    [SerializeField] private string loseMessage;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] public Transform _gridUI;
    [SerializeField] public Transform _pocketsUI;

    public bool HasSave; // if new game - false, else true

    public SaveLoadData DataSL;
    public Spawner Spawner;

    public int TargetCount = 1;
    
    private int _gridSize = 3;

    private void Awake()
    {
        DataSL.LoadGridInfo(_gridUI, out HasSave);
        DataSL.LoadGridInfo(_pocketsUI, out HasSave);
    }

    private void Start()
    {
        if (!HasSave)
        {
            Spawner.SpawnTarget(_pocketsUI.GetChild(0));

            DataSL.SaveGridInfo(_gridUI);
            DataSL.SaveGridInfo(_pocketsUI);

            HasSave = true;
        }
    }

    private void ShowResult(string message)
    {
        _gameOverPanel.SetActive(true);
        _resultText.text = message;
    }

    private void HideTargetLine(List<Transform> targets)
    {
        foreach (Transform target in targets)
        {
            target.GetComponent<Target>().RunAnimation();
        }
    }

    public void UpdateGameStatus(Transform lastSlot)
    {
        int lastSlotInd = lastSlot.GetSiblingIndex();
        int lastSlotRowInd = lastSlotInd / _gridSize;
        int lastSlotColumnInd = lastSlotInd % _gridSize;
        
        int counterRow = 0;
        int counterColumn = 0;

        List<Transform> targetsRow = new List<Transform>();
        List<Transform> targetsColumn = new List<Transform>();

        targetsColumn.Add(lastSlot.GetChild(0));
        targetsRow.Add(lastSlot.GetChild(0));

        for (int i = 1; i < _gridSize; i++)
        {
            CheckRow(lastSlotRowInd, lastSlotColumnInd, i, ref counterRow, targetsRow);
            CheckColumn(lastSlotRowInd, lastSlotColumnInd, i, ref counterColumn, targetsColumn);
        }

        if (counterRow == _gridSize - 1)
        {
            GameOver(winMessage, targetsRow);
        }
        else if (counterColumn == _gridSize - 1)
        {
            GameOver(winMessage, targetsColumn);
        }
        else
            GameOver(loseMessage, null);
    }

    public void GameOver(string message, List<Transform> targets)
    {
        if (targets != null)
            HideTargetLine(targets);
        
        ShowResult(message);

        DataSL.DeleteGridInfo(_gridUI);
        DataSL.DeleteGridInfo(_pocketsUI);
        HasSave = false;
    }

    private void CheckSlot(int slotInd, ref int count, List<Transform> targets)
    {
        Transform currentSlot = _gridUI.GetChild(slotInd);

        if (currentSlot.childCount != 0)
        {
            count++;
            targets.Add(currentSlot.GetChild(0));
        }
            
    }

    private void CheckRow(int rowInd, int columnInd, int index, ref int count, List<Transform> targets)
    {
        int currentSlotColumnInd = (columnInd + index) % _gridSize;
        int currentSlotRowInd = rowInd;
        int currentSlotInd = (_gridSize * currentSlotRowInd + currentSlotColumnInd);

        CheckSlot(currentSlotInd, ref count, targets);
    }

    private void CheckColumn(int rowInd, int columnInd, int index, ref int count, List<Transform> targets)
    {
        int currentSlotColumnInd = columnInd;
        int currentSlotRowInd = (rowInd + index) % _gridSize;
        int currentSlotInd = (_gridSize * currentSlotRowInd + currentSlotColumnInd);

        CheckSlot(currentSlotInd, ref count, targets);
    }
}
