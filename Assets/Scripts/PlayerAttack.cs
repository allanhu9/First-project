using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackCoolDown;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCoolDown)
        {
            Attack();
        }

    }

    private void Attack()
    {

    }
}
