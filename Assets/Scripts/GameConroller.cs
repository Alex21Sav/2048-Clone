using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameConroller : MonoBehaviour
{
    public static GameConroller Instance;

    [SerializeField] private TextMeshProUGUI _gameResult;
    [SerializeField] private TextMeshProUGUI _pointsText;

    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        _gameResult.text = "";
        SetPoints(0);
        GameStarted = true;
        
        Field.Instance.GenerteFiled();
    }

    public void Win()
    {
        GameStarted = false;
        _gameResult.text = "You Win!";
    }

    public void Lose()
    {
        GameStarted = false;
        _gameResult.text = "You Lose!";
    }

    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    private void SetPoints(int points)
    {
        Points = points;
        _pointsText.text = Points.ToString();
    }
}
