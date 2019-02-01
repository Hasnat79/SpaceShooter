using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


public class Done_GameController : MonoBehaviour
{
    public Animator animator;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    //Adding Levels
    public Text levelText;
    private int level;

    public Text scoreText;
    //public Text restartText;
    public Text gameOverText;
    public GameObject restartButton;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        level = 1;
        gameOver = false;
        restart = false;
        //restartText.text = "";
        restartButton.SetActive(false);
        gameOverText.text = "";
        levelText.text = "Level " + level;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
 {
        levelText.text = "level " + level;
 }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                animator.SetInteger("spawn_loop_counter", i);
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
                

            }

            yield return new WaitForSeconds(waveWait);
           
            if (gameOver)
            {
                //  restartText.text = "Press 'R' for Restart";
                restartButton.SetActive(true);
                restart = true;
                break;
            }else
            {
                hazardCount += 5;
                level++;
            }


        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}