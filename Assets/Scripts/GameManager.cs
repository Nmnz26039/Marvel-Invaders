using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int lives = 3;
    public static bool playGame = true;
    public static int nbEnemy = 66;
    public static int touchPlayer = 0;
    public static int score = 0; 

    public Text livesText;
    public Text endScreen;
    public Text scoreText;

    public Snap snapThanos;
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;     
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        
        if (lives == 0)
        {
            endScreen.text = "YOU LOSE, LOSER";
            Vector3 position = new Vector3(0,0,0);
            snapThanos.transform.position = position; 
        }
        else if (nbEnemy == 0)
        {
            endScreen.text = "YOU WIN !";
        }
    }
}
