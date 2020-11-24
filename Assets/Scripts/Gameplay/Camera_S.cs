using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_S : MonoBehaviour
{
	/*[Header("Tolerance")]
	[SerializeField]
	private float xTolerance = 1f;
	[SerializeField]
	private float yTolerance = 0.5f;

	[Header("Velocity")]
	[SerializeField]
	private float speed = 2f;

	private float targetX;
	private float targetY;

	[Header("Limits")]
	[SerializeField]
	private float maxDistanceX = 10;
	[SerializeField]
	private float minDistanceX = -10;

	[SerializeField]
	private float maxDistanceY = 5;
	[SerializeField]
	private float minDistanceY = -5;

	[SerializeField]
	private float valueY = -5;

	private Transform[] playersTransform;

    private Vector3 midPoint;

    private bool runCamera = false;

    private void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");

        playersTransform = new Transform[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            playersTransform[i] = players[i].transform;
        }

        runCamera = true;

    }

    private void Update()
    {
        if (runCamera)
        {
            midPoint = (playersTransform[0].position + playersTransform[1].position) / 2;

			if (CheckXTolerance())
			{
				targetX = midPoint.x;
			}

			if (CheckYTolerance())
			{
				targetY = midPoint.y + valueY;
				Debug.Log(targetY);
			}


			if (targetX > maxDistanceX)
			{
				targetX = maxDistanceX;
			}

			if (targetX < minDistanceX)
			{
				targetX = minDistanceX;
			}

			if (targetY > maxDistanceY)
			{
				targetY = maxDistanceY;
			}

			if (targetY < minDistanceY)
			{
				targetY = minDistanceY;
			}			

            Vector3 finalPosition = new Vector3(targetX, targetY, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * speed);
			
			// Zoom
            //Debug.Log(Vector3.Distance(playersTransform[0].position, playersTransform[1].position));
			float pointDistance = Vector3.Distance(playersTransform[0].position, playersTransform[1].position);
			float x = pointDistance * 23f / 65;
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Mathf.Clamp(x, 18f, 23f), Time.deltaTime * 10);           
        }

    }


	private bool CheckXTolerance()
	{
		return Mathf.Abs(
			transform.position.x - midPoint.x
						 ) > xTolerance;
	}

	private bool CheckYTolerance()
	{
		return Mathf.Abs(
			transform.position.y - midPoint.y
		) > yTolerance;
	}*/

	[Header("Limits")]
	[SerializeField]
	private float maxDistanceX = 10;
	[SerializeField]
	private float minDistanceX = -10;

	[SerializeField]
	private float maxDistanceY = 5;
	[SerializeField]
	private float minDistanceY = -5;

	private float targetX;
	private float targetY;

	[SerializeField]
	private List<Transform> targets;

	[SerializeField]
	private Vector3 offset;

	private Vector3 velocity;

	[SerializeField]
	private float minZoom = 40f;

	[SerializeField]
	private float maxZoom = 10f;

	[SerializeField]
	private float zoomLimiterX = 80f;

	[SerializeField]
	private float zoomLimiterY = 30f;

	private float zoomLimiter = 80f;

	private void LateUpdate() 
	{
		if (targets.Count == 0)
			return;


		Move();
		Zoom();
	}

	private void Zoom()
	{

		float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);

		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newZoom, Time.deltaTime);	
	}

    private void Move()
    {
		Vector3 centerPoint = GetCenterPoint();

		targetX = centerPoint.x;
		targetY = centerPoint.y;

		if (targetX > maxDistanceX)
		{
			targetX = maxDistanceX;
		}

		if (targetX < minDistanceX)
		{
			targetX = minDistanceX;
		}

		if (targetY > maxDistanceY)
		{
			targetY = maxDistanceY;
		}

		if (targetY < minDistanceY)
		{
			targetY = minDistanceY;
		}

		Vector3 newPosition = new Vector3(targetX, targetY, transform.position.z) + offset;
		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, 0.5f);
	}

	private float GetGreatestDistance()
	{
		var bounds = new Bounds(targets[0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++)
		{
			bounds.Encapsulate(targets[i].position);
		}

		float greatestDistance = bounds.size.y;
		zoomLimiter = zoomLimiterY;

		if (bounds.size.x > bounds.size.y)
        {
			greatestDistance = bounds.size.x;
			zoomLimiter = zoomLimiterX;

		}
		return greatestDistance;
	}



	private Vector3 GetCenterPoint()
	{
        if (targets.Count == 1)
        {
			return targets[0].position;
        }

		var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
			bounds.Encapsulate(targets[i].position);
        }

		return bounds.center;
	}
}

