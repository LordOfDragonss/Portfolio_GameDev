using UnityEngine;

namespace VolumetricAudio
{
	/// <summary>This is the base class for all volumetric shapes (e.g. <b>VA_Box</b>).</summary>
	public abstract class VA_VolumetricShape : VA_Shape
	{
		/// <summary>If you set this, then sound will only emit from the thin shell around the shape, else it will emit from inside too.</summary>
		public bool IsHollow { set { isHollow = value; } get { return isHollow; } } [SerializeField] protected bool isHollow;

		// Does this shape have an inner point? (the closest point between this shape's volume and the audio listener)
		public bool InnerPointSet;

		// The position of the inner point
		public Vector3 InnerPoint;

		// The distance between the inner point and the audio listener
		public float InnerPointDistance;

		// If the inner point is inside the volume of the shape
		public bool InnerPointInside;

		public override bool FinalPointSet
		{
			get
			{
				return isHollow == true ? OuterPointSet : InnerPointSet;
			}
		}

		public override Vector3 FinalPoint
		{
			get
			{
				return isHollow == true ? OuterPoint : InnerPoint;
			}
		}

		public override float FinalPointDistance
		{
			get
			{
				return isHollow == true ? OuterPointDistance : InnerPointDistance;
			}
		}

		// This will test if a point is inside this shape (ignores IsHollow)
		public bool PointInShape(Vector3 worldPoint)
		{
			var localPoint = transform.InverseTransformPoint(worldPoint);

			return LocalPointInShape(localPoint);
		}

		public abstract bool LocalPointInShape(Vector3 localPoint);

		public void SetInnerPoint(Vector3 newInnerPoint, bool inside)
		{
			// Make sure the listener exists
			var listenerPosition = default(Vector3);

			if (VA_Common.GetListenerPosition(ref listenerPosition) == true)
			{
				InnerPointSet      = true;
				InnerPoint         = newInnerPoint;
				InnerPointDistance = Vector3.Distance(listenerPosition, newInnerPoint);
				InnerPointInside   = inside;
			}
		}

		public void SetInnerOuterPoint(Vector3 newInnerOuterPoint, bool inside)
		{
			SetInnerPoint(newInnerOuterPoint, inside);
			SetOuterPoint(newInnerOuterPoint);
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();

			InnerPointSet = false;
		}
	}
}