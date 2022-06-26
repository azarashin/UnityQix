using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestPlayerField
{
    [UnityTest]
    public IEnumerator TestPlayerFieldWithEnumeratorPasses()
    {
        Factory factory = TestCommon.Factory();
        Player player = factory.GetPlayer(0);
        Field field = new Field(9, 9);
        player.Setup(field, 4, 8);
        InputManagerStub input = (InputManagerStub)player.GetInput();

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
}
