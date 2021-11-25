using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public int enemyHP = 3;
    public float timeAttack=0f;

    public float startTimeAttack = 0.7f;//скорость атаки
    public Transform attackPos;
    public float attackRange = 38.0f;   //радиус атаки
    public LayerMask isEnemy;           //кого бьет
    public int damage = 2;              //урон

    bool isWalk = true;
    void Start()
    {
        
    }

    
    void Update()
    {
        timeAttack -= Time.deltaTime;
        if (timeAttack <= 0) timeAttack = 0;
    }
    public void Attack()
    {
        if (timeAttack <= 0)
        {
            Collider2D[] toDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, isEnemy);   //помещаем всех вражеских юнитов в массив
                for (int i = 0; i < toDamage.Length; i++)
                {
                    toDamage[i].GetComponent<PlayerController>().TakeDamagePlayer(damage);     //наносим урон каждому юниту из массива
                }
                timeAttack = startTimeAttack;
        }
    }
   

    void OnDrawGizmosSelected()         //проверка
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

    }
    public void TakeDamageEnemy(int damagePlayer)             //нанесение урона
    {
        enemyHP -= damagePlayer;
    }
    void OnCollisionStay2D(Collision2D collision)       //остановка движения и начало атаки
    {

        if (collision.gameObject.tag == "Player")       //если враг косается союзника первым (или флага)
        {
            isWalk = false;
        }       
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<EnemyMove>().isAttack = true;
            gameObject.GetComponent<EnemyMove>().speed = 0f;
            
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<EnemyMove>().isAttack = false;
            gameObject.GetComponent<EnemyMove>().speed = gameObject.GetComponent<EnemyMove>().startSpeed;
        }
    }
}
