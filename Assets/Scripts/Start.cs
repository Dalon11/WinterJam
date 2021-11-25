using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Start : MonoBehaviour
{
    public GameObject _audio, _dog,_convas, _mainCamera;
    
  
    void Update()
    {
        if(_dog != null) Destroy(_dog, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
       {
        DontDestroyOnLoad(_audio );
            //DontDestroyOnLoad(_convas);
            //DontDestroyOnLoad(_mainCamera);


            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
           // _player.transform.position =new Vector2(-165.7f, 26.5f);
            
        }
    }
}
