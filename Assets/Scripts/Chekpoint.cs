using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Chekpoint : MonoBehaviour
{
    #region Inspector   

    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset go;
    #endregion          //виды анимации
    public bool isGo = false;
    SkeletonAnimation _skeletonAnimation;
    public string currentState, currentAnimation, previousState;
    void Start()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        
       
    }

    void Update()
    {
        if (isGo == true) SetCharterState("Go");
        

        void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale) //название анимации, повтор, скорость анимации
        {
            if (animation.name.Equals(currentAnimation)) return;        //возврат, если та же анимация играет
            Spine.TrackEntry animationEntry = _skeletonAnimation.AnimationState.SetAnimation(0, animation, loop);
            animationEntry.TimeScale = timeScale;


            currentAnimation = animation.name;
        }
        void SetCharterState(string state)      //анимации
        {
            if (state.Equals("Go")) SetAnimation(go, true, 1f);

            else SetAnimation(idle, true, 2f);
          
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Player") GetComponent<AudioSource>().Play();
    }
}