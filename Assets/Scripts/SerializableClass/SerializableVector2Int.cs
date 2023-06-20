using System;
using UnityEngine;

/// <summary>
/// Since unity doesn't flag the Vector2Int as serializable, we
/// need to create our own version. This one will automatically convert
/// between Vector2Int and SerializableVector2Int
/// </summary>
///  [System.Serializable]
[System.Serializable]
public struct SerializableVector2Int
{
    /// <summary>
    /// x component
    /// </summary>
    public int x;

    /// <summary>
    /// y component
    /// </summary>
    public int y;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="rX"></param>
    /// <param name="rY"></param>
    public SerializableVector2Int(int rX, int rY)
    {
        x = rX;
        y = rY;
    }

    /// <summary>
    /// Returns a string representation of the object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return String.Format("[{0}, {1}]", x, y);
    }

    /// <summary>
    /// Automatic conversion from SerializableVector3 to Vector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator Vector2Int(SerializableVector2Int rValue)
    {
        return new Vector2Int(rValue.x, rValue.y);
    }

    /// <summary>
    /// Automatic conversion from Vector3 to SerializableVector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator SerializableVector2Int(Vector2Int rValue)
    {
        return new SerializableVector2Int(rValue.x, rValue.y);
    }

    public static SerializableVector2Int operator /(SerializableVector2Int lhs, int rhs)
    {
        return new SerializableVector2Int(lhs.x / rhs, lhs.y / rhs);
    }

    public static SerializableVector2Int operator +(SerializableVector2Int lhs, int rhs)
    {
        return new SerializableVector2Int(lhs.x + rhs, lhs.y + rhs);
    }

    public static SerializableVector2Int operator -(SerializableVector2Int lhs, int rhs)
    {
        return new SerializableVector2Int(lhs.x - rhs, lhs.y - rhs);
    }

    public static SerializableVector2Int operator -(int lhs, SerializableVector2Int rhs)
    {
        return new SerializableVector2Int(lhs - rhs.x, lhs - rhs.y);
    }
}
