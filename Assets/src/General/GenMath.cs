using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenMath {

    public static bool NearlyEqual(float n, float m, float epsilon) {
        return Mathf.Abs(n - m) <= epsilon;
    }

    /// <summary>
    /// Returns the module of the given int values
    /// </summary>
    /// <param name="n"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static int SignlessModule(int n, int m) {
        return n<0 ? m + (n % m) : n % m;
    }
    
    public enum Direction2D{
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    public static Vector3 EnumToVector(Direction2D d) {
        switch (d) {
            default:
            case Direction2D.UP:
                return Vector3.up;
            case Direction2D.DOWN:
                return Vector3.down;
            case Direction2D.RIGHT:
                return Vector3.right;
            case Direction2D.LEFT:
                return Vector3.left;
        }
    }
}
