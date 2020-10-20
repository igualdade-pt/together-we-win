using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    // Start is called before the first frame update
    private void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>().GetComponent<MainMenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
