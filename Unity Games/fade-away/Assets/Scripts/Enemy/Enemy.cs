using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] internal bool hasPath;
    [SerializeField] List<GameObject> waypoints;
    [SerializeField] internal float speed;
    [SerializeField] internal bool circles;
    internal bool reverse;
    Transform currentwaypoint;
    int waypointindex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Count > 0)
            currentwaypoint = waypoints[waypointindex].transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (hasPath)
        {
            if (waypoints != null && waypoints.Count > 0)
            {
                if (waypointindex > waypoints.Count && !circles)
                {
                    reverse = true;
                }
                else if (waypointindex > waypoints.Count && circles)
                {
                    waypointindex = 0;
                }
                if (waypointindex < 0 && reverse)
                {
                    reverse = false;
                }
                moveToWaypoint();
            }
        }
    }

    void moveToWaypoint()
    {
        Vector3 distance = currentwaypoint.localPosition - transform.localPosition;
        Vector3 direction = distance.normalized;

        if (reverse)
        {
            transform.rotation = Quaternion.Slerp(Quaternion.Inverse(transform.rotation), Quaternion.Inverse(currentwaypoint.rotation), 1f);
        }
        else if (!reverse)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, currentwaypoint.rotation, 1f);
        }

        transform.localPosition += direction * speed * Time.deltaTime;
        if (direction.magnitude >= distance.magnitude || direction.magnitude == 0f)
        {
            if (!reverse)
            {
                transform.localPosition = currentwaypoint.localPosition;
                waypointindex++;
                if (waypointindex < waypoints.Count)
                    currentwaypoint = waypoints[waypointindex].transform;
            }
            else if (reverse)
            {
                transform.localPosition = currentwaypoint.localPosition;
                waypointindex--;
                if (waypointindex > 0)
                    currentwaypoint = waypoints[waypointindex].transform;
            }
        }
    }
}
