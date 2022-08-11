using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance;

    [Space(10)]
    [SerializeField] private Cell _cellPref;
    [SerializeField] private RectTransform _rectTransform;
    [Header("Field Properties")]
    [SerializeField] private float _cellSize;
    [SerializeField] private float _spacing;
    [SerializeField] private int _fieldSize;
    [SerializeField] private int _initCellCount;

    private Cell[,] _field;
    private bool _anyCellMoved;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        //SwipeDerection.SwipeEvent += OnInput;
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
            OnInput(Vector2.left);
        if (Input.GetKeyDown(KeyCode.D))
            OnInput(Vector2.right);
        if (Input.GetKeyDown(KeyCode.W))
            OnInput(Vector2.up);
        if (Input.GetKeyDown(KeyCode.S))
            OnInput(Vector2.down);
#endif       
    }
    public void OnInput(Vector2 direction)
    {
        if (!GameConroller.GameStarted)
            return;
        _anyCellMoved = false;
        ResetCellsFlags();

        Move(direction);

        if (_anyCellMoved)
        {
            GenerateRandomCell();
            CheckGameResult();
        }
    }
    private void Move(Vector2 direction)
    {
        int startXY = direction.x > 0 || direction.y < 0 ? _fieldSize - 1 : 0;
        int dir = direction.x != 0 ? (int)direction.x : -(int)direction.y;

        for (int i = 0; i < _fieldSize ; i++)
        {
            for (int k = startXY; k >= 0 && k < _fieldSize; k -= dir)
            {
                var cell = direction.x != 0 ? _field[k, i] : _field[i, k];

                if (cell.IsEmpty)
                    continue;

                var cellToMerge = FindCellToMerge(cell, direction);
                if(cellToMerge != null)
                {
                    GameConroller.Instance.AudioPlaySwip();
                    cell.MergeWithCell(cellToMerge);
                    _anyCellMoved = true;
                    continue;
                }

                var emptyCell = FindEmptyCell(cell, direction); 
                if(emptyCell != null)
                {
                    GameConroller.Instance.AudioPlaySwip();
                    cell.MoveToCell(emptyCell);
                    _anyCellMoved = true;
                }
            }
        }
    }
    private Cell FindCellToMerge(Cell cell, Vector2 derection)
    {
        int startX = cell.X + (int)derection.x;
        int startY = cell.Y - (int)derection.y;

        for (int x = startX, y = startY;
           x >= 0 && x < _fieldSize && y >= 0 && y < _fieldSize;
           x += (int)derection.x, y -= (int)derection.y)
        {
            if (_field[x, y].IsEmpty)
                continue;

            if (_field[x, y].Value == cell.Value && !_field[x, y].HasMerged)
                return _field[x, y];
            break;
        }
        return null;
    }
    private Cell FindEmptyCell(Cell cell, Vector2 derection)
    {
        Cell emptyCell = null;

        int startX = cell.X + (int)derection.x;
        int startY = cell.Y - (int)derection.y;

        for (int x = startX, y = startY;
           x >= 0 && x < _fieldSize && y >= 0 && y < _fieldSize;
           x += (int)derection.x, y -= (int)derection.y)
        {
            if (_field[x, y].IsEmpty)
                emptyCell = _field[x, y];
            else
                break;
        }
        return emptyCell;
    }
    private void CheckGameResult()
    {
        bool lose = true;

        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                //if(_field[x, y].Value == Cell.MaxValue)
                //{
                //    GameConroller.Instance.Win();
                //    return;
                //}
                if( lose &&
                    _field[x, y].IsEmpty ||
                    FindCellToMerge(_field[x, y], Vector2.right) ||
                    FindCellToMerge(_field[x, y], Vector2.left) ||
                    FindCellToMerge(_field[x, y], Vector2.up) ||
                    FindCellToMerge(_field[x, y], Vector2.down))
                {
                    lose = false;
                }
            }
        }
        if (lose)
        {
            GameConroller.Instance.Lose();
        }
    }
    private void CreateField()
    {        
        _field = new Cell[_fieldSize, _fieldSize];
        float fieldWidth = _fieldSize * (_cellSize + _spacing) + _spacing;
        _rectTransform.sizeDelta = new Vector2(fieldWidth, fieldWidth);

        float startX = -(fieldWidth / 2) + (_cellSize / 2) + _spacing;
        float startY = (fieldWidth / 2) - (_cellSize / 2) - _spacing;

        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                var cell = Instantiate(_cellPref, transform, false);
                var position = new Vector2(startX + (x * (_cellSize + _spacing)), startY - (y * (_cellSize + _spacing)));

                cell.transform.localPosition = position;
                _field[x, y] = cell;
                cell.SetValue(x, y, 0);
            }
        }
    }
    public void GenerteFiled()
    {        
        if (_field == null)
            CreateField();        

            for (int x = 0; x < _fieldSize; x++)
                for (int y = 0; y < _fieldSize; y++)
                    _field[x, y].SetValue(x, y, 0);

            for (int i = 0; i < _initCellCount; i++)
                GenerateRandomCell();
    }
    private void GenerateRandomCell()
    {
        var emptyCell = new List<Cell>();

        for (int x = 0; x < _fieldSize; x++)
            for (int y = 0; y < _fieldSize; y++)
                if (_field[x, y].IsEmpty)
                    emptyCell.Add(_field[x, y]);
        if(emptyCell.Count == 0)
             throw new System.Exception("There is no any empty cell");

        int value = Random.Range(0, 10) == 0 ? 2 : 1;

        var cell = emptyCell[Random.Range(0, emptyCell.Count)];
        cell.SetValue(cell.X, cell.Y, value, false);

        CellAnimationController.Instance.SmoothAppear(cell);
    }
    private void ResetCellsFlags()
    {
        for (int x = 0; x < _fieldSize; x++)
            for (int y = 0; y < _fieldSize; y++)
                _field[x, y].ResetFlags();
    }
}
