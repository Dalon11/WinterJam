using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Destroy(GameObject.Find("Game Audio"));
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
    public void Exit()
    {
        Application.Quit();

    }
    public void Voise()
    {
        
        {
            if (AudioListener.volume == 0) AudioListener.volume = 1;
            else if (AudioListener.volume == 1) AudioListener.volume = 0;

        }

    }
}
