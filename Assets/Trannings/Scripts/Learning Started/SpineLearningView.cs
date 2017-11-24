using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;
using Spine.Unity;

public class SpineLearningView : MonoBehaviour {

	[Header("Components")]
	public SpineboyLearningModel model;
	public SkeletonAnimation skeletonAnimation;
	[SpineAnimation] public string run, jump, idle, turn,shoot;

	[Header("Audio")]
	public float footstepPitchOffset;
	public float gunshootPictchOffset;
	public AudioSource jumpSource, gunSource;

	[Header("Effects")]
	public ParticleSystem gunParticles;

	SpineLearningBodyState previousViewState;

	// Use this for initialization
	void Start () {
		skeletonAnimation = GetComponent<SkeletonAnimation> ();
		model.EventShoot += PlayShoot;
	}
	
	// Update is called once per frame
	void Update () {
		if (skeletonAnimation == null)return;
		if (model == null)return;

		if(previousViewState != SpineLearningBodyState.Jumping)
			Turn (model.facingLeft);
		
		var currentModelState = model.state;
		if (previousViewState != currentModelState)
			PlayNewStableAnimation ();
		previousViewState = currentModelState;
	}

	void PlayNewStableAnimation(){
		var newModelState = model.state;
		string nextAnimation;

		if(model.state == SpineLearningBodyState.Jumping){
			nextAnimation = jump;
			jumpSource.Play ();
		}else{
			if (newModelState == SpineLearningBodyState.Running)
				nextAnimation = run;
			else
				nextAnimation = idle;
		}
		skeletonAnimation.AnimationState.SetAnimation (0, nextAnimation, true);
	}

	public void PlayShoot () {
		// Play the shoot animation on track 1.
		var track = skeletonAnimation.AnimationState.SetAnimation(1, shoot, false);
		track.AttachmentThreshold = 1f;
		track.MixDuration = 0f;
		var empty = skeletonAnimation.state.AddEmptyAnimation(1, 0.5f, 0.1f);
		empty.AttachmentThreshold = 1f;
		gunSource.pitch = GetRandomPitch(gunshootPictchOffset);
		gunSource.Play();
		//gunParticles.randomSeed = (uint)Random.Range(0, 100);
		gunSource.Play();
		gunParticles.Play ();
	}
	public float GetRandomPitch (float maxPitchOffset) {
		return 1f + Random.Range(-maxPitchOffset, maxPitchOffset);
	}
	void Turn(bool turn){
		skeletonAnimation.skeleton.flipX = turn;
	}
}
