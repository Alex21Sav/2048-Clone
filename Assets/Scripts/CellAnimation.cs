using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CellAnimation : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _points;

    private float _moveTime = .1f;
    private float _appearTime = .1f;

    private Sequence _sequence;
    public void Move(Cell from, Cell to, bool isMerging)
    {
        from.CancelAnimation();
        to.SetAnimation(this);
        _image.color = ColorManager.Instance.CellColors[from.Value];
        _points.text = from.Points.ToString();
        _points.color = from.Value <= 2 ?
            ColorManager.Instance.PointsDarkColor :
            ColorManager.Instance.PointsLightColor;

        transform.position = from.transform.position;

        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOMove(to.transform.position, _moveTime).SetEase(Ease.InOutQuad));

        if(isMerging)
        {
            _sequence.AppendCallback(() =>
            {
                _image.color = ColorManager.Instance.CellColors[to.Value];
                _points.text = to.Points.ToString();
                _points.color = to.Value <= 2 ?
            ColorManager.Instance.PointsDarkColor :
            ColorManager.Instance.PointsLightColor;
            });

            _sequence.Append(transform.DOScale(1.2f, _appearTime));
            _sequence.Append(transform.DOScale(1, _appearTime));
        }

        _sequence.AppendCallback(() =>
        {
            to.UpdateCell();
            Destroy();
        });
    }
    public void Appear(Cell cell)
    {
        cell.CancelAnimation();
        cell.SetAnimation(this);

        _image.color = ColorManager.Instance.CellColors[cell.Value];
        _points.text = cell.Points.ToString();
        _points.color = cell.Value <= 2 ?
            ColorManager.Instance.PointsDarkColor :
            ColorManager.Instance.PointsLightColor;

        transform.position = cell.transform.position;
        transform.localScale = Vector2.zero;

        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOScale(1.2f, _appearTime *2));
        _sequence.Append(transform.DOScale(1, _appearTime *2));

        _sequence.AppendCallback(() =>
        {
            cell.UpdateCell();
            Destroy();
        });
    }    

    public void Destroy()
    {
        _sequence.Kill();
        Destroy(gameObject);
    }
}
