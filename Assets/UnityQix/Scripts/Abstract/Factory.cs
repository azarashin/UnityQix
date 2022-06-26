using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    public abstract Player GetPlayer(int index);

    public abstract int GetNumberOfPlayers();

}
