using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
   [SerializeField] public GameObject Lose;
    [SerializeField] private Button MenuButton1;
    private void Awake()
    {
        MenuButton1.onClick.AddListener(Menu);

    }
    public void PlayerLose()
    {
        SoundManager.Instance.PlayMusic(Sounds.DEATH_SOUND);
        Lose.SetActive(true);
    }

    public void Menu()
    {
        SoundManager.Instance.PlayMusic(Sounds.MUSIC);

        SceneManager.LoadScene(0);
    }
}
