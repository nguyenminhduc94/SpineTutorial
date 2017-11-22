using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class SpineNew : MonoBehaviour {

	// Use this for initialization

	[SpineAnimation]
	public string walk = "walk";

	[SpineAnimation]
	public string gungrab = "gungrab";

	[SpineAnimation]
	public string gunkeep = "gunkeep";

	[SpineEvent]
	public string footstepEvent = "footstep";

	[Range(-1f,1f)]
	public float currentSpeed;

	public bool facingLeft;

	public AudioSource audiosource;

	private SkeletonAnimation skeletonAnimation;
	private string horizontal = "Horizontal";

	void Start () {
		skeletonAnimation = GetComponent<SkeletonAnimation> ();
		StartCoroutine (GunGrabRoutine());
	}
	
	// Update is called once per frame
	void Update () {
		currentSpeed = Input.GetAxisRaw (horizontal);
		if (currentSpeed != 0)
			TryMove (currentSpeed);
	}

	IEnumerator GunGrabRoutine(){
		skeletonAnimation.AnimationState.SetAnimation(0, walk, true);

		// Repeatedly play the gungrab and gunkeep animation on track 1.
		while (true) {
			yield return new WaitForSeconds(Random.Range(0.5f,3f));
			skeletonAnimation.AnimationState.SetAnimation(1, gungrab, false);

			yield return new WaitForSeconds(Random.Range(0.5f, 3f));
			skeletonAnimation.AnimationState.SetAnimation(1, gunkeep, false);
		}
	}

	void TryMove(float speed){
		facingLeft = (speed == -1) ? true : false;
		var trackEntry = skeletonAnimation.state.GetCurrent (0);
	}
}
