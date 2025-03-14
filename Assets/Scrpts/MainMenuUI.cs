using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;

    private void Start()
    {
        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OutdoorsScene");
        Debug.Log("Play Game!");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
