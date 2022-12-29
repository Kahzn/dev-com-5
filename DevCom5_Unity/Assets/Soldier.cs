using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Soldier : MonoBehaviour
{
    public float health = 10f;
    public float speed = 2f;

    public Faction faction;

    public Animator animator;

    private bool selected = true;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {

        if (selected)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (move != Vector3.zero)
            {
                transform.forward = move;
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            if (Input.GetButtonDown("Jump"))
            {
                Shoot();
            }
            else
            {
                animator.SetBool("attacking", false);
            }
        }
    }

    private void Shoot()
    {
        animator.SetBool("attacking", true);
        Debug.Log("Player shooting");
    }

}
