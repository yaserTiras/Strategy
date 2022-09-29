using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public delegate void GameStatus(bool? success = null);
    public event GameStatus onGameStarted;
    public event GameStatus onGameEnded;


    public static bool isGameStarted = false;


    public void StartGame()
    {
        if (onGameStarted != null)
            onGameStarted();
        isGameStarted = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame(bool s)
    {
        if (!isGameStarted)
        {
            return;
        }
        isGameStarted = false;
        onGameEnded(s);
    }

    public override void Initialize()
    {
        Application.targetFrameRate = 60;
        EventsManager.instance.InitializeGame();
    }
}
