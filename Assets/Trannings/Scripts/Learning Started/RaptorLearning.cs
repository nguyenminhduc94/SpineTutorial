using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class RaptorLearning : MonoBehaviour {

	// Use this for initialization
	[SpineAnimation]
	public string walk;

	[SpineAnimation]
	public string gungrab;

	[SpineAnimation]
	public string gunkeep;

	[SpineEvent]
	public string footstep;

	public AudioSource audioSource;
	private SkeletonAnimation skeletonAnimation;

	void Start () {
		skeletonAnimation = GetComponent<SkeletonAnimation> ();
		skeletonAnimation.AnimationState.Event += HandleEvent;
		StartCoroutine (GunGrabRoutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HandleEvent(Spine.TrackEntry trackEntry, Spine.Event e){
		if(e.Data.Name == footstep){
			audioSource.pitch = 0.5f + Random.Range (-0.2f,0.2f);
			audioSource.Play ();
		}
	}

	IEnumerator GunGrabRoutine(){
		skeletonAnimation.AnimationState.SetAnimation (0,walk,true);
		while(true){
			yield return new WaitForSeconds (1f);
			skeletonAnimation.AnimationState.SetAnimation (1, gungrab, false);
			yield return new WaitForSeconds (1f);
			skeletonAnimation.AnimationState.SetAnimation (1, gunkeep, false);
		}
	}
}
