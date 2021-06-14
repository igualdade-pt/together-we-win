using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceScript : MonoBehaviour
{
    /// <summary>
    /// Save Variables Between Scene
    /// </summary>


    private int indexLanguage = -1;

    private int indexLevel = 0;

    private int indexLevelDifficulty = 0;

    private bool cameFromStartMenu = false;

    private bool unlockNewLevel = false;

    /// <summary>
    /// Index of the chosen language
    /// </summary>
    public int LanguageIndex
    {
        get { return indexLanguage; }
        set { indexLanguage = value; }
    }

    /// <summary>
    /// Index of the chosen level
    /// </summary>
    public int LevelIndex
    {
        get { return indexLevel; }
        set { indexLevel = value; }
    }

    /// <summary>
    /// Index of the chosen difficulty ; 0 - Basic , 1 - Expert
    /// </summary>
    public int DifficultyLevelIndex
    {
        get { return indexLevelDifficulty; }
        set { indexLevelDifficulty = value; }
    }


    /// <summary>
    /// The player came from the start menu
    /// </summary>
    public bool CameFromStartMenu
    {
        get { return cameFromStartMenu; }
        set { cameFromStartMenu = value; }
    }

    /// <summary>
    /// The player unlock new Level
    /// </summary>
    public bool UnlockNewLevel
    {
        get { return unlockNewLevel; }
        set { unlockNewLevel = value; }
    }

}
