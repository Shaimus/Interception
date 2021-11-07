using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Rigidbody botRigidbody;

    [SerializeField]
    private int movementSpeed = 15;

    [SerializeField]
    private PlayerController playerController;

    private void FixedUpdate()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {

        Vector3 targetPos = CalculateInterceptionPoint(playerTransform.position, playerController.velocity, playerController.movementSpeed, transform.position, movementSpeed);
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 movement = direction * movementSpeed * Time.deltaTime;

        botRigidbody.MovePosition(transform.position + movement);
    }

    public Vector3 CalculateInterceptionPoint(Vector3 targetPos, Vector3 targetVel, float targetSpeed, Vector3 chaserPos, float chaserSpeed)
    {
        float targetVelScal = targetVel.magnitude;

        Vector3 initialDistance = targetPos - chaserPos;

        if (chaserSpeed < targetSpeed) // fixing the bug when the chaser moves the wrong direction if its speed is lower than the speed of the target and the target moves towards the chaser
            chaserSpeed = targetSpeed; // so in this case it will try to catch the target, but will never be able to do so, if the target doesn't just run into it

        // quadratic equation 

        float a = Mathf.Pow(targetVelScal, 2) - Mathf.Pow(chaserSpeed, 2);  

        float b = 2 * Vector3.Dot(initialDistance, targetVel);

        float c = Vector3.Dot(initialDistance, initialDistance);
      
        float t = (-b - Mathf.Sqrt(Mathf.Pow(b, 2) - (4 * a * c))) / (2 * a); // if we put + after -b we get a negative t which is invalid
        
        return ((t * targetVel * targetSpeed) + targetPos); //calculating the interception point by multiplying time, target velocity vector, target speed and adding it to the target position
    }
}
