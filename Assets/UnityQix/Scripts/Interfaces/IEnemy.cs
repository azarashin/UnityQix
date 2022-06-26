using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

    public (int, int) LogicalPosition();
    public int Radius(); 
}
