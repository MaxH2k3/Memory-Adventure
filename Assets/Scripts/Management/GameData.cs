using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Health;
    public int Gold;
    public Vector3 PlayerPosition;
    public int Scence;

    public GameData()
    {
        this.Health = PlayerController.Instance.playerState.health;
        this.Gold = EconomyManager.Instance.currentGold;
        this.PlayerPosition = PlayerController.Instance.transform.position;
        this.Scence = 1;
    }
}
