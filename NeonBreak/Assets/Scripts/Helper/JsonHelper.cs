using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class JsonHelper
{
    /// <summary> 
    /// Converts JSON string to array.
    /// </summary>
    /// <param name="json"> 
    /// String in JSON format to convert.
    /// </param>
    /// <returns> 
    /// List with items.
    /// </returns>
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    /// <summary> 
    /// Converts array to json string.
    /// </summary>
    /// <param name="array"> 
    /// Array of items to convert.
    /// </param>
    /// <returns> 
    /// JSON string.
    /// </returns>
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    /// <summary> 
    /// Wrapper model for array to JSON convertion.
    /// </summary>
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}