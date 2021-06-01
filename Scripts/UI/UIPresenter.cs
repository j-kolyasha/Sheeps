using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPresenter : MonoBehaviour
{
    [SerializeField] private Text _levelTime;
    [SerializeField] private Text _score;
    [SerializeField] private Text _countSheep;
    [SerializeField] private Text _countLevel;

    public void CountSheepUpdate(int countSheep)
    {
        _countSheep.text = countSheep.ToString();
    }
    public void CountLevelUpdate(int countLevel)
    {
        _countLevel.text = countLevel.ToString();
    }
    private void CountScoreUpdate()
    {
        _score.text = GameManager.Instance.Score.ToString();
    }
    private void OnEnable()
    {
        GameManager.Instance.OnNextLevel += CountScoreUpdate;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnNextLevel -= CountScoreUpdate;
    }
}
