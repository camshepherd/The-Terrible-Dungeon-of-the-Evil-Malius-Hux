using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleMenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        // do some scene loading stuff here
        //SceneManager.LoadScene("Dungeon");
        GameStores.ResetState();
        SceneManager.LoadScene("IntroScene");
        //print("Loading the scene!");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

}
