using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    [SerializeField] private GameConroller _gameConroller;
    [SerializeField] private RandomEnemy _randomEnemy;

    [SerializeField] private TMP_Text _textFullHP;
    [SerializeField] private TMP_Text _textHP;
    
    [SerializeField] private int _fullHP;
    [SerializeField] private int _startFullHP;
    
    [SerializeField] private int _indexLevel = 1;
    [SerializeField] private int _indexLevelEnemy = 0;
    [SerializeField] private int _hp;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {       
        _hp = _fullHP;
        _textHP.text = _hp.ToString();
        SetFullHP();
    }
    public void TakeDamage(int damage)
    {
        _randomEnemy.TakeDamageAnimation();
        SetHP(_hp - damage);        
        if (_hp <= 0)
        {
            _indexLevel = _indexLevel * 2;
            _indexLevelEnemy = _indexLevelEnemy + 1;

            _fullHP = _startFullHP * _indexLevel;
            _hp = _fullHP;
            SetFullHP();
            _gameConroller.StartGame();
        }
    }
    public void SetHP(int hp)
    {
        _hp = hp;
        _textHP.text = _hp.ToString();
    }
    private void SetFullHP()
    {
        _textFullHP.text = _fullHP.ToString();
    }
    public void RestartGame()
    {
        _hp = _fullHP;
        _textHP.text = _hp.ToString();
    }
    public int GetIndex()
    {
        int index = _indexLevelEnemy;
        return index;
    }
}
