using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AnimatonSpain : MonoBehaviour
{
	#region Inspector	 //виды анимации
	public AnimationReferenceAsset idle;
	public AnimationReferenceAsset fear;
	public AnimationReferenceAsset run;
	#endregion
	public string currentAnimation;		//текущая анимация
	public string currentState;			//текущее состояние
	SkeletonAnimation skeletonAnimation;
	public float speed = 20;

	void Start()
	{
		
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		currentState = "Run";
		SetCharterState(currentState);
	}

    private void Update()
    {
		gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
	}
    public  void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)	//название анимации, повтор, скорость анимации
	{
		if (animation.name.Equals(currentAnimation)) return;        //возврат, если та же анимация играет
		skeletonAnimation.AnimationState.SetAnimation(0, animation, loop).TimeScale=timeScale;
		currentAnimation = animation.name;	
	}
	public void SetCharterState(string state)		//анимации
    {
		if (state.Equals("Idle")) SetAnimation(idle, true, 1f);
		else if (state.Equals("Run")) SetAnimation(run, true, 1f);
		else if (state.Equals("Fear")) SetAnimation(fear, true, 1f);
    }
}

