using UnityEngine;
using System.Collections;

public class FollowAgent : MonoBehaviour {

	protected NavMeshAgent		 agent;
	protected Animator			animator;
	protected Locomotion  locomotion;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;

		animator = GetComponent<Animator>();
		locomotion = new Locomotion(animator);

	}


	protected void SetupAgentLocomotion()
	{
		if (AgentDone())
		{
			locomotion.Do(0, 0);
		}
		else
		{
			float speed = agent.desiredVelocity.magnitude;
			Vector3 velocity = Quaternion.Inverse(transform.rotation) * agent.desiredVelocity;
			float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;
			locomotion.Do(speed, angle);
		}
	}

	void OnAnimatorMove()
	{
		agent.velocity = animator.deltaPosition / Time.deltaTime;
		transform.rotation = animator.rootRotation;
	}

	protected bool AgentDone()
	{
		return !agent.pathPending && AgentStopping();
	}

	protected bool AgentStopping()
	{
		return agent.remainingDistance <= agent.stoppingDistance;
	}


	void OnTriggerStay(Collider col)
	{
		//print("col_enter:"+col.gameObject.tag);
		//Playerが一定距離に近づいたら追いかける
		if (col.gameObject.tag == "Player") {

			if ( this.enabled == false ) return;
			Vector3 dir = this.transform.position - col.transform.position;
			//↓追いかけてくる場合は - dir にする
			Vector3 pos = this.transform.position - dir * 0.02f;
			agent.destination = pos;

		}
		

	}



	// Update is called once per frame
	void Update () 
	{
		SetupAgentLocomotion();

	}

}
