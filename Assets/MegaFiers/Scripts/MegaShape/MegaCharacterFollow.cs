﻿using UnityEngine;

namespace MegaFiers
{
	[ExecuteInEditMode]
	public class MegaCharacterFollow : MonoBehaviour
	{
		public MegaShape	path;
		public bool			rot		= false;
		public Vector3		rotate	= Vector3.zero;
		Rigidbody			rbody;

		void Start()
		{
			float alpha = 0.0f;
			Vector3 tangent = Vector3.zero;
			int kn = 0;

			rbody = GetComponent<Rigidbody>();
			Vector3 p = transform.position;
			Vector3 np = path.FindNearestPointWorld(p, 5, ref kn, ref tangent, ref alpha);
			rbody.MovePosition(np);
		}

		void LateUpdate()
		{
			if ( path )
			{
				Vector3 p = transform.position;

				float alpha = 0.0f;
				Vector3 tangent = Vector3.zero;
				int kn = 0;

				Vector3 np = path.FindNearestPointWorld(p, 5, ref kn, ref tangent, ref alpha);

				if ( rot )
				{
					Vector3 np1 = path.splines[0].InterpCurve3D(alpha + 0.001f, true, ref kn);

					Quaternion er = Quaternion.Euler(rotate);
					Quaternion r = Quaternion.LookRotation(np1 - np);
					transform.rotation = path.transform.rotation * r * er;
				}

				np.y = p.y;
				transform.position = np;
			}
		}
	}
}