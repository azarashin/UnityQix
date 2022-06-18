using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line
{
    int[,] _lineField;

    int IdEmpty = -1;
    int IdLeft = 1;
    int IdRight = 2;
    int IdUp = 4;
    int IdDown = 8;
    public Line(Edge edge)
    {
        _lineField = new int[edge.Width(), edge.Height()]; 
        for(int y=0;y<edge.Height();y++)
        {
            for (int x = 0; x < edge.Width(); x++)
            {
                if(edge.EdgeType(x, y) == EnumEdgeType.None)
                {
                    _lineField[x, y] = IdEmpty; 
                    continue; 
                }
                int val = 0; 
                if(x > 0 && edge.EdgeType(x - 1, y) != EnumEdgeType.None)
                {
                    val += IdLeft;
                }
                if (x < edge.Width() - 1 && edge.EdgeType(x + 1, y) != EnumEdgeType.None)
                {
                    val += IdRight;
                }
                if (y > 0 && edge.EdgeType(x, y - 1) != EnumEdgeType.None)
                {
                    val += IdUp;
                }
                if (y < edge.Height() - 1 && edge.EdgeType(x, y + 1) != EnumEdgeType.None)
                {
                    val += IdDown;
                }
                _lineField[x, y] = val; 
            }
        }
    }

    /// <summary>
    /// フィールド高さ
    /// フィールド上のy座標が偶数の時にラインが設置されているので、フィールド高さは必ず奇数となる
    /// </summary>
    /// <returns></returns>
    public int Height()
    {
        return _lineField.GetLength(1);
    }

    /// <summary>
    /// フィールド幅
    /// フィールド上のx座標が偶数の時にラインが設置されているので、フィールド幅は必ず奇数となる
    /// </summary>
    /// <returns></returns>
    public int Width()
    {
        return _lineField.GetLength(0);
    }

    /// <summary>
    /// 指定された座標におけるラインIDを返す。
    /// ラインIDの下位0ビット目が1の場合：左にラインが伸びている
    /// ラインIDの下位1ビット目が1の場合：右にラインが伸びている
    /// ラインIDの下位2ビット目が1の場合：上にラインが伸びている
    /// ラインIDの下位3ビット目が1の場合：下にラインが伸びている
    /// </summary>
    /// <param name="x">x座標</param>
    /// <param name="y">y座標</param>
    /// <returns>ラインID</returns>
    public int LineID(int x, int y)
    {
        return _lineField[x, y];
    }
    public string DebugLine()
    {
        string ret = "";
        for (int y = 0; y < Height(); y++)
        {
            for (int x = 0; x < Width(); x++)
            {
                if(_lineField[x, y] == IdEmpty)
                {
                    ret += $".";
                }
                else
                {
                    ret += $" {_lineField[x, y]:X}".Last();
                }
            }
            ret += "\r\n";
        }
        return ret;
    }
}
