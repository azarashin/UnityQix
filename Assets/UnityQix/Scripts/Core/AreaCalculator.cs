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
                .Where(x => field.AreaType(x * 2 + 1, y * 2 + 1) == EnumBlockType.OccupiedArea)
                .Count()
                );
    }

    /// <summary>
    /// フィールドの占有・空白状態を更新する
    /// </summary>
    /// <param name="field">フィールド</param>
    /// <param name="targets"></param>
    public void UpdateField(Field field, (int X, int Y)[] targets)
    {
        EnumBlockType[,] newField = field.Copy();
        Field ret = null;
        for (int y = 1; y < field.Height(); y += 2)
        {
            for (int x = 1; x < field.Width(); x += 2)
            {
                newField[x, y] = EnumBlockType.OccupiedArea;
            }
        }
        foreach ((int X, int Y) target in targets)
        {
            Fill(newField, target);
        }
        field.UpdateField(newField);
    }

    /// <summary>
    /// 指定された点を起点としてフィールドを空白で塗りつぶしていく
    /// </summary>
    /// <param name="field">フィールド</param>
    /// <param name="target">指定された点</param>
    /// <returns>更新されたフィールド</returns>
    private Field Fill(EnumBlockType[,] newField, (int X, int Y) target)
    {
        int width = newField.GetLength(0);
        int height = newField.GetLength(1);
        List<(int x, int y)> tasks = new List<(int x, int y)>();
        tasks.Add(target);
        while (tasks.Count() > 0)
        {
            var task = tasks[0];
            tasks.RemoveAt(0);
            if (task.x > 0 && task.y > 0 && task.x < width - 1 && task.y < height - 1) // フィールドの外部や端を塗ろうとしていたら何もしない。
            {
                AddTask(newField, tasks, task);
            }
        }
        return new Field(newField);
    }

    /// <summary>
    /// フィールドのあるマスを空白にし、その周りのマスを空白にするようなタスクを追加する。
    /// そのマスの周りがラインである場合はその先のマスを空白にしない
    /// </summary>
    /// <param name="field">フィールド</param>
    /// <param name="tasks">タスク一覧</param>
    /// <param name="task">これから空白にしようとするマスの座標を持つタスク</param>
    /// <returns>(更新されたタスク一覧, 更新されたフィールド)</returns>
    private void AddTask(EnumBlockType[,] newField, List<(int x, int y)> tasks, (int x, int y) task)
    {
        newField[task.x, task.y] = EnumBlockType.FreeArea;
        if (newField[task.x - 1, task.y] == EnumBlockType.NoLine && newField[task.x - 2, task.y] == EnumBlockType.OccupiedArea)
        {
            tasks.Add((task.x - 2, task.y));
        }
        if (newField[task.x + 1, task.y] == EnumBlockType.NoLine && newField[task.x + 2, task.y] == EnumBlockType.OccupiedArea)
        {
            tasks.Add((task.x + 2, task.y));
        }
        if (newField[task.x, task.y - 1] == EnumBlockType.NoLine && newField[task.x, task.y - 2] == EnumBlockType.OccupiedArea)
        {
            tasks.Add((task.x, task.y - 2));
        }
        if (newField[task.x, task.y + 1] == EnumBlockType.NoLine && newField[task.x, task.y + 2] == EnumBlockType.OccupiedArea)
        {
            tasks.Add((task.x, task.y + 2));
        }
    }


}
