using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_SaveManager : MonoBehaviour
{
    public GameObject playerGO;

    public int maxPuzzles = 6;
    bool[] puzzlesStates;
    int targetId;
    bool targetPuzzleResult;
    J_PuzzleOnLoadActions puzzleOnLoadActions;

    public void Save()
    {
        J_SaveSystem.SavePlayer(playerGO);
        J_SaveSystem.SavePuzzles(puzzlesStates);
    }

    public void Load()
    {
        //===============PLAYER=======================
        J_PlayerData data = J_SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.playerPosition[0];
        position.y = data.playerPosition[1];
        position.z = data.playerPosition[2];
        playerGO.transform.position = position;

        Quaternion rotation;
        rotation.x = data.playerRotation[0];
        rotation.y = data.playerRotation[1];
        rotation.z = data.playerRotation[2];
        rotation.w = data.playerRotation[3];
        playerGO.transform.rotation = rotation;

        for (int i = 0; i < data.itemIDS.Length; i++)
        {
            J_inventoryManager.instance.AddItem(data.itemIDS[i]);
        }
        //===============PLAYER=======================

        //===============PUZZLES=======================
        J_PuzzleData puzzleData = J_SaveSystem.LoadPuzzles();

        puzzlesStates = puzzleData.arePuzzleFinished;

        for (int i = 0; i < puzzlesStates.Length; i++)
        {
            if (puzzlesStates[i] == true)
            {
                if (i == 5) //Medio feo esto, pero por ahora safa
                {
                    puzzleOnLoadActions.ActionPuzzle6.Invoke();
                }
            }
        }
        //===============PUZZLES=======================
    }

    // Start is called before the first frame update
    void Start()
    {
        puzzlesStates = new bool[maxPuzzles];

        puzzleOnLoadActions = FindObjectOfType<J_PuzzleOnLoadActions>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPuzzleState(int puzzleID, bool isResolved) //La Id de los puzzles es el orden en el array bool[] puzzlesStates (0 es el puzzle 1, 1 el 2 y asi)
    {
        puzzlesStates[puzzleID] = isResolved;
    }
    public void SetPuzzleId(int id)
    {
        if (id > maxPuzzles || id < 0)
        {
            Debug.Log("Not valid puzzleID");
            return;
        }

        targetId = id;
    }
    public void SetPuzzleResult(bool result)
    {
        targetPuzzleResult = result;
    }
    public void SendIdandResult()
    {
        SetPuzzleState(targetId, targetPuzzleResult);
    }
}
