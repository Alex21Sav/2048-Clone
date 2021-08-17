using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _points;

    private CellAnimation _currentAnimtion;

    public int X { get; private set; }
    public int Y { get; private set; }

    public int Value { get; private set; }

    public int Points => IsEmpty ? 0 : (int)Mathf.Pow(2, Value);

    public bool IsEmpty => Value == 0;
    public bool HasMerged { get; private set; }

    public const int MaxValue = 11;

    public void SetValue( int x, int y, int value, bool updateUI = true)
    {
        X = x;
        Y = y;
        Value = value;

        if(updateUI)
        UpdateCell();
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;

        GameConroller.Instance.AddPoints(Points);        
    }
    public void ResetFlags()
    {
        HasMerged = false;
    }
    public void MergeWithCell(Cell otherCell)
    {
        CellAnimationController.Instance.SmoothTransition(this, otherCell, true);
        otherCell.IncreaseValue();
        SetValue(X, Y, 0);
    }
    public void MoveToCell(Cell target)
    {
        CellAnimationController.Instance.SmoothTransition(this, target, false);
        target.SetValue(target.X, target.Y, Value, false);
        SetValue(X, Y, 0);        
    }
    public void UpdateCell()
    {
        _points.text = IsEmpty ? string.Empty : Points.ToString();
        _points.color = Value <= 2 ? ColorManager.Instance.PointsDarkColor : ColorManager.Instance.PointsLightColor;
        _image.color = ColorManager.Instance.CellColors[Value];
    }

    public void SetAnimation(CellAnimation animation)
    {
        _currentAnimtion = animation;
    }

    public void CancelAnimation()
    {
        if(_currentAnimtion != null)        
            _currentAnimtion.Destroy();
    }
}
