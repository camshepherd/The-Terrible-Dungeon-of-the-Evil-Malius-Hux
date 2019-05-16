using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedSceneChange : MonoBehaviour
{
    public float delay;
    float timer;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        GameStores.addGameComplete();
        StartCoroutine(changeScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator changeScene()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
