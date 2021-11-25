using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject _1th, _2th, _3th;
    void Start()
    {
        StartCoroutine(First());
    }

    
    void Update()
    {
        
    }
    IEnumerator First()
    {
        yield return new WaitForSeconds(2f);    //ждём
        StartCoroutine(Second());
    }
    IEnumerator Second()
    {
        yield return new WaitForSeconds(3f);    //ждём
        _2th.SetActive(true);
        _1th.SetActive(false);
        StartCoroutine(Three());
    }
    IEnumerator Three()
    {
        yield return new WaitForSeconds(4f);    //ждём
        _3th.SetActive(true);
        _2th.SetActive(false);
        
    }
   public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
