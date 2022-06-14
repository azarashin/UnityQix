using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// フィールド情報を保持する。
/// x, y 座標のいずれかが偶数の場合：ライン又は空白
/// x, y 座標のいずれも奇数の場合：占有又は空白
/// </summary>
public class Field
{
    EnumBlockType[,] _field; 
    /// <summary>
    /// .: 空白
    /// $: 占有
    /// #: ライン
    /// </summary>
    /// <param name="source">フィールドを示す文字列</param>
    public Field(string source)
    {
        source = source.Trim().Replace("\r", "\n").Replace("\n\n", "\n");
        Dictionary<char, EnumBlockType> dic = new Dictionary<char, EnumBlockType>();
        dic['.'] = EnumBlockType.Free;
        dic['$'] = EnumBlockType.Occupied;
        dic['#'] = EnumBlockType.OnLine;
        string[] lines = source.Split('\n');
        if(lines.Any(s => s.Length != lines[0].Length))
        {
            // 全てのラインは同じ長さでなければならない
            Debug.LogError($"All length of lines must be same!\n{source}\n{string.Join(',', lines.Select(s => $"{s.Length}"))}");
            throw new ArgumentException();
        }
        _field = new EnumBlockType[lines[0].Length, lines.Length]; 
        for(int y=0;y<lines.Length;y++)
        {
            for(int x=0;x<lines[y].Length;x++)
            {
                _field[x, y] = dic[lines[y][x]];
                if(x % 2 == 1 && y % 2 == 1)
                {
                    // 空白又は占有
                    if(_field[x,y] == EnumBlockType.OnLine)
                    {
                        Debug.LogError($"({x}, {y}) must be Occupied or Free");
                        throw new ArgumentException();
                    }
                } else
                {
                    // 空白又はライン
                    if (_field[x, y] == EnumBlockType.Occupied)
                    {
                        Debug.LogError($"({x}, {y}) must be OnLine or Free");
                        throw new ArgumentException();
                    }

                }
                if(x == 0 || x == lines[0].Length - 1 || y == 0 || y == lines.Length - 1)
                {
                    // ライン
                    if (_field[x, y] != EnumBlockType.OnLine)
                    {
                        Debug.LogError($"({x}, {y}) must be OnLine");
                        throw new ArgumentException();
                    }
                }
            }
        }
    }

    public Field(int width, int height)
    {
        if(width < 3 || height < 3)
        {
            Debug.LogError($"({width}, {height}) Too small field!");
            throw new ArgumentException();
        }
        if(width % 2 == 0 || height % 2 == 0)
        {
            Debug.LogError($"({width}, {height}) Width / Height must be odd. ");
            throw new ArgumentException();

        }
        _field = new EnumBlockType[width, height]; 
        for(int y=0;y<height;y++)
        {
            for (int x = 0; x < width; x++)
            {
                if(x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    _field[x, y] = EnumBlockType.OnLine;
                } else
                {
                    _field[x, y] = EnumBlockType.Free;
                }
            }
        }
    }

    /// <summary>
    /// フィールド上の指定された座標のブロックの種別を返す
    /// </summary>
    /// <param name="x">x座標</param>
    /// <param name="y">y座標</param>
    /// <returns>ブロックの種別</returns>
    public EnumBlockType BlockType(int x, int y)
    {
        return _field[x, y]; 
    }

    /// <summary>
    /// フィールド高さ
    /// フィールド上のy座標が偶数の時にラインが設置されているので、フィールド高さは必ず奇数となる
    /// </summary>
    /// <returns></returns>
    public int Height()
    {
        return _field.GetLength(1);
    }

    /// <summary>
    /// フィールド上の指定された座標のブロックを指定された種別に設定し直す
    /// </summary>
    /// <param name="type">再設定後のブロック種別</param>
    /// <param name="x">x座標</param>
    /// <param name="y">y座標</param>
    public void UpdateBlock(EnumBlockType type, int x, int y)
    {
        _field[x, y] = type;
    }

    /// <summary>
    /// フィールド幅
    /// フィールド上のx座標が偶数の時にラインが設置されているので、フィールド幅は必ず奇数となる
    /// </summary>
    /// <returns></returns>
    public int Width()
    {
        return _field.GetLength(0);
    }
}
