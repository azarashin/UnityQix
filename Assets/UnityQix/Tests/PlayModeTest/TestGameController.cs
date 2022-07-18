using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestGameController
{
    /// <summary>
    /// 占有していく経緯の確認
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestGameControllerWithEnumeratorPasses000()
    {
        Factory factory = TestCommon.Factory();
        Player player = factory.GetPlayer(0);
        Field field = new Field(9, 9);
        InputManagerStub input = (InputManagerStub)player.GetInput();
        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, 1, 1,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(5, 5, 1) }
            );

        Field expected0 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#########
");

        string expectedOwner0 = Field.DebugOwnedMap(@"
---------
---------
---------
---------
---------
---------
---------
---------
---------
");
        Assert.AreEqual(expected0.DebugField(), field.DebugField());
        Assert.AreEqual((4, 8), player.Position());
        Assert.AreEqual(expectedOwner0, field.DebugOwnedMap());


        input.SetState(false, false, true, false);
        yield return null;
        Field expected1 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooo@ooo#
#.o.@.o.#
#########
");
        Assert.AreEqual(expected1.DebugField(), field.DebugField());
        Assert.AreEqual((4, 6), player.Position());
        Assert.AreEqual(expectedOwner0, field.DebugOwnedMap());

        input.SetState(true, false, false, false);
        yield return null;
        Field expected2 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#o@@@ooo#
#.o.@.o.#
#########
");
        Assert.AreEqual(expected2.DebugField(), field.DebugField());
        Assert.AreEqual((2, 6), player.Position());
        Assert.AreEqual(expectedOwner0, field.DebugOwnedMap());

        input.SetState(true, false, false, false);
        yield return null;
        Field expected3 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#####ooo#
#$o$#.o.#
#########
");
        Assert.AreEqual(expected3.DebugField(), field.DebugField());
        Assert.AreEqual((0, 6), player.Position());

        // このタイミングでプレイヤー番号0 が領地を広げる
        string expectedOwner1 = Field.DebugOwnedMap(@"
---------
---------
---------
---------
---------
---------
---------
-0-0-----
---------
");

        Assert.AreEqual(expectedOwner1, field.DebugOwnedMap());

    }


    /// <summary>
    /// ノルマ達成確認（未達成）
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestGameControllerWithEnumeratorPasses001()
    {
        Factory factory = TestCommon.Factory();
        Player player = factory.GetPlayer(0);
        Field field = new Field(@"
#########
#$o$o$o$#
#ooooooo#
#$o$o$o$#
#ooooooo#
#$o$o$o$#
#ooooooo#
#.o.o.o.#
#########
");
        InputManagerStub input = (InputManagerStub)player.GetInput();
        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, 1, 1,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(5, 5, 1) }
            );


        Assert.IsFalse(controller.IsClearQuota());
        yield return null;

    }

    /// <summary>
    /// ノルマ達成確認（達成）
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestGameControllerWithEnumeratorPasses002()
    {
        Factory factory = TestCommon.Factory();
        Player player = factory.GetPlayer(0);
        Field field = new Field(@"
#########
#$o$o$o$#
#ooooooo#
#$o$o$o$#
#ooooooo#
#$o$o$o$#
#ooooooo#
#$o.o.o.#
#########
");
        InputManagerStub input = (InputManagerStub)player.GetInput();
        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, 1, 1,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(5, 5, 1) }
            );


        Assert.IsTrue(controller.IsClearQuota());
        yield return null;

    }

    /// <summary>
    /// 確定済みのライン上の移動
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestGameControllerWithEnumeratorPasses003()
    {
        Factory factory = TestCommon.Factory();
        PlayerForTest player = (PlayerForTest)factory.GetPlayer(0);
        Field field = new Field(9, 9);
        InputManagerStub input = (InputManagerStub)player.GetInput();
        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, 1, 1,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(5, 5, 1) }
            );

        Field expected0 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#########
");
        Assert.AreEqual(expected0.DebugField(), field.DebugField());
        Assert.AreEqual((4, 8), player.Position());

        input.SetState(true, false, false, false);
        yield return null;
        Field expected1 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#########
");
        Assert.AreEqual(expected1.DebugField(), field.DebugField());
        Assert.AreEqual((2, 8), player.Position());
    }

    /// <summary>
    /// 線を引いて元に戻る
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestGameControllerWithEnumeratorPasses004()
    {
        Factory factory = TestCommon.Factory();
        PlayerForTest player = (PlayerForTest)factory.GetPlayer(0);
        Field field = new Field(9, 9);
        InputManagerStub input = (InputManagerStub)player.GetInput();
        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, 1, 1,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(5, 5, 1) }
            );

        Field expected0 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#########
");
        Assert.AreEqual(expected0.DebugField(), field.DebugField());
        Assert.AreEqual((4, 8), player.Position());

        input.SetState(false, false, true, false);
        yield return null;
        Field expected1 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooo@ooo#
#.o.@.o.#
#########
");
        Assert.AreEqual(expected1.DebugField(), field.DebugField());
        Assert.AreEqual((4, 6), player.Position());


        input.SetState(true, false, false, false);
        yield return null;
        Field expected2 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#o@@@ooo#
#.o.@.o.#
#########
");
        Assert.AreEqual(expected2.DebugField(), field.DebugField());
        Assert.AreEqual((2, 6), player.Position());


        input.SetState(false, true, false, false);
        yield return null;
        Field expected3 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooo@ooo#
#.o.@.o.#
#########
");
        Assert.AreEqual(expected3.DebugField(), field.DebugField());
        Assert.AreEqual((4, 6), player.Position());


        input.SetState(false, false, false, true);
        yield return null;
        Field expected4 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#########
");
        Assert.AreEqual(expected4.DebugField(), field.DebugField());
        Assert.AreEqual((4, 8), player.Position());
    }

    // プレイヤーの残機がなくなり、ゲームが終了したケース
    [UnityTest]
    public IEnumerator TestGameControllerWithEnumeratorPasses005()
    {
        Factory factory = TestCommon.Factory();
        Player player = factory.GetPlayer(0);
        Field field = new Field(9, 9);

        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, 1, 1,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(4, 8, 1) }
            );

        Assert.IsFalse(player.IsInvisible());
        Assert.AreEqual(1, player.Lives);
        Assert.AreEqual(1, player.Units);
        Assert.IsTrue(controller.IsDuringTheGame());
        yield return null;

        player.Damage();

        Assert.IsTrue(player.IsInvisible());
        Assert.AreEqual(0, player.Lives);
        Assert.AreEqual(0, player.Units);
        Assert.IsFalse(controller.IsDuringTheGame());
        yield return null;
    }


}
