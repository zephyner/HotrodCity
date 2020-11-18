using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    [SyncVar] public string matchID;
    [SyncVar] public int playerIndex;

    NetworkMatchChecker networkMatchChecker;

    void Start()
    {
        networkMatchChecker = GetComponent<NetworkMatchChecker>();

        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        else
        {
            UILobby.instance.SpawnPlayerUIPrefab(this);
        }
    }

    //HOST

    public void HostGame()
    {
        string matchID = MatchMaker.GetRandomMatchID();
        CmdHostGame(matchID);
    }

    [Command]
    void CmdHostGame(string _matchID)
    {
        matchID = _matchID;
        if (MatchMaker.instance.HostGame(_matchID, gameObject, out playerIndex))
        {
            Debug.Log($"color = green>Game hosted successfully</color>");
            networkMatchChecker.matchId = _matchID.ToGuid();
            TargetHostGame(true, _matchID);
        }
        else
        {
            Debug.Log($"color = red>Game hosted failed</color>");
            TargetHostGame(false, _matchID);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string _matchID)
    {
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.HostSuccess(success);
    }

    //JOIN

    public void JoinGame(string _inputID)
    {
        CmdJoinGame(_inputID);
    }

    [Command]
    void CmdJoinGame(string _matchID)
    {
        matchID = _matchID;
        if (MatchMaker.instance.JoinGame(_matchID, gameObject, out playerIndex))
        {
            Debug.Log($"color = green>Game joined successfully</color>");
            networkMatchChecker.matchId = _matchID.ToGuid();
            TargetJoinGame(true, _matchID);
        }
        else
        {
            Debug.Log($"color = red>Game hosted failed</color>");
            TargetJoinGame(false, _matchID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string _matchID)
    {
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.JoinSuccess(success);
    }

    //START

    public void StartGame()
    {
        CmdStartGame();
    }

    [Command]
    void CmdStartGame()
    {
        MatchMaker.instance.StartGame(matchID);
        Debug.Log($"color = red>Game Starting</color>");
    }

    public void BeginGame()
    {
        TargetStartGame();
    }

    [TargetRpc]
    void TargetStartGame()
    {
        Debug.Log($"MatchID: {matchID} | Beginning");
        //load game scene
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
