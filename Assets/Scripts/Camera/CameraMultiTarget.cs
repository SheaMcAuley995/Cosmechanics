using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMultiTarget : MonoBehaviour
{
	public static CameraMultiTarget instance = null;

	public float Pitch;
	public float Yaw;
	public float Roll;
	public float PaddingLeft;
	public float PaddingRight;
	public float PaddingUp;
	public float PaddingDown;
	public float MoveSmoothTime = 0.19f;

	private Camera _camera;
	private GameObject[] _targets = new GameObject[0];
	private DebugProjection _debugProjection;

	enum DebugProjection { DISABLE, IDENTITY, ROTATED }
	enum ProjectionEdgeHits { TOP_BOTTOM, LEFT_RIGHT }

    private void Awake()
	{
		_camera = gameObject.GetComponent<Camera>();
		_debugProjection = DebugProjection.ROTATED;

		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

    private void Start()
    {
		if (SceneManager.GetActiveScene().name == "Ship_Name_1" ||
			SceneManager.GetActiveScene().name == "Ship_Name_2" ||
			SceneManager.GetActiveScene().name == "Ship_Name_3" ||
			SceneManager.GetActiveScene().name == "Ship_Name_4" ||
			SceneManager.GetActiveScene().name == "Ship_Name_5" ||
			SceneManager.GetActiveScene().name == "Ship_Name_6" ||
			SceneManager.GetActiveScene().name == "Ship_Name_7")
		{
			PlayerController[] playerControllers = FindObjectsOfType<PlayerController>();
			GameObject[] gameObjects = new GameObject[4];
			for (int i = 0; i < playerControllers.Length; i++)
            {
				gameObjects[i] = playerControllers[i].gameObject;
            }

			SetTargets(gameObjects);
		}
    }

    private void LateUpdate() {

		if (_targets.Length == 0)
			return;
		
		var targetPositionAndRotation = TargetPositionAndRotation(_targets);

		Vector3 velocity = Vector3.zero;
		transform.position = Vector3.SmoothDamp(transform.position, targetPositionAndRotation.Position, ref velocity, MoveSmoothTime);
		transform.rotation = targetPositionAndRotation.Rotation;
	}
	
	PositionAndRotation TargetPositionAndRotation(GameObject[] targets)
	{
		float halfVerticalFovRad = (_camera.fieldOfView * Mathf.Deg2Rad) / 2f;
		float halfHorizontalFovRad = Mathf.Atan(Mathf.Tan(halfVerticalFovRad) * _camera.aspect);

		var rotation = Quaternion.Euler(Pitch, Yaw, Roll);
		var inverseRotation = Quaternion.Inverse(rotation);

		var targetsRotatedToCameraIdentity = targets.Select(target => inverseRotation * target.transform.position).ToArray();

		float furthestPointDistanceFromCamera = targetsRotatedToCameraIdentity.Max(target => target.z);
		float projectionPlaneZ = furthestPointDistanceFromCamera + 3f;

		ProjectionHits viewProjectionLeftAndRightEdgeHits = 
			ViewProjectionEdgeHits(targetsRotatedToCameraIdentity, ProjectionEdgeHits.LEFT_RIGHT, projectionPlaneZ, halfHorizontalFovRad).AddPadding(PaddingRight, PaddingLeft);
		ProjectionHits viewProjectionTopAndBottomEdgeHits = 
			ViewProjectionEdgeHits(targetsRotatedToCameraIdentity, ProjectionEdgeHits.TOP_BOTTOM, projectionPlaneZ, halfVerticalFovRad).AddPadding(PaddingUp, PaddingDown);
		
		var requiredCameraPerpedicularDistanceFromProjectionPlane =
			Mathf.Max(
				RequiredCameraPerpedicularDistanceFromProjectionPlane(viewProjectionTopAndBottomEdgeHits, halfVerticalFovRad),
				RequiredCameraPerpedicularDistanceFromProjectionPlane(viewProjectionLeftAndRightEdgeHits, halfHorizontalFovRad)
		);

		Vector3 cameraPositionIdentity = new Vector3(
			(viewProjectionLeftAndRightEdgeHits.Max + viewProjectionLeftAndRightEdgeHits.Min) / 2f,
			(viewProjectionTopAndBottomEdgeHits.Max + viewProjectionTopAndBottomEdgeHits.Min) / 2f,
			projectionPlaneZ - requiredCameraPerpedicularDistanceFromProjectionPlane);


		return new PositionAndRotation(rotation * cameraPositionIdentity, rotation);
	}

	private static float RequiredCameraPerpedicularDistanceFromProjectionPlane(ProjectionHits viewProjectionEdgeHits, float halfFovRad)
	{
		float distanceBetweenEdgeProjectionHits = viewProjectionEdgeHits.Max - viewProjectionEdgeHits.Min;
		return (distanceBetweenEdgeProjectionHits / 2f) / Mathf.Tan(halfFovRad);
	}

	private ProjectionHits ViewProjectionEdgeHits(IEnumerable<Vector3> targetsRotatedToCameraIdentity, ProjectionEdgeHits alongAxis, float projectionPlaneZ, float halfFovRad)
	{
		float[] projectionHits = targetsRotatedToCameraIdentity
			.SelectMany(target => TargetProjectionHits(target, alongAxis, projectionPlaneZ, halfFovRad))
			.ToArray();
		return new ProjectionHits(projectionHits.Max(), projectionHits.Min());
	}
	
	private float[] TargetProjectionHits(Vector3 target, ProjectionEdgeHits alongAxis, float projectionPlaneDistance, float halfFovRad)
	{
		float distanceFromProjectionPlane = projectionPlaneDistance - target.z;
		float projectionHalfSpan = Mathf.Tan(halfFovRad) * distanceFromProjectionPlane;

		if (alongAxis == ProjectionEdgeHits.LEFT_RIGHT)
		{
			return new[] {target.x + projectionHalfSpan, target.x - projectionHalfSpan};
		}
		else
		{
			return new[] {target.y + projectionHalfSpan, target.y - projectionHalfSpan};
		}
	
	}

    public void SetTargets(GameObject[] targets)
    {
        _targets = targets;
    }


}
