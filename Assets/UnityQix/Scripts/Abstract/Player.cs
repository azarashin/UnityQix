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
    private int _id; 
    private int _pX, _pY;
    private int _lives;
    private int _units;
    private int _initialLives;
    private float _invisibleTime;
    private bool _deadTrigger = false;
    private bool _rebornTrigger = false; 

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

    /// <summary>
    /// ダメージを受けた後の無敵時間(秒)
    /// </summary>
    /// <returns>無敵時間(秒)</returns>
    public abstract float InivsibleTime();

    /// <summary>
    /// ライフがゼロになって残機を減らし、再出場する時の処理。この処理の間入力を受け付けない。
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator CoDamagedThenReborn();

    /// <summary>
    /// ライフがゼロになって残機を減らし、残機がゼロになった時の処理。この処理の間入力を受け付けない。
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator CoDamagedThenDisappear();

    /// <summary>
    /// 入力を受け付けるためのIInputManager クラスのインスタンスを取得する
    /// </summary>
    /// <returns>IInputManager クラスのインスタンス</returns>
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
                int ox = _pX;
                int oy = _pY;
                EnumBlockType front = _field.AreaType((_pX + px2) / 2, (_pY + py2) / 2);
                EnumBlockType frontStep = _field.AreaType(px2, py2);
                if(front == EnumBlockType.NoLine && frontStep == EnumBlockType.NoLine)
                {
                    _field.SetAreaType((_pX + px2) / 2, (_pY + py2) / 2, EnumBlockType.OnLineDrawing);
                    _field.SetAreaType(px2, py2, EnumBlockType.OnLineDrawing);
                } else if (front == EnumBlockType.NoLine && frontStep == EnumBlockType.OnLine)
                {
                    _field.SetAreaType((_pX + px2) / 2, (_pY + py2) / 2, EnumBlockType.OnLineDrawing);
                    _field.SetAreaType(px2, py2, EnumBlockType.ConnectedPoint);
                } else if (front == EnumBlockType.OnLineDrawing)
                {
                    _field.SetAreaType(_pX, _pY, EnumBlockType.NoLine);
                    _field.SetAreaType((_pX + px2) / 2, (_pY + py2) / 2, EnumBlockType.NoLine);
                }
                _calc.UpdateField(_field, _id);
                _pX = px2;
                _pY = py2;
                yield return CoMoveTo(ox, oy, _pX, _pY);
            }
            else
            {
                yield return null;
            }
            if(_deadTrigger)
            {
                yield return CoDamagedThenDisappear();
                _deadTrigger = false;

            }
            if(_rebornTrigger)
            {
                yield return CoDamagedThenReborn();
                _rebornTrigger = false; 
            }
        }

    }

    internal void Setup(Field field, int x, int y, AreaCalculator calc, int lives, int units, int id)
    {
        _input = GetInput();

        _field = field;
        _pX = x;
        _pY = y;
        _calc = calc;
        _pos = new PositionUpdater(_field);
        _initialLives = _lives = lives;
        _units = units;
        _id = id; 
        _invisibleTime = 0.0f; 
        _deadTrigger = false;

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

    public void Damage()
    {
        if(_lives <= 0 || _units <= 0)
        {
            return; 
        }
        if(IsInvisible())
        {
            return; 
        }
        _lives--;
        if(_lives <= 0)
        {
            _units--;
            if(_units <= 0)
            {
                _deadTrigger = true; 
            }
            else
            {
                _rebornTrigger = true; 
                _lives = _initialLives;
            }
        }
        else
        {
            _invisibleTime = InivsibleTime();
        }
    }

    public int Lives
    {
        get
        {
            return _lives; 
        }
    }

    public int Units
    {
        get
        {
            return _units; 
        }
    }

    public bool IsInvisible()
    {
        return (_invisibleTime > 0.0f || _units == 0 || _rebornTrigger || _deadTrigger);
    }
}
