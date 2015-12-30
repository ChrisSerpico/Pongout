using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    // Variables for tracking when to break the block
    public int maxHits;
    private int timesHit;
    private bool isBreakable;
    
    // Use this for initialization
	void Start () 
    {
        timesHit = 0;
        isBreakable = (this.tag == "Breakable");
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
            Destroy(gameObject);
        }
    }
}
