using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SpineboyLearningController : MonoBehaviour {

	public SkeletonAnimation skeletonAnimation;

	[SpineBone(dataField:"skeletonAnimation")]
	public string boneName;

	public new Camera camera;
	Bone bone;

	void OnValidate () {
		if (skeletonAnimation == null) skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

	void Start () {
		bone = skeletonAnimation.Skeleton.FindBone(boneName);
	}

	void Update () {
		var mousePosition = Input.mousePosition;
		var worldMousePosition = camera.ScreenToWorldPoint(mousePosition);
		var skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);

		if (skeletonAnimation.Skeleton.FlipX) skeletonSpacePoint.x *= -1;
		bone.SetPosition(skeletonSpacePoint);
	}
}