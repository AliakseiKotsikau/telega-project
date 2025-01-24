using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private const int WORDS_GAME_SCENE_INDEX = 1;

    public void OnWordsGameButtonClick()
    {
        SceneManager.LoadScene(WORDS_GAME_SCENE_INDEX);
    }
}
