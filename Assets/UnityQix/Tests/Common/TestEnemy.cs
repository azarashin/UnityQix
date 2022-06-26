using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : IEnemy
{
    private int _pX, _pY;
    private int _radius;


    public TestEnemy(int x, int y, int radius)
    {
        _pX = x;
        _pY = y;
        _radius = radius;

    }

    public (int, int) LogicalPosition()
    {
        return (_pX, _pY); 
    }

    public int Radius()
    {
        return _radius; 
    }

    public void SetPosition(int x, int y, int radius)
    {
        _pX = x;
        _pY = y;
        _radius = radius; 
    }
}
