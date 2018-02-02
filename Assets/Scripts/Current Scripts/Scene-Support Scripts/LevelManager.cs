using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void loadLevel (int level)
    {
        //Application.LoadLevel(level);
    }


    public void quitRequest ()
    {
        Application.Quit();
    }
}
