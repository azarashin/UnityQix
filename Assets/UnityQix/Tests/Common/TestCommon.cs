using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestCommon
{
    public static Factory Factory()
    {
        GameObject obj = GameObject.Instantiate((GameObject)Resources.Load("TestFactory"));
        return obj.GetComponent<Factory>(); 
    }
}
