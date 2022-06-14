using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaCalculator
{
    /// <summary>
    /// フィールドの面積を求める
    /// </summary>
    /// <param name="field">フィールド</param>
    /// <returns>面積</returns>
    public int NumberOfAllPoints(Field field)
    {
        return (field.Height() - 1) * (field.Width() - 1) / 4;
    }

    /// <summary>
    /// フィールドのうち占有している部分の面積を求める
    /// </summary>
    /// <param name="field">フィールド</param>
    /// <returns>占有している部分の面積</returns>
    public int NumberOfOccupiedPoints(Field field)
    {
        return Enumerable
            .Range(0, (field.Height() - 1) / 2)
            .Sum(y =>
                Enumerable.Range(0, (field.Width() - 1) / 2)
                .Where(x => field.BlockType(x * 2 + 1, y * 2 + 1) == EnumBlockType.Occupied)
                .Count()
                );
    }

    /// <summary>
    /// フィールドの占有・空白状態を更新する
    /// </summary>
    /// <param name="field">フィールド</param>
    /// <param name="targets"></param>
    /// <returns>更新されたフィールド</returns>
    public Field UpdateField(Field field, (int X, int Y)[] targets)
    {
        for(int y = 1;y < field.Height(); y += 2)
        {
            for (int x = 1; x < field.Width(); x += 2)
            {
                field.UpdateBlock(EnumBlockType.Occupied, x, y); 
            }
        }
        foreach((int X, int Y) target in targets)
        {
            field = Fill(field, target);
        }
        return field; 
    }

    /// <summary>
    /// 指定された点を起点としてフィールドを空白で塗りつぶしていく
    /// </summary>
    /// <param name="field">フィールド</param>
    /// <param name="target">指定された点</param>
    /// <returns>更新されたフィールド</returns>
    private Field Fill(Field field, (int X, int Y) target)
    {
        List<(int x, int y)> tasks = new List<(int x, int y)>();
        tasks.Add(target);
        while (tasks.Count() > 0)
        {
            var task = tasks[0];
            tasks.RemoveAt(0);
            if (task.x > 0 && task.y > 0 && task.x < field.Width() - 1 && task.y < field.Height() - 1) // フィールドの外部や端を塗ろうとしていたら何もしない。
            {
                (tasks, field) = AddTask(field, tasks, task);
            }
        }
        return field;
    }

    /// <summary>
    /// フィールドのあるマスを空白にし、その周りのマスを空白にするようなタスクを追加する。
    /// そのマスの周りがラインである場合はその先のマスを空白にしない
    /// </summary>
    /// <param name="field">フィールド</param>
    /// <param name="tasks">タスク一覧</param>
    /// <param name="task">これから空白にしようとするマスの座標を持つタスク</param>
    /// <returns>(更新されたタスク一覧, 更新されたフィールド)</returns>
    private (List<(int x, int y)>, Field) AddTask(Field field, List<(int x, int y)> tasks, (int x, int y) task)
    {
        field.UpdateBlock(EnumBlockType.Free, task.x, task.y); 
        if(field.BlockType(task.x - 1, task.y) == EnumBlockType.Free && field.BlockType(task.x - 2, task.y) == EnumBlockType.Occupied)
        {
            tasks.Add((task.x - 2, task.y));
        }
        if (field.BlockType(task.x + 1, task.y) == EnumBlockType.Free && field.BlockType(task.x + 2, task.y) == EnumBlockType.Occupied)
        {
            tasks.Add((task.x + 2, task.y));
        }
        if (field.BlockType(task.x, task.y - 1) == EnumBlockType.Free && field.BlockType(task.x, task.y - 2) == EnumBlockType.Occupied)
        {
            tasks.Add((task.x, task.y - 2));
        }
        if (field.BlockType(task.x, task.y + 1) == EnumBlockType.Free && field.BlockType(task.x, task.y + 2) == EnumBlockType.Occupied)
        {
            tasks.Add((task.x, task.y + 2));
        }
        return (tasks, field);
    }
}
