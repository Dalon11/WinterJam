using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLvlScene : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(First());
    }

   
    IEnumerator First()
    {
        yield return new WaitForSeconds(2f);    //ждём
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
