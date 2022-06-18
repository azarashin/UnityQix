using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestAreaCalculator
{
    /// <summary>
    /// 面積計算
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses000()
    {
        Field field = new Field(@"
#######
#.o.o.#
#ooooo#
#.o.o.#
#######
");
        AreaCalculator calc = new AreaCalculator();
        Assert.AreEqual(6, calc.NumberOfAllPoints(field)); 
        yield return null;
    }

    /// <summary>
    /// 占有面積計算
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses001()
    {
        Field field = new Field(@"
#######
#$o$o.#
#ooooo#
#$o$o.#
#######
");
        AreaCalculator calc = new AreaCalculator();
        Assert.AreEqual(6, calc.NumberOfAllPoints(field));
        Assert.AreEqual(4, calc.NumberOfOccupiedPoints(field));
        yield return null;
    }

    /// <summary>
    /// 占有判定
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses002()
    {
        string src = @"
#######
#.o.o.#
#ooooo#
#.o.o.#
#######
";
        string expected = @"
#######
#.o.o.#
#ooooo#
#.o.o.#
#######
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (1, 1)});
        Debug.Log(field.DebugField());
        Assert.AreEqual(6, calc.NumberOfAllPoints(field));
        Assert.AreEqual(0, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 占有判定
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses003()
    {
        string src = @"
#######
#.#.o.#
###ooo#
#.o.o.#
#######
";
        string expected = @"
#######
#.#$o$#
###ooo#
#$o$o$#
#######
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (1, 1) });
        Assert.AreEqual(6, calc.NumberOfAllPoints(field));
        Assert.AreEqual(5, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 占有判定
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses004()
    {
        string src = @"
#######
#.#.o.#
###ooo#
#.o.o.#
#######
";
        string expected = @"
#######
#$#.o.#
###ooo#
#.o.o.#
#######
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (5, 3) });
        Assert.AreEqual(6, calc.NumberOfAllPoints(field));
        Assert.AreEqual(1, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses005()
    {
        string src = @"
###########
#.o.o.o.o.#
###o###ooo#
#.#.#.#.o.#
#o###o#ooo#
#.o.o.#.o.#
#ooo###o###
#.o.#.o.#.#
#ooo#####o#
#.o.o.o.o.#
###########
";
        string expected = @"
###########
#.o.o.o.o.#
###o###ooo#
#$#.#$#.o.#
#o###o#ooo#
#$o$o$#.o.#
#ooo###o###
#$o$#.o.#$#
#ooo#####o#
#$o$o$o$o$#
###########
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (1, 1) });
        Assert.AreEqual(25, calc.NumberOfAllPoints(field));
        Assert.AreEqual(13, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }
    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses006()
    {
        string src = @"
###########
#.o.o.o.o.#
###o###ooo#
#.#.#.#.o.#
#o###o#ooo#
#.o.o.#.o.#
#ooo###o###
#.o.#.o.#.#
#ooo#####o#
#.o.o.o.o.#
###########
";
        string expected = @"
###########
#.o.o.o.o.#
###o###ooo#
#$#.#$#.o.#
#o###o#ooo#
#$o$o$#.o.#
#ooo###o###
#$o$#.o.#$#
#ooo#####o#
#$o$o$o$o$#
###########
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (5, 7) });
        Assert.AreEqual(25, calc.NumberOfAllPoints(field));
        Assert.AreEqual(13, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses007()
    {
        string src = @"
###########
#.o.o.o.o.#
###o###ooo#
#.#.#.#.o.#
#o###o#ooo#
#.o.o.#.o.#
#ooo###o###
#.o.#.o.#.#
#ooo#####o#
#.o.o.o.o.#
###########
";
        string expected = @"
###########
#$o$o$o$o$#
###o###ooo#
#.#$#.#$o$#
#o###o#ooo#
#.o.o.#$o$#
#ooo###o###
#.o.#$o$#.#
#ooo#####o#
#.o.o.o.o.#
###########
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (1, 3) });
        Assert.AreEqual(25, calc.NumberOfAllPoints(field));
        Assert.AreEqual(12, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses008()
    {
        string src = @"
###########
#.o.o.o.o.#
###o###ooo#
#.#.#.#.o.#
#o###o#ooo#
#.o.o.#.o.#
#ooo###o###
#.o.#.o.#.#
#ooo#####o#
#.o.o.o.o.#
###########
";
        string expected = @"
###########
#$o$o$o$o$#
###o###ooo#
#.#$#.#$o$#
#o###o#ooo#
#.o.o.#$o$#
#ooo###o###
#.o.#$o$#.#
#ooo#####o#
#.o.o.o.o.#
###########
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (9, 7) });
        Assert.AreEqual(25, calc.NumberOfAllPoints(field));
        Assert.AreEqual(12, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses009()
    {
        string src = @"
###################
#.o.o.o.o.o.o.o.o.#
#################o#
#.o.o.o.o.o.o.o.#.#
###############o#o#
#.o.o.o.o.o.o.#.#.#
#o###########o#o#o#
#.#.o.o.o.o.#.#.#.#
#o#o###ooooo#o#o#o#
#.#.#.#.o.o.#.#.#.#
#o#o#o#######o#o#o#
#.#.#.o.o.o.o.#.#.#
#o#o###########o#o#
#.#.o.o.o.o.o.o.#.#
#o###############o#
#.o.o.o.o.o.o.o.o.#
###################
";
        string expected = @"
###################
#.o.o.o.o.o.o.o.o.#
#################o#
#$o$o$o$o$o$o$o$#.#
###############o#o#
#.o.o.o.o.o.o.#$#.#
#o###########o#o#o#
#.#$o$o$o$o$#.#$#.#
#o#o###ooooo#o#o#o#
#.#$#.#$o$o$#.#$#.#
#o#o#o#######o#o#o#
#.#$#.o.o.o.o.#$#.#
#o#o###########o#o#
#.#$o$o$o$o$o$o$#.#
#o###############o#
#.o.o.o.o.o.o.o.o.#
###################
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (1, 1) });
        Assert.AreEqual(72, calc.NumberOfAllPoints(field));
        Assert.AreEqual(29, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses010()
    {
        string src = @"
###################
#.o.o.o.o.o.o.o.o.#
#################o#
#.o.o.o.o.o.o.o.#.#
###############o#o#
#.o.o.o.o.o.o.#.#.#
#o###########o#o#o#
#.#.o.o.o.o.#.#.#.#
#o#o###ooooo#o#o#o#
#.#.#.#.o.o.#.#.#.#
#o#o#o#######o#o#o#
#.#.#.o.o.o.o.#.#.#
#o#o###########o#o#
#.#.o.o.o.o.o.o.#.#
#o###############o#
#.o.o.o.o.o.o.o.o.#
###################
";
        string expected = @"
###################
#.o.o.o.o.o.o.o.o.#
#################o#
#$o$o$o$o$o$o$o$#.#
###############o#o#
#.o.o.o.o.o.o.#$#.#
#o###########o#o#o#
#.#$o$o$o$o$#.#$#.#
#o#o###ooooo#o#o#o#
#.#$#.#$o$o$#.#$#.#
#o#o#o#######o#o#o#
#.#$#.o.o.o.o.#$#.#
#o#o###########o#o#
#.#$o$o$o$o$o$o$#.#
#o###############o#
#.o.o.o.o.o.o.o.o.#
###################
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (5, 9) });
        Assert.AreEqual(72, calc.NumberOfAllPoints(field));
        Assert.AreEqual(29, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses011()
    {
        string src = @"
###################
#.o.o.o.o.o.o.o.o.#
#################o#
#.o.o.o.o.o.o.o.#.#
###############o#o#
#.o.o.o.o.o.o.#.#.#
#o###########o#o#o#
#.#.o.o.o.o.#.#.#.#
#o#o###ooooo#o#o#o#
#.#.#.#.o.o.#.#.#.#
#o#o#o#######o#o#o#
#.#.#.o.o.o.o.#.#.#
#o#o###########o#o#
#.#.o.o.o.o.o.o.#.#
#o###############o#
#.o.o.o.o.o.o.o.o.#
###################
";
        string expected = @"
###################
#$o$o$o$o$o$o$o$o$#
#################o#
#.o.o.o.o.o.o.o.#$#
###############o#o#
#$o$o$o$o$o$o$#.#$#
#o###########o#o#o#
#$#.o.o.o.o.#$#.#$#
#o#o###ooooo#o#o#o#
#$#.#$#.o.o.#$#.#$#
#o#o#o#######o#o#o#
#$#.#$o$o$o$o$#.#$#
#o#o###########o#o#
#$#.o.o.o.o.o.o.#$#
#o###############o#
#$o$o$o$o$o$o$o$o$#
###################
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (1, 3) });
        Assert.AreEqual(72, calc.NumberOfAllPoints(field));
        Assert.AreEqual(43, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses012()
    {
        string src = @"
###################
#.o.o.o.o.o.o.o.o.#
#################o#
#.o.o.o.o.o.o.o.#.#
###############o#o#
#.o.o.o.o.o.o.#.#.#
#o###########o#o#o#
#.#.o.o.o.o.#.#.#.#
#o#o###ooooo#o#o#o#
#.#.#.#.o.o.#.#.#.#
#o#o#o#######o#o#o#
#.#.#.o.o.o.o.#.#.#
#o#o###########o#o#
#.#.o.o.o.o.o.o.#.#
#o###############o#
#.o.o.o.o.o.o.o.o.#
###################
";
        string expected = @"
###################
#$o$o$o$o$o$o$o$o$#
#################o#
#.o.o.o.o.o.o.o.#$#
###############o#o#
#$o$o$o$o$o$o$#.#$#
#o###########o#o#o#
#$#.o.o.o.o.#$#.#$#
#o#o###ooooo#o#o#o#
#$#.#$#.o.o.#$#.#$#
#o#o#o#######o#o#o#
#$#.#$o$o$o$o$#.#$#
#o#o###########o#o#
#$#.o.o.o.o.o.o.#$#
#o###############o#
#$o$o$o$o$o$o$o$o$#
###################
";
        Field field = new Field(src);
        Field exField = new Field(expected);
        AreaCalculator calc = new AreaCalculator();
        field = calc.UpdateField(field, new (int X, int Y)[] { (9, 9) });
        Assert.AreEqual(72, calc.NumberOfAllPoints(field));
        Assert.AreEqual(43, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
        yield return null;
    }

    /// <summary>
    /// 縁判定
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses013()
    {
        string src = @"
###################
#$o$o$o$o$o$o$o$#$#
###################
#.o.o.o.o.o.o.o.#$#
###############o#o#
#$#$o$o$o$o$#$#.#$#
#############o#o#o#
#$#.o.o.#.o.#$#.#$#
#o#o###o#ooo#o#o#o#
#$#.#$#.o.o.#$#.#$#
#o#o###########o#o#
#$#.#$o$#$o$o$#.#$#
###############o#o#
#.#.o.#.o.o.o.o.#$#
###################
#.#.o.#$#$o$o$o$#$#
###################
";

        string expected = @"
###################
#...............#.#
*****************##
*...............*.#
***************.*.#
#.#.........#.*.*.#
##***********.*.*.#
#.*.....+...*.*.*.#
#.*.***.+...*.*.*.#
#.*.*.*.....*.*.*.#
#.*.*#*******#*.*.#
#.*.*...#.....*.*.#
***+***********.*.#
*.+...+.........*.#
*+++++***********##
*.+...*.#.......#.#
*******############
";
        expected = expected.Trim(); 
        Field field = new Field(src);
        AreaCalculator calc = new AreaCalculator();
        string edge = new Edge(field).DebugEdge().Trim();
        Debug.Log(edge);
        Debug.Log(expected);
        Assert.AreEqual(expected, edge);
        yield return null;
    }
}
