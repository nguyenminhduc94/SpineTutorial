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
		public string XAxis = "Horizontal";
		public string YAxis = "Vertical";
		public string JumpButton = "Jump";

		[Header("Moving")]
		public float walkSpeed = 1.5f;
		public float runSpeed = 7f;
		public float gravityScale = 6.6f;

		[Header("Jumping")]
		public float jumpSpeed = 25f;

		[Header("Graphics")]
		public SkeletonAnimation skeletonAnimation;

		[Header("Animation")]
		[SpineAnimation(dataField: "skeletonAnimation")]
		public string walkName = "Walk";
		[SpineAnimation(dataField: "skeletonAnimation")]
		public string runName = "Run";
		[SpineAnimation(dataField: "skeletonAnimation")]
		public string idleName = "Idle";
		[SpineAnimation(dataField: "skeletonAnimation")]
		public string jumpName = "Jump";
		[SpineAnimation(dataField: "skeletonAnimation")]
		public string fallName = "Fall";
		[SpineAnimation(dataField: "skeletonAnimation")]
		public string crouchName = "Crouch";

		[Header("Audio")]
		public AudioSource jumpSource,walkSource,hardfallSource;

		[SpineEvent]
		public string footstepEventName = "Footstep";

		private Vector2 input;
		private Vector3 velocity = default(Vector3);
		CharacterController controller;
		float forceCrouchEndTime;
		bool wasGrounded;

		void Awake(){
			controller = GetComponent<CharacterController> ();
		}
		// Use this for initialization
		void Start () {
			skeletonAnimation.AnimationState.Event += HandleEvent;
		}

		void HandleEvent (Spine.TrackEntry trackEntry, Spine.Event e) {
			if (e.Data.Name == footstepEventName) {
				walkSource.Stop();
				//footstepAudioSource.pitch = GetRandomPitch(0.2f);
				walkSource.Play();
			}
		}
		
		// Update is called once per frame
		void Update () {
			playerInput ();
		}

		void playerInput(){
			
			input.x = Input.GetAxisRaw (XAxis);
			input.y = Input.GetAxisRaw (YAxis);
			float dt = Time.deltaTime;

			bool crouching = (controller.isGrounded && input.y < -0.5f);
			if (!crouching) {
				if(Input.GetButton(JumpButton) && controller.isGrounded){
					velocity.y = jumpSpeed;
					wasGrounded = true;
					jumpSource.Play ();
				}
			}

			if (controller.isGrounded) {
				if (input.x != 0) {
					velocity.x = Mathf.Abs (input.x) > 0.6f ? runSpeed : walkSpeed;
					velocity.x *= input.x;
				} else {
					velocity.x = 0;
				}
			}
			var gravityDeltaVelocity = Physics.gravity * gravityScale * dt;
			velocity += gravityDeltaVelocity;
			controller.Move (velocity*dt);

			if (controller.isGrounded) {
				if (crouching) {
					skeletonAnimation.AnimationName = crouchName;
				} else {
					if (input.x != 0)
						skeletonAnimation.AnimationName = Mathf.Abs (input.x) > 0.6f ? runName : walkName;
					else{
						skeletonAnimation.AnimationName = wasGrounded ? crouchName : idleName;
					}
				}
			} else {
				skeletonAnimation.AnimationName = velocity.y > 0 ? jumpName : fallName;
			}

			if(wasGrounded && controller.isGrounded){
				hardfallSource.Play ();
				wasGrounded = false;
			}

			if(input.x != 0){
				skeletonAnimation.skeleton.flipX = input.x < 0;

			}
		}
	}
}
