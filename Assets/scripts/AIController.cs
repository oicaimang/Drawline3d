using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Rigidbody enemyRb;
    private Rigidbody target;
    [SerializeField] float ForceEnemyRb=10;
    private float Seek_Coffiency = 3;
    private float Flee_Coffiency = 3;
    private float Arrival_Coffiency = 3;
    [SerializeField] LayerMask obstaclesMask;
    
    [SerializeField] float max_avoid = 1000;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player").GetComponent<Rigidbody>();
        enemyRb = GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        /*Vector3 velocityEnemyRb = enemyRb.velocity;
        Vector3 desired_velocity = (target.transform.position - enemyRb.transform.position).normalized * Seek_Coffiency;
        Vector3 seek = desired_velocity - velocityEnemyRb;*/

        //enemyRb.AddForce(Arrival(target.position,10));

        enemyRb.AddForce(Seek(target.position) + obstacleAvoidance());
        
    }
    //Seek
    Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 velocityEnemyRb = enemyRb.velocity;
        Vector3 desired_velocity = (target.transform.position - enemyRb.transform.position).normalized * Seek_Coffiency;
        Vector3 seek = (desired_velocity - velocityEnemyRb);
        return seek;
    }
    //Flee
    Vector3 Flee(Vector3 targetPosition)
    {
        Vector3 velocityEnemyRb = enemyRb.velocity;
        Vector3 desired_velocity = (targetPosition - enemyRb.transform.position).normalized * Flee_Coffiency;
        Vector3 flee = -desired_velocity - velocityEnemyRb;
        return flee;
    }
    //Arrival
    Vector3 Arrival(Vector3 targetPosition, float radius) {
        Vector3 velocityEnemyRb = enemyRb.velocity;
        Vector3 desired_velocity = targetPosition - enemyRb.transform.position;
        float distance = desired_velocity.magnitude;
        if (distance < radius)
        {
            desired_velocity = (desired_velocity).normalized * Arrival_Coffiency * (distance / radius);
        }
        else
        {
            desired_velocity = (desired_velocity).normalized * Arrival_Coffiency;
        }
        Vector3 steering = desired_velocity - velocityEnemyRb;
        return steering;
    }
    Vector3 Pursuit(Transform targetTransform)
    {
        float updatesAhead = 3;
        Vector3 futurePosition = targetTransform.position + new Vector3(targetTransform.GetComponent<Rigidbody>().velocity.x, targetTransform.GetComponent<Rigidbody>().velocity.y, 0) * updatesAhead;
        return Seek(futurePosition);
    }
    //evade
   Vector3 Evade(Transform targetTransform, Vector3 targetPosition)
    {
        Vector3 desired_velocity = targetPosition - enemyRb.transform.position;
        float distance = desired_velocity.magnitude;
        float updatesAhead = distance / targetTransform.GetComponent<Rigidbody>().velocity.magnitude;
        Vector3 futurePosition = targetPosition + new Vector3(targetTransform.GetComponent<Rigidbody>().velocity.x, targetTransform.GetComponent<Rigidbody>().velocity.y, 0) * updatesAhead;
        return Flee(futurePosition);
    }
    Vector3 obstacleAvoidance()
    {
        Vector3 velocity = enemyRb.velocity;
        Vector3 steering = Vector3.zero;

        RaycastHit outInfor;
        int mask = 1 << LayerMask.NameToLayer("obstacles");
        bool hit = Physics.Raycast(transform.position, new Vector3(velocity.x, velocity.y, velocity.z), out outInfor, 5, mask);
        if (hit)
        {
            if (outInfor.collider.gameObject.layer == LayerMask.NameToLayer("obstacles"))
            {

                Vector3 diff = -(enemyRb.transform.position - outInfor.collider.gameObject.transform.position);
                Vector3 diffrotate = Quaternion.Euler(0, 90, 0) * diff;
                
                steering += new Vector3(diffrotate.x, diffrotate.y, diffrotate.z).normalized * max_avoid;
                Debug.Log("collider");
            }
        }
        
        Debug.DrawRay(transform.position, new Vector3(velocity.x, velocity.y,velocity.z), Color.red);
        return steering;
    }
}

