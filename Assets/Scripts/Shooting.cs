using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed;
    public GameObject bul;
    public int damagePlayer = 1;
    public float timeAttack, startTimeAttack, attackRange;
    public LayerMask isEnemy;
    void Start()
    {
        bul = gameObject;
        StartCoroutine(Timer());
    }

    
    void Update()
    {
        gameObject.transform.Translate(Vector2.right * speed*Time.deltaTime);
        //gameObject.GetComponent<Rigidbody2D>().velocity=(Vector2.right * speed * Time.deltaTime);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag!="Player")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                DamageBull();

                // Destroy(bul);


            }
            Destroy(bul);
        }       
    }
    IEnumerator Timer()
    { 
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    public void DamageBull()
    {
        if (timeAttack <= 0)
        {
            Collider2D[] toDamage = Physics2D.OverlapCircleAll(bul.transform.position, attackRange, isEnemy);   //помещаем всех вражеских юнитов в массив
            for (int i = 0; i < toDamage.Length; i++)
            {
                toDamage[i].GetComponent<EnemyAttack>().TakeDamageEnemy(damagePlayer);     //наносим урон каждому юниту из массива
            }
            timeAttack = startTimeAttack;
        }
        else
        {
            timeAttack -= Time.deltaTime;
        }
    }
}
