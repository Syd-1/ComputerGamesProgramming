using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float speed = 600.0f;
		public float pushForce = 500.0f;
		public float turnSpeed = 400.0f;
		public Text score;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;
		private int chickensLeft;

	void Start () 
	{
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
			chickensLeft = GameObject.FindGameObjectsWithTag("chicken").Length;
	}

		void OnTriggerEnter(Collider coll)
		{
			if ((coll.gameObject.tag == "chicken") && Input.GetKey("space"))
			{
				Debug.Log("tag matches");
				Destroy(coll.gameObject);
				chickensLeft--;
			}
		}

	void Update (){
		if (Input.GetKey("space"))
		{
			anim.SetInteger("AnimationPar", 2);
		}
		else if (Input.GetKey("d"))
		{
			anim.SetInteger("AnimationPar", 1); ;
		}
		else if (Input.GetKey("w"))
		{
			anim.SetInteger("AnimationPar", 1);
		}
		else if (Input.GetKey("a"))
		{
			anim.SetInteger("AnimationPar", 1);
		}
		else
		{
			anim.SetInteger("AnimationPar", 0);
		}

		if(controller.isGrounded){
			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
		}		

		float turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		controller.Move(moveDirection * Time.deltaTime);
		moveDirection.y -= gravity * Time.deltaTime;

		if (chickensLeft == 0)
		{
			score.text = "YOU WIN!!!";
		}
		else
    		{
			score.text = "Chickens Loose: " + chickensLeft.ToString();
    		}	
	}
}
