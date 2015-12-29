using UnityEngine;
using System.Collections;
using Photon;

public class SmoothPosition : Photon.MonoBehaviour {

	// Smooths positioning over network of the gameobject it's attached to
    private Vector3 correctPos;
    private Quaternion correctRot;

    private void Update()
    {
        // check to see whether this object is owned by us
        if (!photonView.isMine)
        {
            // if not, it's another player's, so smooth its movement
            transform.position = Vector3.Lerp(transform.position, this.correctPos, Time.deltaTime * 15f);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctRot, Time.deltaTime * 15f);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // we own this player, send others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, recieve data
            this.correctPos = (Vector3)stream.ReceiveNext();
            this.correctRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
