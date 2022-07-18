using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFactory : Factory
{
    [SerializeField]
    PlayerForTest[] _players;

    public override int GetNumberOfPlayers()
    {
        return 1;
    }

    public override Player GetPlayer(int index)
    {
        return _players[index];
    }
}
