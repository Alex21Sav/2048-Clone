using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class LavelControl : MonoBehaviour
{
    [SerializeField] private LavelObject _lavelObject;
    private float _bestTimeLavel;
    [SerializeField] private Button[] _choiceLavelButton;
    [SerializeField] private TMP_Text[] _BestTimeText;

    public static event Action<int> GetIndexGame;

    private void OnEnable()
    {
        Timer.SaveTimeGame += SaveTime;
    }
    private void OnDisable()
    {
        Timer.SaveTimeGame -= SaveTime;
    }
    private void Start()
    {
        ViewBestTime();
    }
    private void ViewBestTime()
    {
        for (int i = 0; i < _BestTimeText.Length; i++)
        {
            float time = PlayerPrefs.GetFloat(_lavelObject._ItemLavels[i]._bestTimeKeyLavel);
            if (time == null)
            {
                _bestTimeLavel = 0;
            }
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            _BestTimeText[i].text = string.Format("Time - " + "{0:00}:{1:00}", minutes, seconds);
        }
    }
    private void SaveTime(float time)
    {
        float beckTime = PlayerPrefs.GetFloat(_lavelObject._ItemLavels[Enemy.Instance.GetIndex() - 1]._bestTimeKeyLavel, 0);

        if (beckTime == 0)
        {
            PlayerPrefs.SetFloat(_lavelObject._ItemLavels[Enemy.Instance.GetIndex() - 1]._bestTimeKeyLavel, time);
        }
        else if (beckTime != 0 && time < beckTime)
        {
            PlayerPrefs.SetFloat(_lavelObject._ItemLavels[Enemy.Instance.GetIndex() - 1]._bestTimeKeyLavel, time);
        }
        ViewBestTime();
    }
    public void GetIndex(int index)
    {
        Debug.Log(index);
        GetIndexGame?.Invoke(index);
    }
    public void DellSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
