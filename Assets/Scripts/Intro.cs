using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject _1th, _2th, _3th, _4th;
    public GameObject feer;
    void Start()
    {
        StartCoroutine(First());
    }


    void Update()
    {

    }
    IEnumerator First()
    {
        yield return new WaitForSeconds(4f);    //ждём
        StartCoroutine(Second());
    }
    IEnumerator Second()
    {
        yield return new WaitForSeconds(4f);    //ждём
        _2th.SetActive(true);
        _1th.SetActive(false);
        StartCoroutine(Three());
    }
    IEnumerator Three()
    {
        yield return new WaitForSeconds(4f);    //ждём
        feer.SetActive(true);
        _3th.SetActive(true);
        _2th.SetActive(false);
        StartCoroutine(Fore());

    }
    IEnumerator Fore()
    {
        yield return new WaitForSeconds(2f);    //ждём
        _4th.SetActive(true);
        _3th.SetActive(false);
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(4f);    //ждём
        SceneManager.LoadScene("First lvl");
    }
}
