using TMPro;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeLeft;
    [SerializeField] private TMP_Text _timerTxt;
    
    private bool _timerOn = false;
    private float _currentTime;
    
    public static event Action<float> SaveTimeGame;
   
    void Start()
    {
        TimerStart();
    }
    private void OnEnable()
    {
        Enemy.WinGame += GetTime;
        GameConroller.ResetGame += ResetTimer;
    }
    private void OnDisable()
    {
        Enemy.WinGame -= GetTime;
        GameConroller.ResetGame -= ResetTimer;
    }
    void Update()
    {
        if(_timerOn)
        {
            _timeLeft += Time.deltaTime;
            UpdateTimer(_timeLeft);
        }
    }
    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        _timerTxt.text = string.Format("Time - " + "{0:00}:{1:00}", minutes, seconds);
        _currentTime = currentTime;
    }
    private void TimerStop()
    {
        _timerOn = false;
    }
    private void TimerStart()
    {
        _timerOn = true;
    }
    private void GetTime()
    {
        SaveTimeGame?.Invoke(_currentTime);
    }
    private void ResetTimer()
    {
        UpdateTimer(0);
        _timeLeft = 0;
        _currentTime = 0;
        _timerTxt.text = "00:00";
    }
}
