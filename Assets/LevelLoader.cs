using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    float levelLoadDelay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

        Invoke("LoadFirstScreen", levelLoadDelay);
    }

    private void LoadFirstScreen()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int currentSceneNum = ((currentSceneIndex + 1));
        SceneManager.LoadScene(currentSceneNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
