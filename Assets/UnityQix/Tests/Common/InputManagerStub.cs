using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerStub : IInputManager
{
    bool _left, _right, _up, _down, _slow;

    public bool IsDown()
    {
        return _down;
    }

    public bool IsLeft()
    {
        return _left;
    }

    public bool IsRight()
    {
        return _right;
    }

    public bool IsUp()
    {
        return _up;
    }

    public bool IsSlow()
    {
        return _slow;
    }


    public void SetState(bool left, bool right, bool up, bool down)
    {
        _left = left;
        _right = right;
        _up = up;
        _down = down;
    }

    public void SetSlowMode(bool slow)
    {
        _slow = slow;
    }

}
