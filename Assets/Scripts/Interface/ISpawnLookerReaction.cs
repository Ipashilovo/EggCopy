using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnLookerReaction
{
    void InitValue(int value);
    void OnLevelLoose();
    void OnChangeValue();
    void OnLevelWin();
}
