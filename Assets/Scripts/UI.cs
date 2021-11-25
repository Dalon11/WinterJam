using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject _pause,_player,_exitMenu;
    public Text goldTxt, healTxt;
    public static int _gold=0;
    public static int _heal =3;

    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        _heal = _player.gameObject.GetComponent<PlayerController>().heal;
        _gold = _player.gameObject.GetComponent<PlayerController>().gold;        
        goldTxt.text = (""+_gold);
        healTxt.text = ("" + _heal);
       
    }


    public void Pause()
    {
        
        Time.timeScale = 0; //пауза
        _pause.SetActive(true);
        _exitMenu.SetActive(false);
    }
    public void Play()
    {

        Time.timeScale = 1; //пауза
        _pause.SetActive(false);
        _exitMenu.SetActive(false);
    }
    public void ExitMenu()
    {
        _pause.SetActive(false);
        _exitMenu.SetActive(true);
        
    }
    public void Yes()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    public void Volue()
    {
        if(AudioListener.volume == 0) AudioListener.volume = 1;
        else if (AudioListener.volume == 1) AudioListener.volume = 0;

    }
}

