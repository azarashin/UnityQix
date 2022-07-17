using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdater
{
    private Field _field;
    public PositionUpdater(Field field)
    {
        _field = field;
    }

    /// <summary>
    /// その地点から他の地点に移動可能であるかどうかを判定する。
    /// 判定時点でのフィールドはゲーム中に存在し得る状態のものとする。
    /// 判定に必要な計算時間は多め。
    /// </summary>
    /// <param name="x">判定対象の地点のx座標</param>
    /// <param name="y">判定対象の地点のy座標</param>
    /// <param name="acceptToMoveOnLine">境界線以外の線の上も動ける場合はtrue</param>
    /// <returns></returns>
    public bool IsMovablePointWithHeavyAlgorithm(int x, int y, bool acceptToMoveOnLine = false)
    {
        return
            acceptToMoveOnLine ||
            IsMovable(x, y, x + 2, y + 0, acceptToMoveOnLine) ||
            IsMovable(x, y, x - 2, y + 0, acceptToMoveOnLine) ||
            IsMovable(x, y, x + 0, y + 2, acceptToMoveOnLine) ||
            IsMovable(x, y, x + 0, y - 2, acceptToMoveOnLine);
    }

    /// <summary>
    /// その地点から他の地点に移動可能であるかどうかを判定する。
    /// 判定時点でのフィールドはゲーム中に存在し得る状態のものとする。
    /// 判定に必要な計算時間は少な目。
    /// </summary>
    /// <param name="x0">判定対象の地点のx座標</param>
    /// <param name="y0">判定対象の地点のy座標</param>
    /// <param name="acceptToMoveOnLine">境界線以外の線の上も動ける場合はtrue</param>
    /// <returns></returns>
    public bool IsMovablePointWithLightAlgorithm(int x0, int y0, bool acceptToMoveOnLine = false)
    {
        if (x0 < 0 || y0 < 0 || x0 >= _field.Width() || y0 >= _field.Height())
        {
            return false;
        }
        if (x0 % 2 == 1 || y0 % 2 == 1)
        {
            return false;
        }
        // 対象の座標が線を引ける場所でかつ境界線以外の線の上も動ける場合は、移動可能であると判定する。
        if (acceptToMoveOnLine)
        {
            return true; 
        }

        EnumBlockType upLeft = SafeBlockType(x0 - 1, y0 - 1);
        EnumBlockType upRight = SafeBlockType(x0 + 1, y0 - 1);
        EnumBlockType downLeft = SafeBlockType(x0 - 1, y0 + 1);
        EnumBlockType downRight = SafeBlockType(x0 + 1, y0 + 1);


        // 斜め方向に隣接するいずれかのエリアが占有されていなければ移動可能

        return upLeft == EnumBlockType.FreeArea
            || upRight == EnumBlockType.FreeArea
            || downLeft == EnumBlockType.FreeArea
            || downRight == EnumBlockType.FreeArea;

    }

    private EnumBlockType SafeBlockType(int x, int y)
    {
        if (x >= 0 && x < _field.Width() && y >= 0 && y < _field.Height())
        {
            return _field.AreaType(x, y);
        }
        return EnumBlockType.OccupiedArea; 

    }

    private EnumBlockType SafeAreaType(int x, int y)
    {
        if (x >= 0 && x < _field.Width() && y >= 0 && y < _field.Height())
        {
            return _field.AreaType(x, y);
        }
        return EnumBlockType.NoLine;

    }

    public bool IsMovable(int x0, int y0, int x1, int y1, bool acceptToMoveOnLine = false)
    {
        if (x0 < 0 || y0 < 0 || x0 >= _field.Width() || y0 >= _field.Height()
        || x1 < 0 || y1 < 0 || x1 >= _field.Width() || y1 >= _field.Height())
        {
            return false;
        }
        if (x0 % 2 == 1 || y0 % 2 == 1 || x1 % 2 == 1 || y1 % 2 == 1)
        {
            return false;
        }
        if (!(x0 == x1 && Mathf.Abs(y0 - y1) == 2
            || y0 == y1 && Mathf.Abs(x0 - x1) == 2))
        {
            return false;
        }
        int x = (x0 + x1) / 2;
        int y = (y0 + y1) / 2;

        int dx = x - x0;
        int dy = y - y0;
        (int dx0, int dy0, int dx1, int dy1) = SideForward(dx, dy);
        EnumBlockType sideA = SafeBlockType(x0 + dx0, y0 + dy0);
        EnumBlockType sideB = SafeBlockType(x0 + dx1, y0 + dy1);
        EnumBlockType front = SafeAreaType(x, y);
        EnumBlockType frontStep = SafeAreaType(x1, y1);

        if(front == EnumBlockType.OnLineDrawing)
        {
            return (NumberOfWays(x0, y0) == 0); // 後戻りするのであればOK
        }
        if (sideA == EnumBlockType.FreeArea
            && sideB == EnumBlockType.FreeArea)
        {
            return ((acceptToMoveOnLine || front == EnumBlockType.NoLine || NumberOfWays(x0, y0) == 1) && frontStep != EnumBlockType.OnLineDrawing);
        }

        if (sideA == EnumBlockType.OccupiedArea
            && sideB == EnumBlockType.OccupiedArea)
        {
            return false;
        }
        return true;
    }

    public int NumberOfWays(int x, int y)
    {
        return
            (SafeAreaType(x - 1, y + 0) == EnumBlockType.OnLine ? 1 : 0) +
            (SafeAreaType(x + 1, y + 0) == EnumBlockType.OnLine ? 1 : 0) +
            (SafeAreaType(x + 0, y - 1) == EnumBlockType.OnLine ? 1 : 0) +
            (SafeAreaType(x + 0, y + 1) == EnumBlockType.OnLine ? 1 : 0);

    }

    public (int dx0, int dy0, int dx1, int dy1) SideForward(int dx, int dy)
    {
        if (dx < -1 || dx > 1 || dy < -1 || dy > 1)
        {
            Debug.LogError($"({dx}, {dy}): dx, dy must be -1, 0 or 1!");
            throw new ArgumentException();
        }
        if (!((dx == 0 && dy != 0) || (dy == 0 && dx != 0)))
        {
            Debug.LogError($"({dx}, {dy}): Either dx or dy must be 0!");
            throw new ArgumentException();
        }
        return (dx + dy, dy + dx, dx - dy, dy - dx);
    }
}
