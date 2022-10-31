using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class RandomHelper
{
    public static Vector2 GetRandomDownwardUnitVector(float minX, float maxX) {
        float randomX = UnityEngine.Random.Range(minX * 10, maxX * 10) / 10;
        double yValue = Math.Sqrt(1.0 + Math.Pow((double)randomX, 2.0));
        return new Vector2(randomX, (float)yValue * -1);
    }
}