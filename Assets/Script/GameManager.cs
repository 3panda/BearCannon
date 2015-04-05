using UnityEngine;
using System.Collections;


public enum GameState { playing, won, lost };

public class GameManager : MonoBehaviour {
	public static GameManager SP;

	//GameState
	private GameState gameState;

	//Enemy
	private int totalEnemy;
	private int enemyHit;

	//Player
	private int lifePowerOfPlayerMax;
	private int lifePowerOfPlayer;
	private int damageOfPlayer;

	

	private void Awake ()
	{
		SP = this;

		lifePowerOfPlayerMax = 3;
		//初期化
		lifePowerOfPlayer = lifePowerOfPlayerMax;
		Time.timeScale = 1.0f;
		totalEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
		//Debug.Log("totalEnemy");
	}



	void OnGUI()
	{

		GUILayout.Space(10);
		GUILayout.Label("  Life: " + ":" + lifePowerOfPlayer + "/" + lifePowerOfPlayerMax);

		if (gameState == GameState.lost)
		{
				GUILayout.Label("You Lost!");
				if (GUILayout.Button("Try again"))
				{
						Application.LoadLevel(Application.loadedLevel);
				}
		}
		else if (gameState == GameState.won)
		{
				GUILayout.Label("You won!");
				if (GUILayout.Button("Play again"))
				{
						Application.LoadLevel(Application.loadedLevel);
				}
		}
	}




	//HitEnemy
	public void HitEnemy()
	{
		enemyHit++;
		
		if (enemyHit >= totalEnemy)
		{
			WonGame();
		}
	}

	//HitPlayer
	public void HitPlayer()
	{
		lifePowerOfPlayer --;
		if (lifePowerOfPlayer == 0)
		{
			SetGameOver();
		}
	}


	//GameOver
	public void SetGameOver()
	{
		Time.timeScale = 0.0f; //Pause game
		gameState = GameState.lost;
		Debug.Log("LOSS");
	}

	//WonGame
	public void WonGame()
	{
		Time.timeScale = 0.0f; //Pause game
		gameState = GameState.won;
		Debug.Log("WIN!!");
	}




}
