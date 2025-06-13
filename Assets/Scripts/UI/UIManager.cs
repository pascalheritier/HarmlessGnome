using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class UIManager : MonoBehaviour
{
    #region Parameters

    [Header("Game over")]
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField]
    private GameObject pauseScreen;

    [SerializeField]
    private float volumeChangeIncrement;

    [SerializeField]
    public int firstLevelBuildIndex;

    [Header("Dialogue")]
    [SerializeField]
    private GameObject dialogueBox;

    [Header("Game end")]
    [SerializeField]
    private GameObject gameEndScreen;

    private PlayerInput playerInput;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private LevelTimer levelTimer;

    #endregion

    #region Init
    private void Awake()
    {
        playerInput = FindAnyObjectByType<PlayerInput>();
        playerAttack = FindAnyObjectByType<PlayerAttack>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        levelTimer = FindAnyObjectByType<LevelTimer>();
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        ShowGameEnd(false);
        playerInput.onActionTriggered += (context) =>
        {
            if (context.action.name == InputActionConstants.UI.InputActionCancel)
                OnCancel(context);
        };
    }

    #endregion

    #region Input management

    private void OnCancel(CallbackContext context)
    {
        if (context.control.device is Mouse || !context.started)
            return;

        if (pauseScreen.activeInHierarchy)
            ShowPauseScreen(false);
        else
            ShowPauseScreen(true);
    }

    #endregion

    #region MainMenu

    public void NewGame()
    {
        RestartGame();
    }

    public void ContinueGame()
    {
        int levelToLoad = PlayerPrefs.GetInt(PlayerPrefsConstants.LevelBuildIndex, firstLevelBuildIndex);
        SceneManager.LoadScene(levelToLoad);
    }

    #endregion

    #region Game over

    public void ShowGameOver()
    {
        this.PauseGame(true);
        SoundManager.Instance.PlaySound(gameOverSound);
        gameOverScreen.SetActive(true);
    }

    #endregion

    #region Pause

    public void ShowPauseScreen(bool enabled)
    {
        if(!IsDialogeBoxShowing())
            PauseGame(enabled); // will be unpaused when dialogue is closed
        pauseScreen.SetActive(enabled);
    }

    public void SoundEffectVolume()
    {
        SoundManager.Instance.ChangeSoundEffectVolume(volumeChangeIncrement);
    }

    public void MusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(volumeChangeIncrement);
    }

    #endregion

    #region Game end

    public void ShowGameEnd(bool enabled)
    {
        PauseGame(enabled);
        gameEndScreen.SetActive(enabled);
    } 

    #endregion

    #region UI functions

    public void SaveGame()
    {
        // Use a dedicated serializer to persist other player data
        PlayerPrefs.SetInt(PlayerPrefsConstants.LevelBuildIndex, SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(firstLevelBuildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit(); // only works on build
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // exits play mode in editor
#endif
    } 

    public void PauseGame(bool enabled)
    {
        if (enabled)
        {
            Time.timeScale = 0.0f;

            // make sure no animation can take place while on pause
            if (playerAttack != null)
                playerAttack.enabled = false;
            if (playerMovement != null)
                playerMovement.enabled = false;
            if (levelTimer != null)
                levelTimer.enabled = false;
        }
        else
        {
            Time.timeScale = 1.0f;

            if (playerAttack != null)
                playerAttack.enabled = true;
            if (playerMovement != null)
                playerMovement.enabled = true;
            if (levelTimer != null)
                levelTimer.enabled = true;
        }
    }

    public bool IsPauseScreenShowing()
    {
        return pauseScreen.activeInHierarchy;
    }

    public bool IsGameOverScreenShowing()
    {
        return gameOverScreen.activeInHierarchy;
    }

    public bool IsDialogeBoxShowing()
    {
        return dialogueBox.activeInHierarchy;
    }

    #endregion
}