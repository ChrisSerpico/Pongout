using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    // NOTE TO SELF: SINGLETONS BREAK UNITY BUTTONS UNLESS 
    // YOU WRITE A SCRIPT FOR EACH ONE
    /*
    // Singleton instance
    static LevelManager instance;

    // Destroy other instances of this object 
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Duplicate Level Manager self-destructing");
            Destroy(this.gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    */

    // Handles loading levels
	public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
        PrepareToLoad();
        Application.LoadLevel(name);
        LoadCleanup();
    }

    public void LoadLevel(int index)
    {
        Debug.Log("Level load requested for level with index " + index);
        PrepareToLoad();
        Application.LoadLevel(index);
        LoadCleanup();
    }

    // Load the next level in the build list
    public void LoadNextLevel()
    {
        LoadLevel(Application.loadedLevel + 1);
    }

    // Load the first level, and do extra setup
    public void LoadFirstLevel()
    {
        LoadLevel("Level_01_sp");
    }

    // Attempt to exit the game
    public void QuitRequest()
    {
        Debug.Log("Quit game request received");
        Application.Quit();
    }
    
    // Methods for loading and cleaning up after loading
    private void PrepareToLoad()
    {
        // anytime we load a new level reset the number of breakable bricks to 0
        Brick.breakableCount = 0;
    }

    private void LoadCleanup()
    {
        if (Application.loadedLevelName == "Win Screen" || Application.loadedLevelName == "Lose Screen")
        {
            Ball.ResetLives();
        }
    }


    // if the breakable count is less than 1, load the next level
    public void BrickDestroyed()
    {
        if (Brick.breakableCount <= 0)
        {
            LoadNextLevel();
        }
    }


}
