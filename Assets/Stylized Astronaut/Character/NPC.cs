using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	private Animator anim;
	private CharacterController controller;

	public float speed = 0.1f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 10.0f;

	bool isMove = false;
	int rotation;
	Random rand = new Random();

	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update()
	{

		if (!isMove)
        {
			if (Random.Range(0, 1000) < 3)
            {
				isMove = true;
				rotation = Random.Range(-180, 180);
				transform.Rotate(0, rotation, 0);
			}
		}
		else
        {
			if (Random.Range(0, 1000) < 10)
            {
				isMove = false;
            }
        }

		// processing animation
		if (isMove)
		{
			anim.SetInteger("AnimationPar", 1);
		}
		else
		{
			anim.SetInteger("AnimationPar", 0);
		}

		if (controller.isGrounded && isMove)
		{
			moveDirection = transform.forward * speed;
			//transform.Rotate(0, rotation, 0);
		}
		else
        {
			moveDirection = Vector3.zero;
			moveDirection.y -= gravity * speed;
		}

		
		controller.Move(moveDirection);
		
	}
}
