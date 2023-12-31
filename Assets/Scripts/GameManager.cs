using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;
    private Spawner spawner;

    public Text scoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject newCanvas;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject exitButton;
    public int score { get; private set; }

    public AudioSource audioSource;
    //public AudioClip gameSound;

    private void Awake()
    {
        Application.targetFrameRate = 60; //������� ������ 60

        audioSource.Play();

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        gameOver.SetActive(false);

        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        exitButton.SetActive(false);
        newCanvas.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);
        exitButton.SetActive(true);
        bestScoreText.SetText($"BEST SCORE: {PlayerPrefs.GetInt("BestScore")}");
        newCanvas.SetActive(true);

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f; //������� ����� �� ����������� �.�. ��� ������ * Time.deltaTime
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        int bestScore = PlayerPrefs.GetInt("BestScore");
        if (score > bestScore) 
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        scoreText.text = score.ToString();
    }

    public void Exit ()
    {
        Application.Quit();

    }
}
