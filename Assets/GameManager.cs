using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public Image fadeImage;
    public Image jumpscareGamerImage;
    public Image jumpscareMonsterImage;
    public float fadeDuration = 1f;
    public float startDelay = 1f;
    public Stopwatch stopwatch;
    public TMP_Text gameOverText;
    //public float fadeDuration = 2f;

    public Button menuButton;
    public Button replayButton;

    public GameObject pauseMenuUI;
    public GameObject backPause;
    private bool isPaused = false;
    private bool isEnd = false;

    public MouseLook mouseLook;





    public void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void Start()
    {
        StartCoroutine(StartGame());
        //canvasGroup = button.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isEnd)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.mouseSensitivity = 5f;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        backPause.SetActive(false);
        isPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        mouseLook.mouseSensitivity = 0f;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        backPause.SetActive(true);
        isPaused = true;
    }


    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(startDelay);

        Cursor.lockState = CursorLockMode.Locked;

        stopwatch.StartStopwatch();
        backPause.SetActive(false);

        float timer = 0f;
        Color initialColor = fadeImage.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            fadeImage.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
        
    }

    private void FadeInText(bool monsterWin)
    {
        gameOverText.gameObject.SetActive(true);

        Color initialColor = fadeImage.color;
        Color targetColor = Color.white;

        if (monsterWin)
        {
            gameOverText.text = "THE MONSTER IS VICTORIOUS";
        } else
        {
            gameOverText.text = "THE HUMAN HAS ESCAPED";
        }

        StartCoroutine(FadeText(initialColor, targetColor));
    }

    private IEnumerator FadeText(Color initialColor, Color targetColor)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            gameOverText.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        // End the game
        //EndGame();
    }

    private void FadeInImage()
    {
        fadeImage.gameObject.SetActive(true);

        Color initialColor = fadeImage.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);


        StartCoroutine(FadeImage(initialColor, targetColor));
    }

    private IEnumerator FadeImage(Color initialColor, Color targetColor)
    {
        float timer = 0f;
        

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            fadeImage.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        yield return new WaitForSeconds(startDelay);

        //menuButton.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);


    }



    public void EndGame(bool monsterWin)
    {
        // Perform game-over actions, such as displaying a score, showing a menu, or quitting the application
        // Example: Application.Quit();

        Cursor.lockState = CursorLockMode.Confined;

        isEnd = true;

        stopwatch.PauseStopwatch();

        FadeInText(monsterWin);

        FadeInImage();



        //menuButton.gameObject.SetActive(true);
        //replayButton.gameObject.SetActive(true);

        
    }


}
