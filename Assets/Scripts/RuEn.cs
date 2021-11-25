using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuEn : MonoBehaviour
{
    public GameObject ru,en,
                      ru11, ru12, ru14, ru21, ru22, ru23,
                      en11, en12, en14, en21, en22, en23;

    public bool rus = false;
    void Start()
    {
        
    }

    
    void Update()
    {
       RusEng();
    }
    public void RusEng()
    {
        if(rus == true)
        {
            ru11.gameObject.SetActive(true);
            ru12.gameObject.SetActive(true);
            ru14.gameObject.SetActive(true);
            ru21.gameObject.SetActive(true);
            ru22.gameObject.SetActive(true);
            ru23.gameObject.SetActive(true);

            en11.gameObject.SetActive(false);
            en12.gameObject.SetActive(false);
            en14.gameObject.SetActive(false);
            en21.gameObject.SetActive(false);
            en22.gameObject.SetActive(false);
            en23.gameObject.SetActive(false);
        }
        else if (rus==false)
        {
            ru11.gameObject.SetActive(false);
            ru12.gameObject.SetActive(false);
            ru14.gameObject.SetActive(false);
            ru21.gameObject.SetActive(false);
            ru22.gameObject.SetActive(false);
            ru23.gameObject.SetActive(false);

            en11.gameObject.SetActive(true);
            en12.gameObject.SetActive(true);
            en14.gameObject.SetActive(true);
            en21.gameObject.SetActive(true);
            en22.gameObject.SetActive(true);
            en23.gameObject.SetActive(true);
        }
        
    }
    public void Eng()
    {
        rus = true;
        ru.gameObject.SetActive(true);
        en.gameObject.SetActive(false);
    }
    public void Rus()
    {
        rus = false;
        ru.gameObject.SetActive(false);
        en.gameObject.SetActive(true);
    }

}
