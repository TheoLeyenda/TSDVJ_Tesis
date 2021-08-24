using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class J_SaveSystem : MonoBehaviour
{
    #region Player Save/Load
    public static void SavePlayer(GameObject playerGO)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        J_PlayerData data = new J_PlayerData(playerGO);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static J_PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            J_PlayerData data = formatter.Deserialize(stream) as J_PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Archivo no encontrado en " + path);
            return null;
        }
    }
    #endregion

    #region Puzzles Save/Load
    public static void SavePuzzles(bool[] puzzlesStates)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/puzzles.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        J_PuzzleData data = new J_PuzzleData(puzzlesStates);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static J_PuzzleData LoadPuzzles()
    {
        string path = Application.persistentDataPath + "/puzzles.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            J_PuzzleData data = formatter.Deserialize(stream) as J_PuzzleData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Archivo no encontrado en " + path);
            return null;
        }
    }
    #endregion
}
