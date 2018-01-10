using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SlotShapeTest {

	[Test]
	public void SlotShapeTest_NoDimensions() {
		SlotShape ss = new SlotShape(new int[,,]
        {
            {}
        });

        Assert.Zero(ss.slotPoints.Count);
        /*
        ss = new SlotShape(new int[,,]
        {
            {{1,1}, {1,1}},
            {{1,1}, {1,1}}
        });

        

        Assert.AreEqual(8, ss.slotPoints.Count);
        */

    }

    [Test]
    public void SlotShapeTest_1Dimension () {

        SlotShape ss = new SlotShape(new int[,,]
        {
            {{0}}
        });

        Assert.Zero(ss.slotPoints.Count);

        ss = new SlotShape(new int[,,]
        {
            {{1}}
        });

        Assert.AreEqual(1, ss.slotPoints.Count);
        Assert.True(ss.slotPoints.Contains(new IntVector3(0, 0, 0)));

        ss = new SlotShape(new int[,,]
        {
            {{1, 1, 1}}
        });

        Assert.AreEqual(3, ss.slotPoints.Count);
        Assert.True(ss.slotPoints.Contains(new IntVector3(0, 0, 0)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(1, 0, 0)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(2, 0, 0)));

        ss = new SlotShape(new int[,,]
        {
            {{1, 0, 1}}
        });

        Assert.AreEqual(2, ss.slotPoints.Count);
        Assert.True(ss.slotPoints.Contains(new IntVector3(0, 0, 0)));
        Assert.False(ss.slotPoints.Contains(new IntVector3(1, 0, 0)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(2, 0, 0)));
    }

    [Test]
    public void SlotShapeTest_2Dimensions() {
        SlotShape ss = new SlotShape(new int[,,]
        {
            {
                {0,0},
                {0,0}
            }
        });

        Assert.Zero(ss.slotPoints.Count);

        ss = new SlotShape(new int[,,]
        {
            {
                {1,1},
                {1,1}
            }
        });

        Assert.AreEqual(4, ss.slotPoints.Count);
        Assert.True(ss.slotPoints.Contains(new IntVector3(0, 0, 0)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(1, 0, 0)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(0, 1, 0)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(1, 1, 0)));
    }

    [Test]
    public void SlotShapeTest_3Dimensions() {
        SlotShape ss = new SlotShape(new int[,,]
        {
            { // Layer 1
                {1,1},
                {1,1}
            },
            { // Layer 2
                {1,1},
                {1,1}
            }
        });

        Assert.AreEqual(8, ss.slotPoints.Count);

        // Layer 1
        Assert.True(ss.slotPoints.Contains(new IntVector3(0, 0, 0)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(1, 0, 0)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(0, 1, 0)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(1, 1, 0)));

        // Layer 2
        Assert.True(ss.slotPoints.Contains(new IntVector3(0, 0, 1)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(1, 0, 1)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(0, 1, 1)));
        Assert.True(ss.slotPoints.Contains(new IntVector3(1, 1, 1)));
    }
}