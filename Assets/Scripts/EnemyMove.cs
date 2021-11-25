using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyMove : MonoBehaviour
{
    #region Inspector   
    public AnimationReferenceAsset dead;
    public AnimationReferenceAsset fight;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset walk;

    #endregion          //виды анимации
    float posX = 1f;
    public float speed = 2;

    public AudioSource audioFight, audioWalk,audioDead;

    public Transform[] points;
    SkeletonAnimation _skeletonAnimation;
    public string currentState, currentAnimation, previousState;
    public bool isAlive = true;
    public bool isAttack = false; 
    public float speedAni = 1f;
    public float startSpeed;

    public float waitTime;

    bool canGo = true;

    int i = 1;


    public void Start()
    {
        startSpeed = speed;
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        currentState = "Idle";
        SetCharterState(currentState);
        gameObject.transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);
    }

    void Update()
    {
        if (isAttack == true)
        {
            SetCharterState("Fight");            
        }
        else if (canGo && isAlive && isAttack == false)
        {
            if (!audioWalk.isPlaying) audioWalk.Play(); 
            SetCharterState("Walk");
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        if (transform.position == points[i].position)
        {
            if (i < points.Length - 1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
            //transform.localScale = new Vector2(posX, 1f);
            canGo = false;
            SetCharterState("Idle");
            StartCoroutine(Waiting());
        }
        Dead();
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        canGo = true;
        transform.Rotate(0f, 180f, 0f);
        //posX *= -1f;


    }
    void Dead()
    {
        if (GetComponent<EnemyAttack>().enemyHP == 0)
        {
            if (!audioDead.isPlaying) audioDead.Play();
            isAlive = false;
            SetCharterState("Dead");
        }
    }
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale) //название анимации, повтор, скорость анимации
    {
        if (animation.name.Equals(currentAnimation)) return;        //возврат, если та же анимация играет
        Spine.TrackEntry animationEntry = _skeletonAnimation.AnimationState.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
       
        currentAnimation = animation.name;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if(currentState.Equals("Dead")) Destroy(gameObject);
        if (currentState.Equals("Fight"))
        {
            gameObject.GetComponent<EnemyAttack>().Attack();
            if (!audioFight.isPlaying) audioFight.Play();
        }
    }

    void SetCharterState(string state)		//анимации
    {
        if (isAlive == true)
        {
            if (state.Equals("Walk")) SetAnimation(walk, true, speedAni);
            else if (state.Equals("Fight")) SetAnimation(fight, true, 2f);
            else SetAnimation(idle, true, 1f);

        }
        else if (currentState.Equals("Dead")) SetAnimation(dead, false, 1f);
        currentState = state;
    }

}
