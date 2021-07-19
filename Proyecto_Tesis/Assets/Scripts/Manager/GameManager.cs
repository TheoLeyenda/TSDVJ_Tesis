using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private string nameInputPause;
    [SerializeField] private GameObject CamvasPause_GO;
    [SerializeField] private MouseManager mouseManager;

    public static GameManager instanceGameManager;

    private bool isPauseGame;

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
        if(CamvasPause_GO != null)
            CamvasPause_GO.SetActive(!CamvasPause_GO.activeSelf);

        SetIsPauseGame(!isPauseGame);

        if(mouseManager != null)
            mouseManager.SetCursorLockState(!mouseManager.GetLockCursor());
    }

    public void Pause(bool activateCamvasPause)
    {
        if (CamvasPause_GO != null && activateCamvasPause)
            CamvasPause_GO.SetActive(!CamvasPause_GO.activeSelf);

        SetIsPauseGame(!isPauseGame);

        if (mouseManager != null)
            mouseManager.SetCursorLockState(!mouseManager.GetLockCursor());
    }

    public void SetIsPauseGame(bool value) => isPauseGame = value;

    public bool GetIsPauseGame() { return isPauseGame; }
}
