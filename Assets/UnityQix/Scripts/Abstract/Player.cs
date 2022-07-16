using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField]
    bool _acceptToMoveOnLine = false;

    private IInputManager _input;
    private Field _field;
    private AreaCalculator _calc;
    private PositionUpdater _pos;
    private int _pX, _pY;
    private int _lives;
    private int _units;
    private int _initialLives;

    private bool _onBase;

    /// <summary>
    /// 移動するときの経過処理。移動経過中は移動入力を受け付けない。
    /// </summary>
    /// <param name="ox">移動元x座標</param>
    /// <param name="oy">移動元y座標</param>
    /// <param name="x">移動先x座標</param>
    /// <param name="y">移動先y座標</param>
    /// <returns></returns>
    protected abstract IEnumerator CoMoveTo(int ox, int oy, int x, int y);
    public abstract IInputManager GetInput();

    private void Start()
    {
        StartCoroutine(CoRun());
    }

    private void Update()
    {
    }

    private IEnumerator CoRun()
    {
        while (true)
        {
            if (_field == null)
            {
                yield return null;
                continue;
            }
            int px2 = _pX, py2 = _pY;
            if (_input.IsLeft())
            {
                px2 = _pX - 2;
            }
            else if (_input.IsRight())
            {
                px2 = _pX + 2;
            }
            else if (_input.IsUp())
            {
                py2 = _pY - 2;
            }
            else if (_input.IsDown())
            {
                py2 = _pY + 2;
            }
            if (_pos.IsMovable(_pX, _pY, px2, py2, _acceptToMoveOnLine))
            {
                _field.SetAreaType((_pX + px2) / 2, (_pY + py2) / 2, EnumBlockType.OnLine);
                _field.SetAreaType(px2, py2, EnumBlockType.OnLine);
                _calc.UpdateField(_field, new (int, int)[] { (5, 5) });
                _pX = px2;
                _pY = py2;
                yield return MoveTo(_pX, _pY);
            }
            else
            {
                yield return null;
            }
        }

    }

    public void Setup(Field field, int x, int y)
    {
        _input = GetInput();

        _field = field;
        _pX = x;
        _pY = y;
        _calc = new AreaCalculator();
        _pos = new PositionUpdater(_field);
        _initialLives = _lives = lives;
        _units = units;

        Edge edge = new Edge(_field);
        if (edge.EdgeType(_pX, _pY) != EnumEdgeType.Edge)
        {
            Debug.LogError($"Player must be in edge({_pX}, {_pY})");
            throw new ArgumentException();
        }

        _onBase = true;
    }

    public (int, int) Position()
    {
        return (_pX, _pY); 
    }

    public int Radius()
    {
        return 1; // プレイヤーの半径は固定
    }
}
