using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class ViewportHelper
{
    /// <summary> 
    /// Helper valiable checking if right upper corner was already determined.
    /// </summary>
    private static bool _rightUpperCornerInit = false;

    /// <summary> 
    /// Helper valiable checking if left lower corner was already determined.
    /// </summary>
    private static bool _leftDownCornerInit = false;

    /// <summary> 
    /// Private field with vector of right upper corner.
    /// </summary>
    private static Vector2 _rightUpperCorner;

    /// <summary> 
    /// Private field with vector of left lower corner.
    /// </summary>
    private static Vector2 _leftDownCorner;


    /// <summary> 
    /// Public property returning the vector of right upper corner.
    /// </summary>
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
    
    /// <summary> 
    /// Public property returning the vector of left lower corner.
    /// </summary>
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
    
    /// <summary> 
    /// Get viewport width.
    /// </summary>
    /// <returns> 
    /// Viewport width.
    /// </returns>
    public static float GetViewportWidth() {
        return RightUpperCorner.x - LeftDownCorner.x;
    }

    /// <summary> 
    /// Get viewport height.
    /// </summary>
    /// <returns> 
    /// Viewport height.
    /// </returns>
    public static float GetViewportHeight() {
        return RightUpperCorner.y - LeftDownCorner.y;
    }
}