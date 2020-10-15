using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }


    private void LoadLevelIndex(int index) => print("Level Index = " + index);

    public void ButtonLevelSelected(int levelIndex) => LoadLevelIndex(levelIndex); 
    
}