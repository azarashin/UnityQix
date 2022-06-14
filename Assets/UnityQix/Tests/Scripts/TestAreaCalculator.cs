using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestAreaCalculator
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
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestAreaCalculatorWithEnumeratorPasses001()
    {
        UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true; 
        // 不正なテストデータ
        try
        {
            Field field = new Field(@"
...
...
...
");

        } catch(ArgumentException)
        {
            yield break;

        }
        Assert.Fail(); // 例外を出すはずなのに出していない場合はテスト失敗
        yield return null;
    }
}
