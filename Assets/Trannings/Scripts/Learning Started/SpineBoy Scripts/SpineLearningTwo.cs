using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineLearningTwo : MonoBehaviour {

	// Use this for initialization

	[SpineAnimation]
	public string walkAnimation;

	[SpineAnimation]
	public string runAnimation;

	[SpineAnimation]
	public string runToIdleAnimation;

	[SpineAnimation]
	public string idleTurnAnimation;

	[SpineAnimation]
	public string idleAnimation;

	public float runDuration;
	private SkeletonAnimation skeletonAnimation;
	public Spine.AnimationState animationState;

	void Start () {
		skeletonAnimation = GetComponent<SkeletonAnimation> ();
		animationState = skeletonAnimation.AnimationState;
		StartCoroutine (DoDemoRoutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator DoDemoRoutine(){
		while(true){
			animationState.SetAnimation (0, walkAnimation, true);
			yield return new WaitForSeconds (runDuration);

			animationState.SetAnimation (0,runAnimation,true);
			yield return new WaitForSeconds (runDuration);

			animationState.SetAnimation (0,runToIdleAnimation,false);
			animationState.AddAnimation (0, idleAnimation, false, 0);
			yield return new WaitForSeconds (1f);

			skeletonAnimation.skeleton.flipX = true;
			animationState.SetAnimation (0, idleTurnAnimation, false);
			animationState.AddAnimation (0, idleAnimation, false, 0);
			yield return new WaitForSeconds (0.5f);

			animationState.SetAnimation (0,idleTurnAnimation,false);
			animationState.AddAnimation (0, idleAnimation, false, 0);
			skeletonAnimation.skeleton.flipX = false;
			yield return new WaitForSeconds (0.5f);
		}
	}
}
