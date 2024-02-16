using System;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static float Approach(float val, float target, float maxMove)
        => val > target ? Mathf.Max(val - maxMove, target) : Mathf.Min(val + maxMove, target);

    public static T GetRandom<T>(this IList<T> items) => items[UnityEngine.Random.Range(0, items.Count)];

    public static float Map(float value, float oldMin, float oldMax, float newMin, float newMax)
        => Mathf.Lerp(newMin, newMax, Mathf.InverseLerp(oldMin, oldMax, value));

    public static float ClampedMap(float value, float oldMin, float oldMax, float newMin, float newMax)
        => Mathf.Lerp(newMin, newMax, Mathf.Clamp01(Mathf.InverseLerp(oldMin, oldMax, value)));
    public static float ClampedMap(float value, float oldMin, float oldMax, float newMin, float newMax, Func<float, float> lerpFunc)
        => Mathf.Lerp(newMin, newMax, Mathf.Clamp01(lerpFunc(Mathf.Clamp01(Mathf.InverseLerp(oldMin, oldMax, value)))));

    public static void SetX(this Transform t, float x) => t.position = new(x, t.position.y, t.position.z);
    public static void SetY(this Transform t, float y) => t.position = new(t.position.x, y, t.position.z);
    public static void SetZ(this Transform t, float z) => t.position = new(t.position.x, t.position.y, z);

    public static Vector3 WithX(this Vector3 v, float x) => new(x, v.y, v.z);
    public static Vector3 WithY(this Vector3 v, float y) => new(v.x, y, v.z);
    public static Vector3 WithZ(this Vector3 v, float z) => new(v.x, v.y, z);
}
