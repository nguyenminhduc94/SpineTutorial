using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using Spine.Unity.Examples;


public class SpineboyLearningModel : MonoBehaviour {

	// Use this for initialization
	public SpineLearningBodyState state;
	[Range(-1f,1f)]
	public float currentSpeed;
	public bool facingLeft;

	public event System.Action EventShoot;
	public float shootInterval;
	float lastshootTime;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TryMove(float speed){
		currentSpeed = speed;
		if (speed != 0f) {
			facingLeft = (currentSpeed < 0f);
		}
		if (state != SpineLearningBodyState.Jumping)
			state = (speed == 0) ? SpineLearningBodyState.Idle : SpineLearningBodyState.Running;
	}

	public void TryJump(){
		if (state == SpineLearningBodyState.Jumping)
			return;
		else
			StartCoroutine (StartJump());
	}

	public void TryShoot(){
		float currentTime = Time.time;
		if ((currentTime - lastshootTime) > shootInterval) {
			lastshootTime = currentTime;
			if (EventShoot != null) EventShoot ();
		}
	}
	IEnumerator StartJump(){
		
		state = SpineLearningBodyState.Jumping;
		yield return new WaitForSeconds (1.25f);
		state = SpineLearningBodyState.Idle;
	}
}
public enum SpineLearningBodyState{
	Idle,
	Running,
	Jumping
}
