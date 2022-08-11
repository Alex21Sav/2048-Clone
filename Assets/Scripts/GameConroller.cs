using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class GameConroller : MonoBehaviour
{
    public static GameConroller Instance;

    [SerializeField] private TextMeshProUGUI _gameResult;
    [SerializeField] private AvatarEnemyActiv _avatarEnemyActiv;
    [SerializeField] private AudioSource _swipe;

    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }
    
    public static event Action ResetGame;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void Start()
    {
        ResatartGame();
    }
    
    private void OnEnable()
    {
        StartLavel.GetIndexGame += ChoiceGame;
    }
    private void OnDisable()
    {
        StartLavel.GetIndexGame -= ChoiceGame;
    }
    public void ResatartGame()
    {
        int index = Enemy.Instance.GetIndex();
        _gameResult.text = "";
        SetPoints(0);
        GameStarted = true;
        
        Field.Instance.GenerteFiled();
        Enemy.Instance.RestartGame();
        _avatarEnemyActiv.EnemyDisable();
        _avatarEnemyActiv.EnemyActiv(index);
        ResetGame?.Invoke();
    }
    public void ChoiceGame(int index)
    {
        _gameResult.text = "";
        SetPoints(0);
        GameStarted = true;
        
        Field.Instance.GenerteFiled();
        Enemy.Instance.SetPresentHP(index );
        _avatarEnemyActiv.EnemyDisable();
        _avatarEnemyActiv.EnemyActiv(index);
        ResetGame?.Invoke();
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
    }
    public void AudioPlaySwip()
    {
        _swipe.pitch = Random.Range(0.8f, 1.2f);
        _swipe.Play();
    }
}
