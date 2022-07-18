using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestPlayer
{
    // 敵との距離が十分にあるケース
    [UnityTest]
    public IEnumerator TestPlayerWithEnumeratorPasses000()
    {
        Factory factory = TestCommon.Factory();
        Player player = factory.GetPlayer(0);
        Field field = new Field(9, 9);

        int lives = 2;
        int units = 2; 

        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, lives, units,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(5, 5, 1) }
            );

        Assert.IsFalse(player.IsInvisible());
        Assert.IsTrue(player.Lives == lives);
        Assert.IsTrue(player.Units == units);
        yield return null;

        Assert.IsFalse(player.IsInvisible());
        Assert.IsTrue(player.Lives == lives);
        Assert.IsTrue(player.Units == units);
        yield return null;
    }

    // 敵に接触したケース
    [UnityTest]
    public IEnumerator TestPlayerWithEnumeratorPasses001()
    {
        Factory factory = TestCommon.Factory();
        Player player = factory.GetPlayer(0);
        Field field = new Field(9, 9);

        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, 2, 2,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(4, 8, 1) }
            );

        Assert.IsFalse(player.IsInvisible());
        Assert.AreEqual(2, player.Lives);
        Assert.AreEqual(2, player.Units);
        yield return null;

        player.Damage(); 

        Assert.IsTrue(player.IsInvisible());
        Assert.AreEqual(1, player.Lives);
        Assert.AreEqual(2, player.Units);
        yield return null;
    }

    // 敵に接触し続け、残機が減るケース
    [UnityTest]
    public IEnumerator TestPlayerWithEnumeratorPasses002()
    {
        Factory factory = TestCommon.Factory();
        Player player = factory.GetPlayer(0);
        Field field = new Field(9, 9);

        GameController controller = factory.GetGameController();
        controller.Setup(
            field,
            new (int, int)[] { (4, 8) },
            0.8f, 1, 2,
            new Player[] { player },
            new IEnemy[] { new TestEnemy(4, 8, 1) }
            );

        Assert.IsFalse(player.IsInvisible());
        Assert.AreEqual(1, player.Lives);
        Assert.AreEqual(2, player.Units);
        yield return null;

        player.Damage();

        Assert.IsTrue(player.IsInvisible());
        Assert.AreEqual(1, player.Lives);
        Assert.AreEqual(1, player.Units);
        yield return null;
    }

    // 敵に接触し続け、残機がなくなるケース
    [UnityTest]
    public IEnumerator TestPlayerWithEnumeratorPasses003()
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
        yield return null;

        player.Damage();

        Assert.IsTrue(player.IsInvisible());
        Assert.AreEqual(0, player.Lives);
        Assert.AreEqual(0, player.Units);
        yield return null;
    }

}
