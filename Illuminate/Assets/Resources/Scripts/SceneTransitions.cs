using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public string SceneName;

    private Scene scene;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
        
    }
    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneName);
    }
}


