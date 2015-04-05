using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	//攻撃目標
	private GameObject TargetObject;
	//加速度の値
	private float forceValue = 5f;
	//指定位置
	private Vector3 nVec;

	// Use this for initialization
	private void Start () 
	{
	 TargetObject = GameObject.FindWithTag("Player");
	//ターゲットを指定位置に飛ばす
	//指定位置（ターゲット座標ーコントロール座標）
	nVec = Vector3.Normalize(TargetObject.transform.position - this.transform.position);
	//高さを固定
	nVec.y = 0.0f;
	//飛ばす
	this.rigidbody.velocity = nVec * forceValue;
	}

	//Objectに当たった時の判定
	private void OnCollisionEnter(Collision collision) 
	{
		//BlockPlayerかに当たったら消滅
		if(collision.gameObject.tag == "Block" || collision.gameObject.tag == "Player")
		{
			Destroy(this.gameObject);
			//Playerの場合はPlayerのLifeを削る
			if (collision.gameObject.tag == "Player")
			{
				GameManager.SP.HitPlayer();
				//Destroy(collision.gameObject);
			}

		}
		
	}

	
}
