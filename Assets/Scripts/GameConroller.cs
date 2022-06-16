using UnityEngine;
using TMPro;

public class GameConroller : MonoBehaviour
{
    public static GameConroller Instance;

    [SerializeField] private TextMeshProUGUI _gameResult;
    [SerializeField] private RandomEnemy _randomEnemy;
    [SerializeField] private AudioSource _swipe;

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
        int index = Enemy.Instance.GetIndex();
        _gameResult.text = "";
        SetPoints(0);
        GameStarted = true;
        
        Field.Instance.GenerteFiled();
        Enemy.Instance.RestartGame();
        _randomEnemy.EnemyDisable();
        _randomEnemy.RandomEnemyActiv(index);
        
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
