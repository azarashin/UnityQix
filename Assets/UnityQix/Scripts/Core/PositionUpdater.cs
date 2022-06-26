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

    public bool IsMovable(int x0, int y0, int x1, int y1, bool acceptToMoveOnLine = false)
    {
        if(x0 < 0 || y0 < 0 || x0 >= _field.Width() || y0 >= _field.Height()
        || x1 < 0 || y1 < 0 || x1 >= _field.Width() || y1 >= _field.Height())
        {
            Debug.LogError($"({x0}, {y0}) => ({x1}, {y1}): Out of field! ");
            throw new ArgumentException();
        }
        if(x0 % 2 == 1 || y0 % 2 == 1 || x1 % 2 == 1 || y1 % 2 == 1)
        {
            Debug.LogError($"({x0}, {y0}) => ({x1}, {y1}): Invalid position! ");
            throw new ArgumentException();
        }
        if(!( x0 == x1 && Mathf.Abs(y0 - y1) == 2
            || y0 == y1 && Mathf.Abs(x0 - x1) == 2))
        {
            Debug.LogError($"({x0}, {y0}) => ({x1}, {y1}): Player can not step over 2 line tiles at once! ");
            throw new ArgumentException();
        }
        int x = (x0 + x1) / 2;
        int y = (y0 + y1) / 2;
        int dx = x - x0;
        int dy = y - y0;
        (int dx0, int dy0, int dx1, int dy1) = SideForward(dx, dy); 
        int xA = x0 + dx0;
        int yA = y0 + dy0;
        int xB = x0 + dx1;
        int yB = y0 + dy1;
        EnumBlockType sideA = EnumBlockType.OccupiedArea;
        EnumBlockType sideB = EnumBlockType.OccupiedArea;
        EnumBlockType front = EnumBlockType.NoLine;
        if(xA >= 0 && xA < _field.Width() && yA >= 0 && yA < _field.Height())
        {
            sideA = _field.AreaType(xA, yA); 
        }
        if(xB >= 0 && xB < _field.Width() && yB >= 0 && yB < _field.Height())
        {
            sideB = _field.AreaType(xB, yB); 
        }
        if(x >= 0 && x < _field.Width() && y >= 0 && y < _field.Height())
        {
            front = _field.AreaType(x, y); 
        }
        Debug.Log($"dx: {dx}, dy:{dy}, dx0: {dx0}, dy0:{dy0}, dx1: {dx1}, dy1:{dy1}");
        Debug.Log(sideA);
        Debug.Log(sideB);
        Debug.Log(front);
        if(sideA == EnumBlockType.FreeArea 
            && sideB == EnumBlockType.FreeArea) 
        {
            return (acceptToMoveOnLine || front == EnumBlockType.NoLine); 
        }

        if(sideA == EnumBlockType.OccupiedArea 
            && sideB == EnumBlockType.OccupiedArea) 
        {
            return false; 
        }
        return true; 
    }

    public (int dx0, int dy0, int dx1, int dy1) SideForward(int dx, int dy)
    {
        if(dx < -1 || dx > 1 || dy < -1 || dy > 1)
        {
            Debug.LogError($"({dx}, {dy}): dx, dy must be -1, 0 or 1!");
            throw new ArgumentException();
        }
        if(!((dx == 0 && dy != 0) || (dy == 0 && dx != 0)))
        {
            Debug.LogError($"({dx}, {dy}): Either dx or dy must be 0!");
            throw new ArgumentException();
        }
        return (dx + dy, dy + dx, dx - dy, dy - dx); 
    }
}
