using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestEdge
{
    /// <summary>
    /// 縁判定
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestEdgeSimplePasses001()
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

        string expected = Edge.DebugEdge(@"
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
");
        expected = expected.Trim();
        Field field = new Field(src);
        string edge = new Edge(field).DebugEdge().Trim();
        Assert.AreEqual(expected, edge);

    }
}
