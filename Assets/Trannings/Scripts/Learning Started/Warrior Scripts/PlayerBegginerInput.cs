using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting.Messaging;
using Spine.Unity;
using System;
using UnityEditorInternal;

namespace Unity.Learning{
	
	public class PlayerBegginerInput : MonoBehaviour {

		[Header("Controls")]
		private string XAxis = "Horizontal";
		private string YAxis = "Vertical";

		[Header("Moving")]
		public float walkSpeed = 1.5f;
		public float runSpeed = 7f;
		public float gravityScale = 6.6f;

		[Header("Jumping")]
		public float jumpSpeed = 25f;

		[Header("Graphics")]
		private SkeletonAnimation skeletonAnimation;

		private Vector2 input;
		private Vector3 velocity = default(Vector3);
		CharacterController controller;


		void Awake(){
			controller = GetComponent<CharacterController> ();
		}
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			playerInput ();
		}

		void playerInput(){
			
			input.x = Input.GetAxisRaw (XAxis);
			input.y = Input.GetAxisRaw (YAxis);
			float dt = Time.deltaTime;
			Debug.Log (controller.isGrounded);
			if (controller.isGrounded) {
				
			} else {
				if(input.x !=0){
					velocity.x = Mathf.Abs (input.x) > 0.6f ? runSpeed : walkSpeed;

				}
			}
			var gravityDeltaVelocity = Physics.gravity * gravityScale * dt;
			//velocity += gravityDeltaVelocity;
			controller.Move (velocity*dt);
		}
	}
}
