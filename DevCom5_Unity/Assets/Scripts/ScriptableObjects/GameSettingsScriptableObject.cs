using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettingsScriptableObject", order = 1)]
public class GameSettingsScriptableObject : ScriptableObject
{
    public int startingResources = 200;
}