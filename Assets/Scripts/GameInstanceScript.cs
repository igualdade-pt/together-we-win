using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceScript : MonoBehaviour
{
    /// <summary>
    /// Save Variables Between Scene
    /// </summary>


    private int indexLanguage = 0;

    private int indexLevel = 0;

    public int LanguageIndex
    {
        get { return indexLanguage; }
        set { indexLanguage = value; }
    }

    public int LevelIndex
    {
        get { return indexLevel; }
        set { indexLevel = value; }
    }

}
