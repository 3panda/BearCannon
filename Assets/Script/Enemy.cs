using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	//Bullet
	private GameObject EnemyBullet;
	//EnemyBulletの座標
	private Vector3 PosBullet;
	//攻撃可能かどうか
	private bool Attack;//このままだといらない？
	//弾丸の発射回数
	private int NumberOfAttacks;
	//弾丸発射後の経過時間
	private float timer = 0; 
	//発射の時限管理
	private bool TimedManagement; 

	private void Start () 
	{
		//攻撃可能かどうか
		Attack = false;

	}

	private void Update ()
	{

		//Debug
		Debug.Log("Attack" + Attack);
		Debug.Log("NumberOfAttacks" + NumberOfAttacks);
		Debug.Log("TimedManagement" + TimedManagement);
		Debug.Log("経過時間" + timer);
	}


	private void OnTriggerEnter(Collider col)
	{
		
		//Playerが接触したら
		if (col.gameObject.tag == "Player") 
		{
			Debug.Log("Playerに接触："+Attack);

			Attack = true;
			//弾丸の発射回数の初期化
			NumberOfAttacks = 0;
			//攻撃停止時間をカウントを初期化
			timer = 0.0f;

		}


		//攻撃可能の場合のみ実行
		if( Attack == true)
		{
			//発火を　3.0秒事に繰り返す
			InvokeRepeating("Fire", 1.0f, 3.0f);
		} 

	
	}



	//Colliderから離れたら攻撃不可能にする
	private void OnTriggerExit(Collider col)
	{
		Attack = false;
		Debug.Log("Playerが離れた："+Attack);
		//離れたら弾丸の生成を止める
		Reset ();
	}


//弾丸の生成
	private void Fire ()
	{
		//弾丸のカウントを初期化
		//this(Enemy)のPostioonを取得
		PosBullet = this.transform.localPosition;
		//PlayerBulletの座標を調整
		PosBullet.y += 0.4f;

		//攻撃は三回まで
		if (NumberOfAttacks < 3)
		{
			EnemyBullet = Resources.Load("Prefabs/EnemyBullet") as GameObject;
			Instantiate(EnemyBullet, new Vector3(PosBullet.x, PosBullet.y, PosBullet.z), Quaternion.identity);
			//攻撃回数をカウント
			NumberOfAttacks ++ ;
		} else {
			//攻撃が三回になったら停止
			Reset ();
			//攻撃不可
			//Attack = false;
			//攻撃不可能時間のカウントを開始
			TimedManagement = true;
		}

	}



	private void OnTriggerStay(Collider col)
	{

			//攻撃の時間管理
			if (TimedManagement == true)
			{
				//攻撃停止時間をカウント
				timer += Time.deltaTime;
				//攻撃停止時間が5秒経過したらカウントを停止
				if (timer > 10.0f){
					TimedManagement = false;
					timer = 0.0f;
					Debug.Log("待機時間終了");
					//攻撃可能に
					//Attack = true;
					//攻撃可能の場合のみ実行
					if( Attack == true)
					{
						//発火を　3.0秒事に繰り返す
						InvokeRepeating("Fire", 1.0f, 3.0f);
						Debug.Log("Attack開始" + Attack);
					}
					
				}
			}

	}


	//弾丸の停止とカウントの初期化
	private void Reset ()
	{
		//弾丸のカウントを初期化
		NumberOfAttacks = 0;
		//攻撃停止時間をカウントを初期化
		timer = 0.0f;
		//弾丸の時間差生成を停止
		CancelInvoke("Fire");
		Debug.Log("止めた: "+Attack);

	}

}


