using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    //TODO make this a singleton


    // Handles loading levels
	public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
        PrepareToLoad();
        Application.LoadLevel(name);
    }

    public void LoadLevel(int index)
    {
        Debug.Log("Level load requested for level with index " + index);
        PrepareToLoad();
        Application.LoadLevel(index);
    }

    // Load the next level in the build list
    public void LoadNextLevel()
    {
        LoadLevel(Application.loadedLevel + 1);
    }

    // Attempt to exit the game
    public void QuitRequest()
    {
        Debug.Log("Quit game request received");
        Application.Quit();
    }
    
    private void PrepareToLoad()
    {

    }


}
