using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private Button playButton;
    
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(playGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
    }


    private void playGame()
    {

        SoundManager.Instance.PlaySound(Sounds.BUTTON_CLCK);
        SoundManager.Instance.PlayMusic(Sounds.MUSIC);
        SceneManager.LoadScene(1);
    }
}
