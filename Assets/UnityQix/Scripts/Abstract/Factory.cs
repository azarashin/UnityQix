using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    [SerializeField]
    GameController _controller;

    public abstract Player GetPlayer(int index);

    public abstract int GetNumberOfPlayers();

    public GameController GetGameController()
    {
        return _controller; 
    }

}
