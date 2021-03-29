using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask friendMask;
	public LayerMask obstacleMask;

	//[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	public List<Transform> visibleFriends = new List<Transform>();
	public List<Transform> visibleBodies = new List<Transform>();

	void Start() {
		StartCoroutine ("FindTargetsWithDelay", .2f);
		StartCoroutine("FindFriendsWithDelay", .2f);
		viewRadius = GetComponentInParent<EnemyControllerShooting>().viewRadius;
		viewAngle = GetComponentInParent<EnemyControllerShooting>().viewAngle;

	}


	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	IEnumerator FindFriendsWithDelay(float delay)
	{
		while (true) {
			yield return new WaitForSeconds(delay);
			FindVisibleFriends();
		}
	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);
				}
			}
		}
	}

	void FindVisibleFriends()
	{
		visibleFriends.Clear();
		visibleBodies.Clear();
		Collider[] friendsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, friendMask);

		for (int i = 0; i < friendsInViewRadius.Length; i++)
		{
			Transform friend = friendsInViewRadius[i].transform;
			Vector3 dirToFriend = (friend.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToFriend) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, friend.position);

				if (!Physics.Raycast(transform.position, dirToFriend, dstToTarget, obstacleMask))
				{
					visibleFriends.Add(friend);
                    if (friend.tag == "DeadEnemy")
                    {
						visibleBodies.Add(friend);
						visibleFriends.Remove(friend);
					}
				}
			}
		}
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
