using UnityEngine;
using System.Collections;
using Photon;
using UnityEngine.UI;

public class NetworkHandler : PunBehaviour 
{
    // THERE SHOULD ONLY BE ONE INSTANCE OF THIS AT ANY GIVEN TIME
    static NetworkHandler instance = null;


    // a prefab of the button used to join games
    public GameObject joinButton;
    // a prefab of the button used to create games
    public GameObject createButton;

    // a list of possible room properties
    string[] roomProps = { "map" };

	// Instancing
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Duplicate Network Handler self-destructing");
            Destroy(this.gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    
    // Use this for initialization
	void Start () 
    {
        PhotonNetwork.ConnectUsingSettings("pre-alpha");
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
	}

    // Called once the player is in the lobby
    public override void OnJoinedLobby()
    {
        // allow them to create a game and update the room list
        Debug.Log("Successfully joined lobby");
        UpdateRoomList();
        GameObject newButton = (GameObject)Instantiate(createButton);
        newButton.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
        newButton.GetComponent<Button>().onClick.AddListener(() => { CreateRoom(); });
    }

    // create a new room
    public void CreateRoom()
    {
        // hashtable for room properties
        // TODO create methods to allow player to define this better
        ExitGames.Client.Photon.Hashtable customRoomProps = new ExitGames.Client.Photon.Hashtable() { { "map", 1 } };
        RoomOptions roomOptions = new RoomOptions() { customRoomProperties = customRoomProps, customRoomPropertiesForLobby = roomProps };
        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        // TODO find the map the room is using and load that
        //PhotonNetwork.room.customProperties.TryGetValue
        GameObject.FindObjectOfType<LevelManager>().LoadLevelMultiplayer("DefaultLevel"); // THIS IS A HACK 
        //Debug.Log(PhotonNetwork.room.name);
    }

    // update the room list with games that are available to join
    public void UpdateRoomList()
    {
        RoomInfo[] roomList = PhotonNetwork.GetRoomList();
        if (roomList.Length > 0)
        {
            GameObject newButton = (GameObject)Instantiate(joinButton);
            newButton.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
            //newButton.GetComponent<Text>().text = ("Room 0: " + roomList[0].playerCount + "/" + roomList[0].maxPlayers);
            newButton.GetComponent<Button>().onClick.AddListener(() => { PhotonNetwork.JoinRoom(roomList[0].name); });
        }
    }
}
