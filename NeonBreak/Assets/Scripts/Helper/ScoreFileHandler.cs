using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public static class ScoreFileHandler{

    /// <summary> 
    /// Save as JSON in persistent storage.
    /// </summary>
    /// <param name="toSave"> 
    /// List of items to save.
    /// </param>
    /// <param name="filename"> 
    /// Filename in persistent storage.
    /// </param>
    public static void SaveToJSON<T>(List<T> toSave, string filename) {
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(GetPath(filename), content);
    }

    /// <summary> 
    /// Read from persistent storage.
    /// </summary>
    /// <param name="filename"> 
    /// Filename that is been read.
    /// </param>
    public static List<T> ReadFromJSON<T>(string filename) {
        string content = ReadFile(GetPath(filename));

        if(string.IsNullOrEmpty(content) || content == "{}") {
            return new List<T>();
        }

        List<T> res = JsonHelper.FromJson<T>(content).ToList();

        return res;

    }

    /// <summary> 
    /// Get path for storage place.
    /// </summary>
    /// <param name="filename"> 
    /// Path ends in this file.
    /// </param>
    /// <returns> 
    /// Storage path as string.
    /// </returns>
    private static string GetPath(string filename) {
        return Application.persistentDataPath + "/" + filename;
    }

    /// <summary> 
    /// Write file to storage.
    /// </summary>
    /// <param name="path"> 
    /// Path to storage.
    /// </param>
    /// <param name="content"> 
    /// String that will be saved.
    /// </param>
    private static void WriteFile(string path, string content) {
;
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter (fileStream)) {
            writer.Write(content);
        }
    }

    /// <summary> 
    /// Read string from storage.
    /// </summary>
    /// <param name="path"> 
    /// Path to storage.
    /// </param>
    /// <returns> 
    /// String from storage.
    /// </returns>
    private static string ReadFile(string path) {
        if(File.Exists(path)) {
            using(StreamReader reader = new StreamReader(path)) {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }

}