using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameLevel{
	NONE=0,
	EASY,
	NORMAL,
	HARD
}

public class GameManager{
	public bool isStarGame=false;
	public List<int> enemyList = new List<int>();
	
	private static GameManager gameManager;
	private GameConstant gameConstant;
	private bool isFirst =true;
	private GameLevel gameLevel = GameLevel.NONE;
	private bool isGameOver=false;


	public void setIsGameOver(bool status){
		isGameOver = status;
	}

	public bool getGameOver(){
		return isGameOver;
	}

	public bool getIsFirst(){
		return isFirst;
	}

	public void setIsFirst(bool status){
		isFirst = status;
	}

	public void setGameLevel(GameLevel level){
		gameLevel = level;
	}

	public GameLevel getGameLevel(){
		return gameLevel;
	}

	public void resetEnemyList(){
		enemyList.Clear();
	}

	public List<int> getEnemyList(){
		return enemyList;
	}

	public static GameManager getSingleton(){
		if(gameManager==null){
			gameManager= new GameManager();
		}
		return gameManager;
	}

	public GameConstant getGameConstant(){
		if(gameConstant==null){
			gameConstant=new GameConstant();
		}
		return gameConstant;
	}

	public void setIsStarGame(bool status){
		isStarGame=status;
	}

	public void addEnemyList(int position){
		//Debug.Log("Add  position "+position);
		enemyList.Add(position);
	}

	public int getScoreByGameLevel(){
		gameConstant = getGameConstant();
		switch(gameLevel){
		case GameLevel.EASY : return gameConstant.getEasyScore();
		case GameLevel.NORMAL : return gameConstant.getNormalScore();
		case GameLevel.HARD : return gameConstant.getHardScore();
		default :return 0;
		}
	}

}
