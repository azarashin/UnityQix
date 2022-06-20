using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TesPositionUpdater
{
    private bool ArgumentExceptionCheck(PositionUpdater p, int x0, int y0, int x1, int y1)
    {
        bool original = UnityEngine.TestTools.LogAssert.ignoreFailingMessages; 
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            p.IsMovable(x0, y0, x1, y1);

        }
        catch (ArgumentException)
        {
            // 正しく例外が発生した
            UnityEngine.TestTools.LogAssert.ignoreFailingMessages = false;
            return true;

        }
        // 出るはずの例外が出なかったので失敗
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = false;
        return false;
    }

    private bool ArgumentExceptionCheckDelta(PositionUpdater p, int dx, int dy)
    {
        bool original = UnityEngine.TestTools.LogAssert.ignoreFailingMessages; 
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
        try
        {
            p.SideForward(dx, dy);

        }
        catch (ArgumentException)
        {
            // 正しく例外が発生した
            UnityEngine.TestTools.LogAssert.ignoreFailingMessages = false;
            return true;

        }
        // 出るはずの例外が出なかったので失敗
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = false;
        return false;
    }

    // パラメータ異常チェック
    [Test]
    public void TesPositionUpdaterSimplePasses000()
    {
        IrregularCheck(new Field(@"
#######
#.o.o.#
#ooooo#
#.o.o.#
#ooooo#
#.o.o.#
#######
"));

        IrregularCheck(new Field(@"
#######
#$#.o.#
#o#ooo#
#$#.o.#
#o#ooo#
#$#.o.#
#######
"));
    }

    private void IrregularCheck(Field field)
    { 
        PositionUpdater p = new PositionUpdater(field);
        // 壁外から内側へ
        // 左上
        Assert.IsTrue(ArgumentExceptionCheck(p, -2, 2, 0, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, -2, 2, 0));
        Assert.IsTrue(ArgumentExceptionCheck(p, -1, 2, 0, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, -1, 2, 0));

        // 右上
        Assert.IsTrue(ArgumentExceptionCheck(p, 8, 2, 6, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, -2, 4, 0));
        Assert.IsTrue(ArgumentExceptionCheck(p, 7, 2, 6, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, -1, 4, 0));

        // 左下
        Assert.IsTrue(ArgumentExceptionCheck(p, -2, 4, 0, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 8, 2, 6));
        Assert.IsTrue(ArgumentExceptionCheck(p, -1, 4, 0, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 7, 2, 6));

        // 右下
        Assert.IsTrue(ArgumentExceptionCheck(p, 8, 4, 6, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 8, 4, 6));
        Assert.IsTrue(ArgumentExceptionCheck(p, 7, 4, 6, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 7, 4, 6));

        // 内側から壁外へ
        // 左上
        Assert.IsTrue(ArgumentExceptionCheck(p, 0, 2, -2, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 0, 2, -2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 0, 2, -1, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 0, 2, -1));

        // 右上
        Assert.IsTrue(ArgumentExceptionCheck(p, 6, 2, 8, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 0, 4, -2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 6, 2, 7, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 0, 4, -1));

        // 左下
        Assert.IsTrue(ArgumentExceptionCheck(p, 0, 4, -2, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 6, 2, 8));
        Assert.IsTrue(ArgumentExceptionCheck(p, 0, 4, -1, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 6, 2, 7));

        // 右下
        Assert.IsTrue(ArgumentExceptionCheck(p, 6, 4, 8, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 6, 4, 8));
        Assert.IsTrue(ArgumentExceptionCheck(p, 6, 4, 7, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 6, 4, 7));

        // 不正な歩幅
        // 内側で動き回る
        // 左→右
        Assert.IsTrue(ArgumentExceptionCheck(p, 0, 2, 1, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 0, 2, 3, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 0, 2, 4, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 2, 3, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 2, 5, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 2, 6, 2));
        // 上→下
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 0, 4, 1));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 0, 4, 3));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 0, 4, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 2, 4, 3));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 2, 4, 5));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 2, 4, 6));
        // 右→左
        Assert.IsTrue(ArgumentExceptionCheck(p, 6, 4, 5, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 6, 4, 3, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 6, 4, 2, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 4, 3, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 4, 1, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 4, 4, 0, 4));
        // 下→上
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 6, 2, 5));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 6, 2, 3));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 6, 2, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 4, 2, 3));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 4, 2, 1));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 4, 2, 0));

        // 不正な移動開始位置
        // 左→右
        Assert.IsTrue(ArgumentExceptionCheck(p, 1, 2, 2, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 1, 2, 3, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 1, 2, 4, 2));

        // 上→下
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 1, 2, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 1, 2, 3));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 1, 2, 4));

        // 右→左
        Assert.IsTrue(ArgumentExceptionCheck(p, 5, 2, 4, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 5, 2, 3, 2));
        Assert.IsTrue(ArgumentExceptionCheck(p, 5, 2, 2, 2));

        // 下→上
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 5, 2, 4));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 5, 2, 3));
        Assert.IsTrue(ArgumentExceptionCheck(p, 2, 5, 2, 2));
    }

    // 空のフィールド
    [Test]
    public void TesPositionUpdaterSimplePasses001()
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
        PositionUpdater p = new PositionUpdater(field);
        // 壁端から内側へ
        // 左上
        Assert.IsTrue(p.IsMovable(0, 2, 2, 2));
        Assert.IsTrue(p.IsMovable(2, 0, 2, 2));

        // 右上
        Assert.IsTrue(p.IsMovable(6, 2, 4, 2));
        Assert.IsTrue(p.IsMovable(4, 0, 4, 2));

        // 左下
        Assert.IsTrue(p.IsMovable(0, 4, 2, 4));
        Assert.IsTrue(p.IsMovable(2, 6, 2, 4));

        // 右下
        Assert.IsTrue(p.IsMovable(6, 4, 4, 4));
        Assert.IsTrue(p.IsMovable(4, 6, 4, 4));

        // 内側で動き回る
        // 左→右
        Assert.IsTrue(p.IsMovable(2, 2, 4, 2));
        // 上→下
        Assert.IsTrue(p.IsMovable(4, 2, 4, 4));
        // 左→右
        Assert.IsTrue(p.IsMovable(4, 4, 2, 4));
        // 下→上
        Assert.IsTrue(p.IsMovable(2, 4, 2, 2));
    }

    // 右から左へ占有領域へ侵入する
    [Test]
    public void TesPositionUpdaterSimplePasses002()
    {
        Field field = new Field(@"
#######
#$#.o.#
#o#ooo#
#$#.o.#
#o#ooo#
#$#.o.#
#######
");
        PositionUpdater p = new PositionUpdater(field);
        Assert.IsTrue(p.IsMovable(4, 2, 2, 2));
        Assert.IsFalse(p.IsMovable(2, 2, 0, 2));
    }

    // 左から右へ占有領域へ侵入する
    [Test]
    public void TesPositionUpdaterSimplePasses003()
    {
        Field field = new Field(@"
#######
#.o.#$#
#ooo#o#
#.o.#$#
#ooo#o#
#.o.#$#
#######
");
        PositionUpdater p = new PositionUpdater(field);
        Assert.IsTrue(p.IsMovable(2, 2, 4, 2));
        Assert.IsFalse(p.IsMovable(4, 2, 6, 2));
    }

    // 上から下へ占有領域へ侵入する
    [Test]
    public void TesPositionUpdaterSimplePasses004()
    {
        Field field = new Field(@"
#######
#.o.o.#
#ooooo#
#.o.o.#
#######
#$o$o$#
#######
");
        PositionUpdater p = new PositionUpdater(field);
        Assert.IsTrue(p.IsMovable(2, 2, 4, 2));
        Assert.IsFalse(p.IsMovable(2, 4, 2, 6));
    }

    // 上から下へ占有領域へ侵入する
    [Test]
    public void TesPositionUpdaterSimplePasses005()
    {
        Field field = new Field(@"
#######
#$o$o$#
#######
#.o.o.#
#ooooo#
#.o.o.#
#######
");
        PositionUpdater p = new PositionUpdater(field);
        Assert.IsTrue(p.IsMovable(2, 4, 2, 2));
        Assert.IsFalse(p.IsMovable(2, 2, 2, 0));
    }

    // 占有領域間をジャンプする
    [Test]
    public void TesPositionUpdaterSimplePasses006()
    {
        Field field = new Field(@"
#######
#$#.#$#
###o###
#.o.o.#
###o###
#$#.#$#
#######
");
        PositionUpdater p = new PositionUpdater(field);
        Assert.IsTrue(p.IsMovable(2, 2, 4, 2));
        Assert.IsTrue(p.IsMovable(4, 2, 2, 2));
        Assert.IsTrue(p.IsMovable(2, 2, 2, 4));
        Assert.IsTrue(p.IsMovable(2, 4, 2, 2));
    }


    // ライン上(占有領域と非占有領域の境目)を移動する
    [Test]
    public void TesPositionUpdaterSimplePasses007()
    {
        Field field = new Field(@"
#######
#$#.#$#
###o###
#.o.o.#
###o###
#$#.#$#
#######
");
        PositionUpdater p = new PositionUpdater(field);
        Assert.IsTrue(p.IsMovable(0, 2, 2, 2));
        Assert.IsTrue(p.IsMovable(2, 2, 2, 0));
        Assert.IsTrue(p.IsMovable(2, 0, 4, 0));
        Assert.IsTrue(p.IsMovable(4, 0, 4, 2));
        Assert.IsTrue(p.IsMovable(4, 2, 6, 2));
        Assert.IsTrue(p.IsMovable(6, 2, 6, 4));
        Assert.IsTrue(p.IsMovable(6, 4, 4, 4));
        Assert.IsTrue(p.IsMovable(4, 4, 4, 6));
        Assert.IsTrue(p.IsMovable(4, 6, 2, 6));
        Assert.IsTrue(p.IsMovable(2, 6, 2, 4));
        Assert.IsTrue(p.IsMovable(2, 4, 0, 4));
        Assert.IsTrue(p.IsMovable(0, 4, 0, 2));
    }

    // ライン上(非占有領域上)を移動する
    [Test]
    public void TesPositionUpdaterSimplePasses008()
    {
        Field field = new Field(@"
#######
#.o.#.#
###o#o#
#.o.o.#
#o#o###
#.#.o.#
#######
");
        PositionUpdater p = new PositionUpdater(field);
        Assert.IsFalse(p.IsMovable(0, 2, 2, 2));
        Assert.IsFalse(p.IsMovable(2, 2, 0, 2));
        Assert.IsFalse(p.IsMovable(4, 0, 4, 2));
        Assert.IsFalse(p.IsMovable(4, 2, 4, 0));
        Assert.IsFalse(p.IsMovable(6, 4, 4, 4));
        Assert.IsFalse(p.IsMovable(4, 4, 6, 4));
        Assert.IsFalse(p.IsMovable(2, 6, 2, 4));
        Assert.IsFalse(p.IsMovable(2, 4, 2, 6));

        Assert.IsTrue(p.IsMovable(0, 2, 2, 2, true));
        Assert.IsTrue(p.IsMovable(2, 2, 0, 2, true));
        Assert.IsTrue(p.IsMovable(4, 0, 4, 2, true));
        Assert.IsTrue(p.IsMovable(4, 2, 4, 0, true));
        Assert.IsTrue(p.IsMovable(6, 4, 4, 4, true));
        Assert.IsTrue(p.IsMovable(4, 4, 6, 4, true));
        Assert.IsTrue(p.IsMovable(2, 6, 2, 4, true));
        Assert.IsTrue(p.IsMovable(2, 4, 2, 6, true));

    }

    // 
    [Test]
    public void TesPositionUpdaterSimplePasses009()
    {
        Field field = new Field(@"
###
#.#
###
");
        PositionUpdater p = new PositionUpdater(field);
        ArgumentExceptionCheckDelta(p, -2, 0);
        ArgumentExceptionCheckDelta(p, 2, 0);
        ArgumentExceptionCheckDelta(p, 0, 2);
        ArgumentExceptionCheckDelta(p, 0, -2);

        ArgumentExceptionCheckDelta(p, 0, 0);
        ArgumentExceptionCheckDelta(p, 1, 1);
        ArgumentExceptionCheckDelta(p, 1, -1);
        ArgumentExceptionCheckDelta(p, -1, 1);
        ArgumentExceptionCheckDelta(p, -1, -1);
    }

    // 
    [Test]
    public void TesPositionUpdaterSimplePasses010()
    {
        Field field = new Field(@"
###
#.#
###
");
        PositionUpdater p = new PositionUpdater(field);
        Assert.AreEqual((1, 1, -1, 1), p.SideForward(0, 1));
        Assert.AreEqual((-1, -1, 1, -1), p.SideForward(0, -1));
        Assert.AreEqual((1, 1, 1, -1), p.SideForward(1, 0));
        Assert.AreEqual((-1, -1, -1, 1), p.SideForward(-1, 0));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TesPositionUpdaterWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
