using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class J_PuzzleData
{
    public bool[] arePuzzleFinished;

    public J_PuzzleData(bool[] puzzleFlags) {
        arePuzzleFinished = new bool[6];

        arePuzzleFinished = puzzleFlags;
    }
}
