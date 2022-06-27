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
    public IEnumerator TestPlayerFieldWithEnumeratorPasses000()
    {
        Factory factory = TestCommon.Factory();
        Player player = factory.GetPlayer(0);
        Field field = new Field(9, 9);
        InputManagerStub input = (InputManagerStub)player.GetInput();
        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, 
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

        input.SetState(false, false, true, false);
        yield return null;
        Field expected1 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooo#ooo#
#.o.#.o.#
#########
");
        Assert.AreEqual(expected1.DebugField(), field.DebugField());

        input.SetState(true, false, false, false);
        yield return null;
        Field expected2 = new Field(@"
#########
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#ooooooo#
#.o.o.o.#
#o###ooo#
#.o.#.o.#
#########
");
        Assert.AreEqual(expected2.DebugField(), field.DebugField());

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

    }


    /// <summary>
    /// ノルマ達成確認（未達成）
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestPlayerFieldWithEnumeratorPasses001()
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
            0.8f,
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
    public IEnumerator TestPlayerFieldWithEnumeratorPasses002()
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
            0.8f,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(5, 5, 1) }
            );


        Assert.IsTrue(controller.IsClearQuota());
        yield return null;

    }
}
