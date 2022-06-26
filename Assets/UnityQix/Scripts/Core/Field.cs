using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// フィールド情報を保持する。
/// x, y 座標のいずれかが偶数の場合：ライン又は非ライン
/// x, y 座標のいずれも奇数の場合：占有又は非占有
/// </summary>
public class Field
{
    EnumBlockType[,] _field;
    /// <summary>
    /// .: 非占有
    /// $: 占有
    /// o: 非ライン
    /// #: ライン
    /// </summary>
    /// <param name="source">フィールドを示す文字列</param>
    public Field(string source)
    {
        source = source.Trim().Replace("\r", "\n").Replace("\n\n", "\n");
        Dictionary<char, EnumBlockType> dic = new Dictionary<char, EnumBlockType>();
        dic['.'] = EnumBlockType.FreeArea;
        dic['o'] = EnumBlockType.NoLine;
        dic['$'] = EnumBlockType.OccupiedArea;
        dic['#'] = EnumBlockType.OnLine;
        string[] lines = source.Split('\n');
        if (lines.Any(s => s.Length != lines[0].Length))
        {
            // 全てのラインは同じ長さでなければならない
            Debug.LogError($"All length of lines must be same!\n{source}\n{string.Join(',', lines.Select(s => $"{s.Length}"))}");
            throw new ArgumentException();
        }
        _field = new EnumBlockType[lines[0].Length, lines.Length];
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                _field[x, y] = dic[lines[y][x]];
                if (x % 2 == 1 && y % 2 == 1)
                {
                    // このエリアは非占有又は占有のはず
                    if (_field[x, y] == EnumBlockType.OnLine || _field[x, y] == EnumBlockType.NoLine)
                    {
                        Debug.LogError($"({x}, {y}) must be Occupied or FreeArea");
                        throw new ArgumentException();
                    }
                }
                else
                {
                    // このエリアは非占有又はラインのはず
                    if (_field[x, y] == EnumBlockType.OccupiedArea || _field[x, y] == EnumBlockType.FreeArea)
                    {
                        Debug.LogError($"({x}, {y}) must be OnLine or NoLine");
                        throw new ArgumentException();
                    }

                }
                if (x == 0 || x == lines[0].Length - 1 || y == 0 || y == lines.Length - 1)
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

    internal void UpdateField(EnumBlockType[,] newField)
    {
        if (_field.GetLength(0) != newField.GetLength(0) || _field.GetLength(1) != newField.GetLength(1))
        {
            throw new ArgumentException();
        }
        Array.Copy(newField, _field, _field.Length);
    }

    public EnumBlockType[,] Copy()
    {
        EnumBlockType[,] field = new EnumBlockType[_field.GetLength(0), _field.GetLength(1)];
        Array.Copy(_field, field, _field.Length);
        return field;
    }

    public Field(int width, int height)
    {
        if (width < 3 || height < 3)
        {
            Debug.LogError($"({width}, {height}) Too small field!");
            throw new ArgumentException();
        }
        if (width % 2 == 0 || height % 2 == 0)
        {
            Debug.LogError($"({width}, {height}) Width / Height must be odd. ");
            throw new ArgumentException();

        }
        _field = new EnumBlockType[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    _field[x, y] = EnumBlockType.OnLine;
                }
                else if (x % 2 == 0 || y % 2 == 0)
                {
                    _field[x, y] = EnumBlockType.NoLine;
                }
                else
                {
                    _field[x, y] = EnumBlockType.FreeArea;
                }
            }
        }
    }

    public Field(EnumBlockType[,] field)
    {
        _field = new EnumBlockType[field.GetLength(0), field.GetLength(1)];
        Array.Copy(field, _field, field.Length);
    }

    /// <summary>
    /// フィールド上の指定された座標のエリアの種別を返す
    /// </summary>
    /// <param name="x">x座標</param>
    /// <param name="y">y座標</param>
    /// <returns>エリアの種別</returns>
    public EnumBlockType AreaType(int x, int y)
    {
        return _field[x, y];
    }

    /// <summary>
    /// フィールドを更新する
    /// </summary>
    /// <param name="x">更新位置(x)</param>
    /// <param name="y">更新位置(y)</param>
    /// <param name="type">更新値</param>
    public void SetAreaType(int x, int y, EnumBlockType type)
    {
        _field[x, y] = type;
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
    /// フィールド幅
    /// フィールド上のx座標が偶数の時にラインが設置されているので、フィールド幅は必ず奇数となる
    /// </summary>
    /// <returns></returns>
    public int Width()
    {
        return _field.GetLength(0);
    }

    public string DebugField()
    {
        string ret = "";
        Dictionary<EnumBlockType, string> dic = new Dictionary<EnumBlockType, string>();
        dic[EnumBlockType.FreeArea] = ".";
        dic[EnumBlockType.NoLine] = "o";
        dic[EnumBlockType.OccupiedArea] = "$";
        dic[EnumBlockType.OnLine] = "#";
        for (int y = 0; y < Height(); y++)
        {
            for (int x = 0; x < Width(); x++)
            {
                ret += dic[_field[x, y]];
            }
            ret += "\r\n";
        }
        return ret;
    }
}
