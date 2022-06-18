﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestLine
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestLineSimplePasses000()
    {
        string src = @"
#######****++++######
#...................#
#...................#
#...#...*...+...#...#
#..###..**.++...#...#
#...#...*...+...#...#
#...................#
*...................*
*..###..**.++...#...*
*...#...*...+...#...*
*...................*
+...#...*...+...#...+
+..###..**.++...#...+
+...................+
+...................+
#...................#
#..###..**.++...#...#
#...................#
#...................#
#...................#
#######****++++######
";

        string expected = @"
A33333333333333333339
C...................C
C...................C
C...8...8...8...8...C
C..2F1..E1.2D...C...C
C...4...4...4...4...C
C...................C
C...................C
C..2B1..A1.29...8...C
C...4...4...4...4...C
C...................C
C...8...8...8...8...C
C..271..61.25...4...C
C...................C
C...................C
C...................C
C..231..21.21...0...C
C...................C
C...................C
C...................C
633333333333333333335
";
        expected = expected.Trim();
        Edge edge = new Edge(src);
        AreaCalculator calc = new AreaCalculator();
        string line = new Line(edge).DebugLine().Trim();
        Debug.Log(line + "\n\n" + expected);
        Assert.AreEqual(expected, line);
    }

}