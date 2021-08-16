using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private string nameInputPause;
    [SerializeField] private GameObject CamvasPause_GO;
    public MouseManager mouseManager;

    public static GameManager instanceGameManager;

    private bool isPauseGame;

    public static event Action<GameManager> OnPause;

    public static event Action<GameManager> OnDispause;

    void Awake()
    {
        if (instanceGameManager == null)
        {
            instanceGameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isPauseGame = false;
        if (CamvasPause_GO != null)
            CamvasPause_GO.SetActive(false);

        if (inputManager != null)
        {
            inputManager.GetInputFunction(nameInputPause).myFunction = Pause;
            inputManager.GetInputFunction(nameInputPause).SetSpecialInput(true);
        }
    }

    public void LoadScene(string nameGameOver)
    {
        SceneManager.LoadScene(nameGameOver);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void Pause()
    {
        if (CamvasPause_GO != null)
            CamvasPause_GO.SetActive(!CamvasPause_GO.activeSelf);

        SetIsPauseGame(!isPauseGame);

        if (mouseManager != null)
        {
            if (isPauseGame)
                mouseManager.SetCursorLockState(false);
            else
                mouseManager.SetCursorLockState(true);
        }

        if (isPauseGame)
        {
            if (OnPause != null)
                OnPause(this);
        }
        else
        {
            if (OnDispause != null)
                OnDispause(this);
        }
    }

    public void Pause(bool activateCamvasPause)
    {
        if (CamvasPause_GO != null)
            CamvasPause_GO.SetActive(activateCamvasPause);

        SetIsPauseGame(activateCamvasPause);

        if (mouseManager != null)
        {
            if (isPauseGame)
                mouseManager.SetCursorLockState(false);
            else
                mouseManager.SetCursorLockState(true);
        }

        if (isPauseGame)
        {
            if (OnPause != null)
                OnPause(this);
        }
        else
        {
            if (OnDispause != null)
                OnDispause(this);
        }
    }

    public void SetIsPauseGame(bool value) => isPauseGame = value;

    public bool GetIsPauseGame() { return isPauseGame; }
}
