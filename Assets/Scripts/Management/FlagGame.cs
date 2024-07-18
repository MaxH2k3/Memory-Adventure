using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGame : Singleton<FlagGame>
{
    public bool IsLoadScene = true;
    public bool IsLoadGame = true;

    public void ResetFlag()
    {
        IsLoadGame = true;
    }

}
