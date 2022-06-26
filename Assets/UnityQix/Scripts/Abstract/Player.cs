using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    private IInputManager _input;
    private Field _field;
    private int _pX, _pY; 

    private bool _onBase;

    public abstract IEnumerator MoveTo(int x, int y);
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
        while(true)
        {
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
            if((px2 != _pX || py2 != _pY) && px2 >= 0 && py2 >= 0 && px2 < _field.Width() && py2 < _field.Height())
            {
                yield return MoveTo(_pX, _pY);
            } else 
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

        AreaCalculator calc = new AreaCalculator();
        Edge edge = new Edge(_field);
        if(edge.EdgeType(_pX, _pY) != EnumEdgeType.Edge)
        {
            Debug.LogError($"Player must be in edge({_pX}, {_pY})");
            throw new ArgumentException();
        }

        _onBase = true; 
    }
}
