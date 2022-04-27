using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("BoardGame",LoadSceneMode.Single);
    }
    public void Scene3()
    {
        SceneManager.LoadScene("Final");
    }

}