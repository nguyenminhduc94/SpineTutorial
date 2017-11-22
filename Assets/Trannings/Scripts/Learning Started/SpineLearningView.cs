using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;
using Spine.Unity;

public class SpineLearningView : MonoBehaviour {

	public SpineboyLearningModel model;
	public SkeletonAnimation skeletonAnimation;
	SpineLearningBodyState previousViewState;
	[SpineAnimation] public string run, jump, idle, turn;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(previousViewState != SpineLearningBodyState.Jumping)
			Turn (model.facingLeft);
		
		var currentModelState = model.state;
		if (previousViewState != currentModelState)
			PlayNewStableAnimation ();
		previousViewState = currentModelState;
	}

	void PlayNewStableAnimation(){
		var newModelState = model.state;
		Debug.Log (model.state);
		string nextAnimation;
		if (newModelState == SpineLearningBodyState.Running)
			nextAnimation = run;
		else
			nextAnimation = idle;
		skeletonAnimation.AnimationState.SetAnimation (0, nextAnimation, true);

	}
	void Turn(bool turn){
		skeletonAnimation.skeleton.flipX = turn;
	}
}
