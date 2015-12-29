using UnityEngine;
using System.Collections;

public class SetupLevel : MonoBehaviour {

    // Sets up a level to work in multiplayer (generates paddle, etc.)

    // The paddle prefab to instantiate
    public GameObject paddle;
    
    // Use this for initialization
	void Start () 
    {
        PhotonNetwork.Instantiate("Paddle", paddle.transform.position, paddle.transform.rotation, 0);
	}
}
