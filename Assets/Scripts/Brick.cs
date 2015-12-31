using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    // Variables for tracking when to break the block
    public int maxHits;
    private int timesHit;
    private bool isBreakable;

    // Keeps track of how many breakable bricks there are left
    public static int breakableCount = 0;
    private LevelManager levelManager; // For messaging about destroyed bricks
    
    // Use this for initialization
	void Start () 
    {
        timesHit = 0;
        isBreakable = (this.tag == "Breakable");

        levelManager = GameObject.FindObjectOfType<LevelManager>();

        // keep track of how many breakable bricks there are
        if (isBreakable)
        {
            breakableCount++;
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreakable)
        {
            HandleHits();
        }
    }

    private void HandleHits()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            Destroy(gameObject);
        }
    }
}
