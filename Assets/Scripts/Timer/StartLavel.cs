using UnityEngine;
using System;
using UnityEngine.UI;

public class StartLavel : MonoBehaviour
{
    [SerializeField] private Button _choiceLavelButton;
    [SerializeField] private int _indexLavel;
    
    public static event Action<int> GetIndexGame;

    private void OnEnable()
    {
        _choiceLavelButton.onClick.AddListener(GetIndex);
    }
    private void OnDisable()
    {
        _choiceLavelButton.onClick.RemoveListener(GetIndex);
    }
    private void GetIndex()
    {
        GetIndexGame?.Invoke(_indexLavel);
    }
}
