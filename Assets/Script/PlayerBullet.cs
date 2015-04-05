using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
	//攻撃目標の座標
	private Vector3 Target;
	//加速度の値
	private float forceValue = 5f;
	//指定位置
	private Vector3 nVec;

	// Use this for initialization
	private void Start () 
	{

		//Rayを発射してクリックした座標を取得
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		//z=100までにオブジェクトがあるか
		if(Physics.Raycast(ray, out hit,100))
		{
			//目標 = RaycastHitで衝突したオブジェクト取得 = クリックした座標を取る
			//RaycastHitが衝突した座標を取得
			Target = hit.point;
			//ターゲットを指定位置に飛ばす
			//指定位置（ターゲット座標ーコントロール座標）
			nVec = Vector3.Normalize(Target - this.transform.position);
			//高さを固定
			nVec.y = 0.0f;
			//飛ばす
			this.rigidbody.velocity = nVec * forceValue;
		}

	}
	
	//Objectに当たった時の判定
	private void OnCollisionEnter(Collision collision) 
	{
		//BlockかEnemyに当たったら消滅
		if(collision.gameObject.tag == "Block" || collision.gameObject.tag == "Enemy")
		{
			Destroy(this.gameObject);
			//Enemyの場合はEnemyも消滅
			if (collision.gameObject.tag == "Enemy") 
			{
				GameManager.SP.HitEnemy();
				Destroy(collision.gameObject);
			}
		}
		
	}



}
