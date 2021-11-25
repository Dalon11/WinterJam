

using UnityEngine;
using System.Collections;
using Spine.Unity;

namespace Spine.Unity.Examples {
	public class Raptor : MonoBehaviour {

		#region Inspector
		public AnimationReferenceAsset walk;
		public AnimationReferenceAsset gungrab;
		public AnimationReferenceAsset gunkeep;
		#endregion

		SkeletonAnimation skeletonAnimation;

		void Start () {
			skeletonAnimation = GetComponent<SkeletonAnimation>();
			StartCoroutine(GunGrabRoutine());
		}

		IEnumerator GunGrabRoutine () {
			// Play the walk animation on track 0.
			skeletonAnimation.AnimationState.SetAnimation(0, walk, true);

			// Repeatedly play the gungrab and gunkeep animation on track 1.
			while (true) {
				yield return new WaitForSeconds(Random.Range(0.5f, 3f));
				skeletonAnimation.AnimationState.SetAnimation(1, gungrab, false);

				yield return new WaitForSeconds(Random.Range(0.5f, 3f));
				skeletonAnimation.AnimationState.SetAnimation(1, gunkeep, false);
			}

		}

	}
}
