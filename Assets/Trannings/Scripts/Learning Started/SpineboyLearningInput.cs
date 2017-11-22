using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineboyLearningInput : MonoBehaviour {

	public SpineboyLearningModel model;
	public string horizontal = "Horizontal";
	public string jump = "Jump";
	public float currentSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currentSpeed = Input.GetAxisRaw (horizontal);
		model.TryMove (currentSpeed);
	}


}
