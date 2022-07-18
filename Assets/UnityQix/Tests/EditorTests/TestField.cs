using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestField
{
    /// <summary>
    /// 最小フィールド
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses000()
    {
        Field field = new Field(@"
###
#.#
###
");
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(0, 0));
        Assert.AreEqual(EnumBlockType.FreeArea, field.AreaType(1, 1));
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(2, 2));
    }

    // 不正なテストデータ(外がラインではない)
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses001()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            Field field = new Field(@"
...
...
...
");

        }
        catch (ArgumentException)
        {
            return;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
    }

    // 不正なテストデータ(置いてはいけないライン)
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses002()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            Field field = new Field(@"
###
###
###
");

        }
        catch (ArgumentException)
        {
            return;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
    }

    /// <summary>
    /// テスト用フィールド
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses003()
    {
        Field field = new Field(@"
#######
#.o.o.#
#ooooo#
#.o.o.#
#ooooo#
#.o.o.#
#######
");
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(0, 0));
        Assert.AreEqual(EnumBlockType.FreeArea, field.AreaType(1, 1));
        Assert.AreEqual(EnumBlockType.NoLine, field.AreaType(2, 2));
    }

    // 不正なテストデータ(置いてはいけないライン)
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses004()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            Field field = new Field(@"
#######
#.o.o.#
#ooooo#
#.o#o.#
#ooooo#
#.o.o.#
#######
");

        }
        catch (ArgumentException)
        {
            return;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
    }

    /// <summary>
    /// テスト用フィールド(正しいラインだらけ)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses005()
    {
        Field field = new Field(@"
#######
#.#.#.#
#######
#.#.#.#
#######
#.#.#.#
#######
");
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(0, 0));
        Assert.AreEqual(EnumBlockType.FreeArea, field.AreaType(1, 1));
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(2, 2));
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(1, 2));
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(2, 1));
        Assert.AreEqual(EnumBlockType.FreeArea, field.AreaType(3, 3));
    }

    /// <summary>
    /// 最小フィールド(中央を占有)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses006()
    {
        Field field = new Field(@"
###
#$#
###
");
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(0, 0));
        Assert.AreEqual(EnumBlockType.OccupiedArea, field.AreaType(1, 1));
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(2, 2));
    }

    // 不正なテストデータ(外が占有)
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses007()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            Field field = new Field(@"
$$$
$.$
$$$
");

        }
        catch (ArgumentException)
        {
            return;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
    }


    // 不正なテストデータ(置いてはいけない占有)
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses008()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            Field field = new Field(@"
#######
#.$.o.#
#ooooo#
#.o.o.#
#ooooo#
#.o.o.#
#######
");

        }
        catch (ArgumentException)
        {
            return;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
    }

    // 不正なテストデータ(置いてはいけない占有)
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses009()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            Field field = new Field(@"
#######
#.o.o.#
#$oooo#
#.o.o.#
#ooooo#
#.o.o.#
#######
");

        }
        catch (ArgumentException)
        {
            return;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
    }
    /// <summary>
    /// テスト用フィールド(全部占有)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses010()
    {
        Field field = new Field(@"
#######
#$#$o$#
#o#####
#$#$#$#
###o###
#$o$o$#
#######
");
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(0, 0));
        Assert.AreEqual(EnumBlockType.OccupiedArea, field.AreaType(1, 1));
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(2, 2));
        Assert.AreEqual(EnumBlockType.NoLine, field.AreaType(1, 2));
        Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(2, 1));
        Assert.AreEqual(EnumBlockType.OccupiedArea, field.AreaType(3, 3));
    }

    /// <summary>
    /// テスト用フィールド(幅と高さ確認)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses011()
    {
        Field field = new Field(@"
#######
#$#$o$#
#o##o##
#$#$o$#
#o#####
#$#$#$#
###o###
#$o$o$#
#######
");
        Assert.AreEqual(7, field.Width());
        Assert.AreEqual(9, field.Height());
    }

    /// <summary>
    /// 空のフィールド
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses012()
    {
        int width = 5;
        int height = 7;
        Field field = new Field(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    Assert.AreEqual(EnumBlockType.OnLine, field.AreaType(x, y));
                }
                else if (x % 2 == 0 || y % 2 == 0)
                {
                    Assert.AreEqual(EnumBlockType.NoLine, field.AreaType(x, y));
                }
                else
                {
                    Assert.AreEqual(EnumBlockType.FreeArea, field.AreaType(x, y));
                }
            }
        }
        Field fieldExpected = new Field(@"
#####
#.o.#
#ooo#
#.o.#
#ooo#
#.o.#
#####
");
        Assert.AreEqual(fieldExpected.DebugField(), field.DebugField());

    }

    /// <summary>
    /// 空のフィールド(プレイヤーの占有状況確認)
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestAreaCalculatorWithEnumeratorPasses013()
    {
        int width = 5;
        int height = 7;
        Field field = new Field(width, height);
        string expectedOwnerMap = Field.DebugOwnedMap(@"
-----
-----
-----
-----
-----
-----
-----
");
        Assert.AreEqual(expectedOwnerMap, field.DebugOwnedMap());

    }

}
