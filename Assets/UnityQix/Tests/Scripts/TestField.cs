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
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses000()
    {
        Field field = new Field(@"
###
#.#
###
");
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(0, 0));
        Assert.AreEqual(EnumBlockType.Free, field.BlockType(1, 1));
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(2, 2));
        yield return null;
    }

    // 不正なテストデータ(外がラインではない)
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses001()
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
            yield break;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
        yield return null;
    }

    // 不正なテストデータ(置いてはいけないライン)
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses002()
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
            yield break;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
        yield return null;
    }

    /// <summary>
    /// テスト用フィールド
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses003()
    {
        Field field = new Field(@"
#######
#.....#
#.....#
#.....#
#.....#
#.....#
#######
");
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(0, 0));
        Assert.AreEqual(EnumBlockType.Free, field.BlockType(1, 1));
        Assert.AreEqual(EnumBlockType.Free, field.BlockType(2, 2));
        yield return null;
    }

    // 不正なテストデータ(置いてはいけないライン)
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses004()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            Field field = new Field(@"
#######
#.....#
#.....#
#..#..#
#.....#
#.....#
#######
");

        }
        catch (ArgumentException)
        {
            yield break;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
        yield return null;
    }

    /// <summary>
    /// テスト用フィールド(正しいラインだらけ)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses005()
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
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(0, 0));
        Assert.AreEqual(EnumBlockType.Free, field.BlockType(1, 1));
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(2, 2));
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(1, 2));
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(2, 1));
        Assert.AreEqual(EnumBlockType.Free, field.BlockType(3, 3));
        yield return null;
    }

    /// <summary>
    /// 最小フィールド(中央を占有)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses006()
    {
        Field field = new Field(@"
###
#$#
###
");
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(0, 0));
        Assert.AreEqual(EnumBlockType.Occupied, field.BlockType(1, 1));
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(2, 2));
        yield return null;
    }

    // 不正なテストデータ(外が占有)
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses007()
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
            yield break;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
        yield return null;
    }


    // 不正なテストデータ(置いてはいけない占有)
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses008()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            Field field = new Field(@"
#######
#.$...#
#.....#
#.....#
#.....#
#.....#
#######
");

        }
        catch (ArgumentException)
        {
            yield break;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
        yield return null;
    }

    // 不正なテストデータ(置いてはいけない占有)
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses009()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            Field field = new Field(@"
#######
#.....#
#$....#
#.....#
#.....#
#.....#
#######
");

        }
        catch (ArgumentException)
        {
            yield break;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
        yield return null;
    }
    /// <summary>
    /// テスト用フィールド(全部占有)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses010()
    {
        Field field = new Field(@"
#######
#$#$.$#
#.#####
#$#$#$#
###.###
#$.$.$#
#######
");
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(0, 0));
        Assert.AreEqual(EnumBlockType.Occupied, field.BlockType(1, 1));
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(2, 2));
        Assert.AreEqual(EnumBlockType.Free, field.BlockType(1, 2));
        Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(2, 1));
        Assert.AreEqual(EnumBlockType.Occupied, field.BlockType(3, 3));
        yield return null;
    }

    /// <summary>
    /// テスト用フィールド(幅と高さ確認)
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses011()
    {
        Field field = new Field(@"
#######
#$#$.$#
#.##.##
#$#$.$#
#.#####
#$#$#$#
###.###
#$.$.$#
#######
");
        Assert.AreEqual(7, field.Width());
        Assert.AreEqual(9, field.Height());
        yield return null;
    }

    /// <summary>
    /// 空のフィールド
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses012()
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
                    Assert.AreEqual(EnumBlockType.OnLine, field.BlockType(x, y));
                }
                else
                {
                    Assert.AreEqual(EnumBlockType.Free, field.BlockType(x, y));
                }
            }
        }
        yield return null;
    }
}
