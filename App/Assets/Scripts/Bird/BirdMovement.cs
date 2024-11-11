using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour
{
    public enum MovementType { Stand, Walk, Fly }
    public MovementType movementType;

    public float movementSpeed = 2f;          // Speed at which the bird moves
    public float flyHeight = 5f;              // Height at which the bird flies
    public float playerRadius = 3f;           // Radius around the player to stay within
    public float behaviorChangeInterval = 5f; // Interval in seconds to change behavior
    public int minHops = 1;                   // Minimum number of hops when walking
    public int maxHops = 5;                   // Maximum number of hops when walking
    public float groundReturnSpeed = 2f;      // Speed at which the bird returns to ground level

    private Vector3 targetPosition;
    private Vector3 playerPosition;
    private bool isMoving = false;

    private Animator animator;                // Animator component reference
    private int hopCount;                     // Number of hops for the current walk action

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the bird model
        StartCoroutine(UpdatePlayerPosition());
        StartCoroutine(ChangeBehavior());
    }

    void Update()
    {
        if (isMoving && movementType != MovementType.Stand)
        {
            MoveTowardsTarget();
        }

        // If the bird should be on the ground but isn't, bring it down smoothly
        if ((movementType == MovementType.Walk || movementType == MovementType.Stand) && transform.position.y > 0.1f)
        {
            ReturnToGround();
        }
    }

    IEnumerator UpdatePlayerPosition()
    {
        while (true)
        {
            // Get real-time player position from GPS
            playerPosition = new Vector3(
                (float)NativeGPSPlugin.GetLatitude(),
                0,
                (float)NativeGPSPlugin.GetLongitude()
            );

            yield return new WaitForSeconds(1f); // Update player position every second
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Rotate only along the y-axis if the bird is flying
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            if (movementType == MovementType.Fly)
            {
                // Lock rotation to y-axis only (yaw rotation) for flying
                targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * movementSpeed);
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        // Check if the bird reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            isMoving = false; // Stop moving after reaching the target
            animator.SetBool("isFlying", false);  // Stop fly animation
            animator.SetBool("isHopping", false); // Stop hop animation
            animator.SetInteger("hop", 0);   // Reset hop count in Animator
        }
    }

    IEnumerator ChangeBehavior()
    {
        while (true)
        {
            // Randomly select a new movement type, but enforce ground rules for walking
            movementType = (MovementType)Random.Range(0, 3); // 0 = Stand, 1 = Walk, 2 = Fly

            // Set movement behavior based on selected type
            switch (movementType)
            {
                case MovementType.Stand:
                    isMoving = false; // Bird stands still
                    animator.SetBool("isFlying", false);
                    animator.SetBool("isHopping", false);
                    animator.SetInteger("hop", 0); // Reset hop count
                    break;

                case MovementType.Walk:
                    isMoving = false;
                    hopCount = Random.Range(minHops, maxHops + 1); // Randomize hop count
                    animator.SetBool("isFlying", false);           // Stop flying animation
                    animator.SetBool("isHopping", true);           // Start hop animation
                    animator.SetInteger("hop", hopCount);     // Set hop count in Animator
                    SetNewTargetPosition(false);                   // Walk to a nearby target
                    break;

                case MovementType.Fly:
                    isMoving = true;
                    animator.SetBool("isFlying", true);            // Start flying animation
                    animator.SetBool("isHopping", false);          // Stop hop animation
                    animator.SetInteger("hop", 0);            // Reset hop count in Animator
                    SetNewTargetPosition(true);                    // Fly to a target at flyHeight
                    break;
            }

            // Wait for the interval before changing behavior again
            yield return new WaitForSeconds(behaviorChangeInterval);
        }
    }

    void SetNewTargetPosition(bool isFlying)
    {
        // Random direction within the radius around the player
        Vector2 randomDirection = Random.insideUnitCircle.normalized * playerRadius;
        targetPosition = new Vector3(playerPosition.x + randomDirection.x, 0, playerPosition.z + randomDirection.y);

        if (isFlying)
        {
            targetPosition.y = flyHeight; // Set the flying height for the bird
        }
        else
        {
            targetPosition.y = 0; // Keep the bird on the ground if walking
        }
    }

    // Method to smoothly bring the bird down to ground level
    void ReturnToGround()
    {
        Vector3 groundedPosition = new Vector3(transform.position.x, 0, transform.position.z);
        animator.SetBool("isFlying", true);
        transform.position = Vector3.MoveTowards(transform.position, groundedPosition, groundReturnSpeed * Time.deltaTime);
        
    }
}
