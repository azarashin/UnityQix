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
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses000()
    {
        Field field = new Field(@"
#######
#.o.o.#
#ooooo#
#.o.o.#
#######
");
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(5, 7, 1) });
        Assert.AreEqual(6, calc.NumberOfAllPoints(field)); 
    }

    /// <summary>
    /// 占有面積計算
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses001()
    {
        Field field = new Field(@"
#######
#$o$@.#
#ooo@o#
#$o$@.#
#######
");
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(5, 7, 1) });
        Assert.AreEqual(6, calc.NumberOfAllPoints(field));
        Assert.AreEqual(4, calc.NumberOfOccupiedPoints(field));
    }

    /// <summary>
    /// 占有判定
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses002()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(1, 1, 1) });
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(6, calc.NumberOfAllPoints(field));
        Assert.AreEqual(0, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }

    /// <summary>
    /// 占有判定
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses003()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(1, 1, 1) });
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(6, calc.NumberOfAllPoints(field));
        Assert.AreEqual(5, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }

    /// <summary>
    /// 占有判定
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses004()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(5, 3, 1)});
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(6, calc.NumberOfAllPoints(field));
        Assert.AreEqual(1, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses005()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(1, 1, 1)});
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(25, calc.NumberOfAllPoints(field));
        Assert.AreEqual(13, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }
    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses006()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(5, 7, 1)});
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(25, calc.NumberOfAllPoints(field));
        Assert.AreEqual(13, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses007()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(1, 3, 1)});
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(25, calc.NumberOfAllPoints(field));
        Assert.AreEqual(12, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses008()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(9, 7, 1)});
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(25, calc.NumberOfAllPoints(field));
        Assert.AreEqual(12, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses009()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(1, 1, 1)});
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(72, calc.NumberOfAllPoints(field));
        Assert.AreEqual(29, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses010()
    {
        string src = @"
###################
#.o.o.o.o.o.o.o.o.#
#################o#
#.o.o.o.o.o.o.o.#.#
###############o#o#
#.o.o.o.o.o.o.#.#.#
#o###########o#o#o#
#.#.o.o.@.o.#.#.#.#
#o#o###o@o@o#o#o#o#
#.#.#.#.o.@.#.#.#.#
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
#.#$o$o$@$o$#.#$#.#
#o#o###o@o@o#o#o#o#
#.#$#.#$o$@$#.#$#.#
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(5, 9, 1)});
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(72, calc.NumberOfAllPoints(field));
        Assert.AreEqual(29, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses011()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(1, 3, 1)});
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(72, calc.NumberOfAllPoints(field));
        Assert.AreEqual(43, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }

    /// <summary>
    /// 占有判定(中マップ)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses012()
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
        AreaCalculator calc = new AreaCalculator(new IEnemy[] { new TestEnemy(9, 9, 1)});
        calc.UpdateField(field, 0, 1);
        Assert.AreEqual(72, calc.NumberOfAllPoints(field));
        Assert.AreEqual(43, calc.NumberOfOccupiedPoints(field));
        Assert.AreEqual(exField.DebugField(), field.DebugField());
    }


}
