using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class FieldOfView : MonoBehaviour
{
	[SerializeField]
	private float fieldOfViewAngle, fieldOfViewRange;

	public float FieldOfViewAngle
	{
		get
		{
			return fieldOfViewAngle;
		}

		set { fieldOfViewAngle = value; }
	}

	public float FieldOfViewRange
	{
		get
		{
			return fieldOfViewRange;
		}
		set { fieldOfViewRange = value; }

	}
	public bool Active { get; set; }

	public Transform Parent
	{
		get
		{
			return parent;
		}

		set
		{
			parent = value;
		}
	}

	[SerializeField]
	private Transform parent;

	/// <summary>
	///  check if object can see in specific angle
	/// </summary>
	/// <param name="targetHeight">specifi height of target</param>
	/// <param name="target">other object</param>
	/// <param name="targetInRange">compare distance from other object</param>
	/// <returns></returns>
	public bool CanSeeTarget(Transform target, float targetHeight)
	{
		Vector3 targetDir = target.position - transform.position;
		float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));
		/////////////////////////////////////////////////////////////////////////////////
		float direct = (float)Math.Sqrt((Math.Pow(fieldOfViewRange, 2) - Math.Pow(transform.position.y, 2)));
		float dif = transform.position.y / targetHeight;
		float blindZone = direct / dif;
		/////////////////////////////////////////////////////////////////////////////////
		bool inheightOfAngle = Vector3.Distance(Parent.position, target.position) > blindZone;
		bool inRangeOfAngle = Vector3.Distance(transform.position, target.position) < FieldOfViewRange;
		if (angleToPlayer >= -fieldOfViewAngle && angleToPlayer <= fieldOfViewAngle)
		{
			if (inRangeOfAngle && inheightOfAngle)
			{
				return true;
			}
		}
		return false;


	}
	public void Draw()
	{
		Gizmos.color = Color.magenta;
		float totalFOV = FieldOfViewAngle;
		float rayRange = FieldOfViewRange;
		Quaternion leftRayRotation = Quaternion.AngleAxis(-totalFOV, Vector3.up);
		Quaternion rightRayRotation = Quaternion.AngleAxis(totalFOV, Vector3.up);
		Vector3 leftRayDirection = leftRayRotation * transform.forward;
		Vector3 rightRayDirection = rightRayRotation * transform.forward;
		Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
		Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
	}

}
