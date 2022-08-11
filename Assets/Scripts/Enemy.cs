using TMPro;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    [SerializeField] private GameConroller _gameConroller;
    [SerializeField] private AvatarEnemyActiv avatarEnemyActiv;
    [SerializeField] private LavelControl _lavelControl;

    [SerializeField] private TMP_Text _textFullHP;
    [SerializeField] private TMP_Text _textHP;
    
    [SerializeField] private int _fullHP;
    [SerializeField] private int _startFullHP;
    
    [SerializeField] private int _indexLevel = 1;
    [SerializeField] private int _indexLevelEnemy = 0;
    [SerializeField] private int _hp;
    
    public static event Action WinGame;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {       
        _hp = _fullHP * _indexLevel;
        _textHP.text = _hp.ToString();
        SetFullHP();
    }
    private void OnEnable()
    {
        StartLavel.GetIndexGame += ChoiceIndexEnemy;
    }
    private void OnDisable()
    {
        StartLavel.GetIndexGame -= ChoiceIndexEnemy;
    }
    public void TakeDamage(int damage)
    {
        avatarEnemyActiv.TakeDamageAnimation();
        SetHP(_hp - damage);        
        if (_hp <= 0)
        {
            WinGame?.Invoke();
            _indexLevel = _indexLevel + 1;
            _indexLevelEnemy = _indexLevelEnemy + 1;
            SetPresentHP(_indexLevel);
            _gameConroller.ResatartGame();
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
    public void SetPresentHP(int index)
    {
        _indexLevel = index;
        _fullHP = _startFullHP * _indexLevel;
        _hp = _fullHP;
        _textHP.text = _hp.ToString();
        _textFullHP.text = _fullHP.ToString();
    }
    public void RestartGame()
    {
        _hp = _fullHP;
        _textFullHP.text = _hp.ToString();
        _textHP.text = _hp.ToString();
    }
    public int GetIndex()
    {
        int index = _indexLevelEnemy;
        return index;
    }

    private void ChoiceIndexEnemy(int index)
    {
        _indexLevel = index;
        _indexLevelEnemy = index;
    }
}
