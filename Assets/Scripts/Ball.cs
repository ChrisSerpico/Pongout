using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	// initial velocity of the ball
    public Vector2 initVelocity;

    // the paddle this ball is attached to
    private Paddle paddle; 

    // a vector that gets the relative position of the paddle
    private Vector3 paddleToBallVector;

    // whether the game has started 
    private bool hasStarted = false;  

    // the number of lives this ball currently has
    public int lives;

    // reference to level manager
    private LevelManager levelManager;
    
    // Use this for initialization
	void Start () 
    {
	    // find the paddle
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;

        // find the level manager
        levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!hasStarted)
        {
            // lock the ball relative to the paddle
            this.transform.position = paddle.transform.position + paddleToBallVector;

            // wait for a mouse press to launch 
            if (Input.GetMouseButtonDown(0))
            {
                this.GetComponent<Rigidbody2D>().velocity = initVelocity;
                hasStarted = true;
            }
        }
	}

    // on collision, allow the player to redirect the ball by hitting it with different parts of the paddle
    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 tweak = GetComponent<Rigidbody2D>().velocity;

        if (collision.collider.gameObject.tag == "Paddle")
        {
            tweak.x = initVelocity.x * ((this.transform.position.x - paddle.transform.position.x) / (paddle.GetComponent<Collider2D>().bounds.size.x / 2)) * 2f;
            tweak.y = initVelocity.y;
            //Debug.Log(tweak);
        }

        if (hasStarted)
        {
            GetComponent<Rigidbody2D>().velocity = tweak;
        }
    }

    // Lose a life, if there are fewer than 0 lives left, go to lose screen
    public void LoseLife()
    {
        if (lives >= 1)
        {
            lives--;
            hasStarted = false;
            Debug.Log(lives);
        }
        else
        {
            levelManager.LoadLevel("LoseScreen");
        }
    }
}
