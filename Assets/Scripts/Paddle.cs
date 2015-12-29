using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	// Update is called once per frame
	void Update () 
    {
        MoveWithMouse();
	}

    private void MoveWithMouse()
    {
        Vector3 paddlePos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        float mousePosInBlocks = (Input.mousePosition.x - Screen.width/2) / Screen.width * 16;  // magic number is the number of unity units onscreen TODO put this in a const somewhere
        paddlePos.x = Mathf.Clamp(mousePosInBlocks, -7, 7);
        this.transform.position = paddlePos;
    }
}
