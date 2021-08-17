using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellAnimationController : MonoBehaviour
{
    public static CellAnimationController Instance { get; private set; }

    [SerializeField] private CellAnimation _animationPref;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DOTween.Init();
    }

    public void SmoothTransition(Cell from, Cell to, bool isMirging)
    {
        Instantiate(_animationPref, transform, false).Move(from, to, isMirging);
    }

    public void SmoothAppear(Cell cell)
    {
        Instantiate(_animationPref, transform, false).Appear(cell);
    }

}
