using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumBlockType
{
    /// <summary>
    /// 空白
    /// </summary>
    FreeArea, 

    /// <summary>
    /// ライン
    /// </summary>
    OnLine,

    /// <summary>
    /// ライン無し
    /// </summary>
    NoLine,

    /// <summary>
    /// 占有
    /// </summary>
    OccupiedArea,
    OnLineDrawing,
}
