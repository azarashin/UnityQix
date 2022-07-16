using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForTest : Player
{
    private IInputManager _input;

    public int Px { get; private set; }
    public int Py { get; private set; }

    public PlayerForTest()
    {
        _input = new InputManagerStub();
        Px = -1;
        Py = -1;
    }

    public override IInputManager GetInput()
    {
        return _input;
    }

    protected override IEnumerator CoMoveTo(int ox, int oy, int x, int y)
    {
        Px = x;
        Py = y; 
        yield return null; 
    }
    {
        yield return null;
    }
}
