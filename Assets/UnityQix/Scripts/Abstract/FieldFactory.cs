using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FieldFactory : MonoBehaviour
{
    public abstract Field GenerateField(int stage);
    public abstract (int x, int y) GetInitialPosition(int stage, int index);
    public abstract int MaxPlayer(int stage); 
}
