using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class ViewportHelper
{
    private static bool _rightUpperCornerInit = false;
    private static bool _leftDownCornerInit = false;
    private static Vector2 _rightUpperCorner;
    private static Vector2 _leftDownCorner;

    public static Vector2 RightUpperCorner{
        get{
            if(!_rightUpperCornerInit) {
                _rightUpperCorner = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
                _rightUpperCornerInit = true;
            }
            return _rightUpperCorner;
        }
        private set{}
    }
    
    public static Vector2 LeftDownCorner{
        get{
            if(!_leftDownCornerInit) {
                _leftDownCorner = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
                _leftDownCornerInit = true;
            }
            return _leftDownCorner;
        }
        private set{}
    }
    
    public static float GetViewportWidth() {
        return RightUpperCorner.x - LeftDownCorner.x;
    }

    public static float GetViewportHeight() {
        return RightUpperCorner.y - LeftDownCorner.y;
    }
}