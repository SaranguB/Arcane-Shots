using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject settings;

    private static SettingsController instance;
    [SerializeField] private GameState gameState = GameState.PLAY_MODE;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button MenuButton;
    public static SettingsController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        replayButton.onClick.AddListener(ReloadScene);
        MenuButton.onClick.AddListener(MainLobby);
    }


    public void EnableSettings()
    {
        SoundManager.Instance.PlaySound(Sounds.BUTTON_CLCK);
        settings.SetActive(true);
        gameState = GameState.PAUSE_MODE;


    }

    public void DisableSettings()
    {
        SoundManager.Instance.PlaySound(Sounds.BUTTON_CLCK);
        settings.SetActive(false);
        gameState = GameState.PLAY_MODE;

    }

    public GameState GetGameState()
    {
        return gameState;
    }

    private void ReloadScene()
    {
        SoundManager.Instance.PlaySound(Sounds.BUTTON_CLCK);
        SoundManager.Instance.PlayMusic(Sounds.MUSIC);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

    }

    private void MainLobby()
    {

        SoundManager.Instance.PlaySound(Sounds.BUTTON_CLCK);
        SoundManager.Instance.PlayMusic(Sounds.MUSIC);
        SceneManager.LoadScene(0);
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
    }
}
