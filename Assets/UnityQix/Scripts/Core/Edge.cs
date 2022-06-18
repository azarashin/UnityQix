﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    private EnumEdgeType[,] _edge; 

    public Edge(Field field)
    {
        _edge = new EnumEdgeType[field.Width(), field.Height()];
        for (int y = 0; y < field.Height(); y++)
        {
            for (int x = 0; x < field.Width(); x++)
            {
                _edge[x, y] =  EnumEdgeType.None;
            }
        }

        // エッジ部分を検出する
        for (int y = 0; y < field.Height(); y++)
        {
            for (int x = 0; x < field.Width(); x++)
            {
                if (x % 2 == 0 && y % 2 == 0)
                { // このフェーズでは確定できない線エリア
                    continue;
                }
                if (x % 2 == 1 && y % 2 == 1)
                { // 占有対象エリア
                    continue;
                }
                if (x % 2 == 0)
                {
                    if(x > 0 && x < field.Width() - 1)
                    {
                        bool a = field.AreaType(x - 1, y) == EnumBlockType.FreeArea;
                        bool b = field.AreaType(x + 1, y) == EnumBlockType.FreeArea;
                        if(field.AreaType(x, y) == EnumBlockType.NoLine)
                        {
                            _edge[x, y] = EnumEdgeType.None;
                        }
                        else if (a && b)
                        {
                            _edge[x, y] = EnumEdgeType.Line;
                        }
                        else if(!a && !b)
                        {
                            _edge[x, y] = EnumEdgeType.Filled;
                        }
                        else
                        {
                            _edge[x, y] = EnumEdgeType.Edge;
                        }
                    }
                    else if (x == 0)
                    {
                        if (field.AreaType(x + 1, y) == EnumBlockType.FreeArea)
                        {
                            _edge[x, y] = EnumEdgeType.Edge;
                        }
                        else
                        {
                            _edge[x, y] = EnumEdgeType.Filled;
                        }
                    }
                    else if(x == field.Width() - 1)
                    {
                        if (field.AreaType(x - 1, y) == EnumBlockType.FreeArea)
                        {
                            _edge[x, y] = EnumEdgeType.Edge;
                        }
                        else
                        {
                            _edge[x, y] = EnumEdgeType.Filled;
                        }
                    }
                }
                if (y % 2 == 0)
                {
                    if (y > 0 && y < field.Height() - 1)
                    {
                        bool a = field.AreaType(x, y - 1) == EnumBlockType.FreeArea;
                        bool b = field.AreaType(x, y + 1) == EnumBlockType.FreeArea;
                        if (field.AreaType(x, y) == EnumBlockType.NoLine)
                        {
                            _edge[x, y] = EnumEdgeType.None;
                        }
                        else if (a && b)
                        {
                            _edge[x, y] = EnumEdgeType.Line;
                        }
                        else if (!a && !b)
                        {
                            _edge[x, y] = EnumEdgeType.Filled;
                        }
                        else
                        {
                            _edge[x, y] = EnumEdgeType.Edge;
                        }
                    }
                    else if (y == 0)
                    {
                        if (field.AreaType(x, y + 1) == EnumBlockType.FreeArea)
                        {
                            _edge[x, y] = EnumEdgeType.Edge;
                        }
                        else
                        {
                            _edge[x, y] = EnumEdgeType.Filled;
                        }
                    }
                    else if (y == field.Height() - 1)
                    {
                        if (field.AreaType(x, y - 1) == EnumBlockType.FreeArea)
                        {
                            _edge[x, y] = EnumEdgeType.Edge;
                        }
                        else
                        {
                            _edge[x, y] = EnumEdgeType.Filled;
                        }
                    }
                }
            }
        }

        // 補間する
        for (int y = 0; y < field.Height(); y++)
        {
            for (int x = 0; x < field.Width(); x++)
            {
                if ((x % 2 == 1 && y % 2 == 0) || (x % 2 == 0 && y % 2 == 1))
                { // 確定している線エリア
                    continue; 
                }
                if (x % 2 == 1 && y % 2 == 1)
                { // 占有対象エリア
                    continue;
                }
                if (field.AreaType(x, y) == EnumBlockType.NoLine)
                {
                    _edge[x, y] = EnumEdgeType.None;
                    continue; 
                }
                EnumEdgeType up, down, left, right;
                up = down = left = right = EnumEdgeType.Filled; 
                if (x > 0)
                {
                    left = _edge[x - 1, y]; 
                }
                if (x < field.Width() - 1)
                {
                    right = _edge[x + 1, y];
                }
                if (y > 0)
                {
                    up = _edge[x, y - 1];
                }
                if (y < field.Height() - 1)
                {
                    down = _edge[x, y + 1];
                }
                if (left == EnumEdgeType.Edge || right == EnumEdgeType.Edge || up == EnumEdgeType.Edge || down == EnumEdgeType.Edge)
                {
                    _edge[x, y] = EnumEdgeType.Edge;
                }
                else if (left == EnumEdgeType.Line || right == EnumEdgeType.Line || up == EnumEdgeType.Line || down == EnumEdgeType.Line)
                {
                    _edge[x, y] = EnumEdgeType.Line;
                }
                else if (left == EnumEdgeType.Filled || right == EnumEdgeType.Filled || up == EnumEdgeType.Filled || down == EnumEdgeType.Filled)
                {
                    _edge[x, y] = EnumEdgeType.Filled;
                }
                else
                {
                    _edge[x, y] = EnumEdgeType.None;
                }
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
        return _edge.GetLength(1);
    }

    /// <summary>
    /// フィールド幅
    /// フィールド上のx座標が偶数の時にラインが設置されているので、フィールド幅は必ず奇数となる
    /// </summary>
    /// <returns></returns>
    public int Width()
    {
        return _edge.GetLength(0);
    }

    public string DebugEdge()
    {
        string ret = "";
        Dictionary<EnumEdgeType, string> dic = new Dictionary<EnumEdgeType, string>();
        dic[EnumEdgeType.None] = ".";
        dic[EnumEdgeType.Line] = "+";
        dic[EnumEdgeType.Edge] = "*";
        dic[EnumEdgeType.Filled] = "#";
        for (int y = 0; y < Height(); y++)
        {
            for (int x = 0; x < Width(); x++)
            {
                ret += dic[_edge[x, y]];
            }
            ret += "\r\n";
        }
        return ret;
    }
}
