using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterMovement movement;

    [SerializeField] PartyController partyFollow;

    RayCast rayCast;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<CharacterMovement>();

        partyFollow = GetComponent<PartyController>();

        rayCast = GetComponent<RayCast>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.MoveCharacter();

        partyFollow.FollowPlayer();

        rayCast.RayCastGenerator();
    }
}
