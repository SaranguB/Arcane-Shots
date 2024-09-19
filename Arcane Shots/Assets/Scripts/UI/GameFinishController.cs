using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFinishController : MonoBehaviour
{
    [SerializeField] public GameObject Won;
 
    [SerializeField] private Button MenuButton ;

    private void Awake()
    {
        MenuButton.onClick.AddListener(Menu);
       
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayerWon()
    {
        SettingsController.Instance.SetGameState(GameState.PAUSE_MODE);
        SoundManager.Instance.PlayMusic(Sounds.GAME_FINISHED);
        Won.SetActive(true);
    }

}
