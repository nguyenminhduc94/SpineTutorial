using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SpineboyLearningModel : MonoBehaviour {

	// Use this for initialization
	public SpineLearningBodyState state;
	[Range(-1f,1f)]
	public float currentSpeed;
	public bool facingLeft;
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
		if(state != SpineLearningBodyState.Jumping)
			state = (speed == 0) ? SpineLearningBodyState.Idle : SpineLearningBodyState.Running;
	}
		
}
public enum SpineLearningBodyState{
	Idle,
	Running,
	Jumping
}
