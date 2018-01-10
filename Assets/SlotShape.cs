using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SlotShape {

    public int[,,] slotShape;
    public List<IntVector3> slotPoints
    {
        get { return GetPointsFromShape(slotShape); }
    }

    public SlotShape(int[,,] _slotShape) {
        slotShape = _slotShape;
    }

    private List<IntVector3> GetPointsFromShape(int[,,] slotShape) {
        List<IntVector3> slotPoints = new List<IntVector3>();
        
        for (int x = 0; x < slotShape.GetLength(2); x++)
        {
            for (int y = 0; y < slotShape.GetLength(1); y++)
            {
                for (int z = 0; z < slotShape.GetLength(0); z++)
                {
                    if (slotShape[z, y, x] > 0) slotPoints.Add(new IntVector3(x, y, z));
                }
            }
        }
        

        return slotPoints;
    }
}
