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

    /// <summary>
    /// 描画中
    /// </summary>
    OnLineDrawing,

    /// <summary>
    /// 描画が完了したポイント
    /// </summary>
    ConnectedPoint,
}
