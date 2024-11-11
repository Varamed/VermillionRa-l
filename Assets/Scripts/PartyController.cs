using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    [SerializeField] Transform player;
    public List<Transform> partyMembers;
    [SerializeField] float followDistance = 2.0f;
    [SerializeField] float moveSpeed = 3.0f;
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] float formationSpacing = 2.0f;
    [SerializeField] float minimumDistance = 1.0f;
    
    void Update()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        Vector3 forwardDirection = player.forward;
        Vector3 rightDirection = player.right;

        for (int i = 0; i < partyMembers.Count; i++)
        {
            Transform member = partyMembers[i];

            Vector3 targetPosition = player.position + rightDirection * (formationSpacing * (i - (partyMembers.Count - 1) / 2.0f)) + forwardDirection * -followDistance;
            
            if (Vector3.Distance(member.position, targetPosition) > minimumDistance)
            {
                member.position = Vector3.Lerp(member.position, targetPosition, moveSpeed * Time.deltaTime);
            }

            Vector3 lookDirection = targetPosition - member.position;
            if (lookDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                member.rotation = Quaternion.Slerp(member.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            if (targetPosition.magnitude < 0.1f)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        AvoidClumping();
    }
    void AvoidClumping()
    {
        for (int i = 0; i < partyMembers.Count; i++)
        {
            for (int j = 1; j < partyMembers.Count; j++)
            {
                Transform memberA = partyMembers[i];
                Transform memberB = partyMembers[j];

                float distance = Vector3.Distance(memberA.position, memberB.position);

                if (distance < minimumDistance)
                {
                    Vector3 direction = (memberA.position - memberB.position).normalized;
                    memberA.position += direction * (minimumDistance - distance) / 2;
                    memberB.position -= direction * (minimumDistance - distance) / 2;
                }
            }
        }
    }
}
