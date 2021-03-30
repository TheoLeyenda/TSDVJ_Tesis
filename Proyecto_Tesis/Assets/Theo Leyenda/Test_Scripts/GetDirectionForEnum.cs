using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDirectionForEnum : MonoBehaviour
{
    public enum Direction
    {
        forward,
        back,
        up,
        down,
        left,
        right,
    }
    public Vector3 GetVectorDirection(Direction directionUse)
    {
        Vector3 direction = Vector3.zero;
        switch (directionUse)
        {
            case Direction.forward:
                direction = Vector3.forward;
                break;
            case Direction.back:
                direction = Vector3.back;
                break;
            case Direction.up:
                direction = Vector3.up;
                break;
            case Direction.down:
                direction = Vector3.down;
                break;
            case Direction.left:
                direction = Vector3.left;
                break;
            case Direction.right:
                direction = Vector3.right;
                break;
        }
        return direction;
    }
}
