using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForTest : Player
{
    private IInputManager _input;

    public PlayerForTest()
    {
        _input = new InputManagerStub(); 
    }

    public override IInputManager GetInput()
    {
        return _input;
    }

    public override IEnumerator MoveTo(int x, int y)
    {
        yield return null; 
    }
}
