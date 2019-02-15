using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Done_GameController : MonoBehaviour
{
    

    public Animator moveZoneanimator;
    public Animator fireZoneanimator;
    //gameover text animator
    public Animator game_text_animator;
    public Animator over_text_animator;
   // bool gameOver_boolean;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    //Adding Levels
    public Text levelText;
    public Text highestLevel;
    private int level;

    //Adding Highest Score
    public Text highestScoreText;
    private int highestScore;

    
    //public Text restartText;
    //public Text gameOverText;
    public GameObject restartButton;

    private bool gameOver;
    private bool restart;
    public Text scoreText;
    private int score;
    //pause menu UI
    public GameObject pauseMenuUI;
    //menu UI
    public GameObject menuUI;
    private bool sound;
    void Start()
    {
        sound = true;
        //gameOver_boolean = false;
        level = 1;
        gameOver = false;
        restart = false;
        //restartText.text = "";
        restartButton.SetActive(false);
        //gameOverText.text = "";
        levelText.text = "Level " + level;
        highestScoreText.text = "";
        highestLevel.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
 {
        levelText.text = "level " + level;
  //      if(score > highestScore)
  //    {
  //      highestScore = score;
  //    }
 }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                moveZoneanimator.SetInteger("spawn_loop_counter", i);
                fireZoneanimator.SetInteger("Spawn_loop_counter2", i);
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
                if (gameOver)
                {
                    
                    //  restartText.text = "Press 'R' for Restart";
                    //gameOver_boolean = true;
                    game_text_animator.SetBool("gameOver1", gameOver);
                    over_text_animator.SetBool("gameOver2",gameOver);
                    restartButton.SetActive(true);
                    restart = true;
                    highestScoreText.text = "HIGHEST SCORE \n"+PlayerPrefs.GetInt("highestScore",0).ToString();//Don't forget to reset by button or anything :3
                    highestLevel.text = "HIGHEST LEVEL\n" + PlayerPrefs.GetInt("highestLevel", 1).ToString();
                    break;
                }
            }
            
            if (!gameOver)
            {                            
                    hazardCount += 5;
                    level++;
                    if(level > PlayerPrefs.GetInt("highestLevel",1)){
                    PlayerPrefs.SetInt("highestLevel", level);
                }
            }
            yield return new WaitForSeconds(waveWait);
          
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        if (score > PlayerPrefs.GetInt("highestScore", 0))
        {
            PlayerPrefs.SetInt("highestScore", score);
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }


    public void GameOver()
    {
        //gameOverText.text = "Game Over!";
        gameOver = true;

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //pause menu functions
    public void pauseButtonPressed()
    {
        Time.timeScale = 0f;
        if (!sound)
        {
           AudioListener.volume=0f;
        }
        else
        {
            AudioListener.volume = 0.5f;
        }
        
        pauseMenuUI.SetActive(true);
    }
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        if (sound)
        {
            AudioListener.volume = 1f;
        }
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        
        Application.Quit();

    }

    //menu button functions
    public void menuButtonPressed()
    {
        menuUI.SetActive(true);
    }
    public void soundOff()
    {
        AudioListener.volume = 0f;
        sound = false;
        menuUI.SetActive(false);
    }
    public void soundOn()
    {
        AudioListener.volume =  1f;
        sound = true;
        pauseButtonPressed();
        menuUI.SetActive(false);
    }
    //reset button functions
    public void reset_highscore_level()
    {
        
        //PlayerPrefs.DeleteKey("highestScore");
        // PlayerPrefs.DeleteKey("highestLevel");
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        
    }
}
