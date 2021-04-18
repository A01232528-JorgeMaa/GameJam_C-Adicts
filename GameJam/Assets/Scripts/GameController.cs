using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameOverScreen gameOverScreen;
    int maxNights = 0;

    public void GameOver()
    {
        gameOverScreen.Setup(maxNights);
    }
}
