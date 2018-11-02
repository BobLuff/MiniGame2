using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Enable : MonoBehaviour {

    [SerializeField]
    private TrackGraphic _trackGraphic;
    [SerializeField]
    private Player_Control _playerControl;



    public void SetPlayerState(bool playerState)
    {
        _playerControl.enabled = playerState;
        _trackGraphic.enabled = playerState;
    }
}
