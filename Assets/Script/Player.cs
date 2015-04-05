using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject PlayerBullet;
	//PlayerBulletの座標
	private Vector3 PosBullet;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0)){
			//PosBulletにPlayer(this)の座標を取得
			PosBullet = this.transform.localPosition;
			//PlayerBulletの座標を調整
			PosBullet.y += 0.4f;
			//PlayerBulletの生成
			PlayerBullet = Resources.Load("Prefabs/PlayerBullet") as GameObject;
			Instantiate(PlayerBullet, new Vector3(PosBullet.x, PosBullet.y, PosBullet.z), Quaternion.identity);
		}
	}
}


