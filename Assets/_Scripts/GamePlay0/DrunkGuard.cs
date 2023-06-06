using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class DrunkGuard : NetworkBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    private Transform currentWaypoint;
    private int currentIndex;
    [SerializeField]
    private float rotationSpeed;
    private NetworkRigidbody rb;

    public override void Spawned()
    {
        rb = GetComponent<NetworkRigidbody>();
        SetCurrentWaypoint();

    }

    public override void FixedUpdateNetwork()
    {
        MoveToWaypoint();
        // transform.Rotate(Vector3.down * rotationSpeed, Space.World);
    }

    private void MoveToWaypoint()
    {

        Vector3 direction = currentWaypoint.position - transform.position;
        rb.Rigidbody.AddForce(direction.normalized * 3);

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 3f)
        {
            SetCurrentWaypoint();
            rb.Rigidbody.velocity = Vector3.zero;
        }
    }

    private void SetCurrentWaypoint()
    {
        currentIndex = Random.Range(0, waypoints.Length);
        currentWaypoint = waypoints[currentIndex];
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.TryGetComponent<IStrikeable>(out IStrikeable hitObject))
        {
            Vector3 direction = (transform.position - other.gameObject.transform.position).normalized;
            hitObject.Knock(direction, 5);
            Debug.Log("hitobject knock");
            // StartCoroutine(GetBack());
        }
    }
    // private IEnumerator GetBack()
    // {
    //     rb.Rigidbody.AddForce(Vector3.back * 10, ForceMode.Impulse);
    //     yield return null;
    // }

}
