using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool useSkipKey = false;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (useSkipKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchScene(sceneName);
            }
        }
    }

    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
