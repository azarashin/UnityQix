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
            if (_input.IsLeft())
            {
                yield return MoveTo(_pX, _pY);
            }
            yield return null; 
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
