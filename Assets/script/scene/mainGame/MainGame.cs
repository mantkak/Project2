using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainGame : MonoBehaviour {
	public ParticleSystem testParticle;
	public HitPointGenerator HPgenerator;
	public List<EnemyGenerator> generatorList;
	public GameObject startGameLabel;
	public GameObject countDownLabel;
	public GameObject scoreLabel;
	public GameObject pauseDialog;
	public GameObject resultDialog;
	public GameObject pauseButton;

	private int gameScore;
	private int countDown;
	private float time;
	private List<int> generatorOrderList = new List<int>();
	private GameManager gameManager ;
	// Use this for initialization
	void Start () {
		initial();
		InvokeRepeating("callGenerator",0,2);
	}
	// Update is called once per frame
	void Update () {
		if(gameManager.getGameOver()&&!gameManager.isStarGame){
			onPauseEndGame();
			resultDialog.GetComponentInChildren<UILabel>().text=gameScore.ToString();
			resultDialog.SetActive(true);
			setCharacter(false);
		}else if(!gameManager.getGameOver()&&gameManager.isStarGame){
			resultDialog.SetActive(false);
			setCharacter(true);
		}

		if(!gameManager.isStarGame){
			pauseButton.SetActive(false);
			foreach(GameObject character in HPgenerator.getCharacterList()){
				character.GetComponent<BoxCollider>().enabled=false;
			}
		}
	}

	private void initial(){
		resultDialog.SetActive(false);
		gameManager=GameManager.getSingleton();
		gameManager.setIsFirst(true);
		gameManager.setIsGameOver(false);
		setGameLevel();
		gameScore =0;
		countDown=3;
		gameManager.resetEnemyList();
		foreach(EnemyGenerator temp in generatorList){
			temp.initialGenerator();
		}
		HPgenerator.HPinitial();
		HPgenerator.createHP();
	}

	private void setCharacter(bool status){
		foreach(GameObject character in HPgenerator.getCharacterList()){
			character.SetActive(status);
		}
	}

	public void onRestartGame(){

		generatorOrderList.Clear();
		Debug.Log("generatorOrder is "+generatorOrderList.Count);
		pauseDialog.SetActive(false);
		pauseButton.SetActive(true);
		scoreLabel.GetComponent<UILabel>().text="score";
		foreach(EnemyGenerator temp in generatorList){
			temp.restartGenerator();
		}
		HPgenerator.resetHP();
		initial();
		countDownLabel.GetComponent<UILabel>().text="3";
		countDownLabel.SetActive(true);
		setCharacter(true);
	}

	public List<int> getGenaratorOrderList(){
		return generatorOrderList;
	}

	private void setGameLevel(){
		gameManager.setGameLevel(GameLevel.EASY);
	}

	private void callGenerator(){
		if(gameManager.isStarGame){
			int random = Random.Range(0,generatorList.Count);
			generatorList[random].creatEnemy();
			generatorOrderList.Add(random);
		}
	}

	public void onInputClick(int value){
		if(gameManager.getEnemyList().Count>=1){
			if(value==gameManager.getEnemyList()[0]&&gameManager.getIsFirst()){
				generatorList[generatorOrderList[0]].onSetSelectionEffect(0);
				gameManager.setIsFirst(false);
			}else if(value==gameManager.getEnemyList()[1]&&!gameManager.getIsFirst()){
				generatorList[generatorOrderList[0]].onSetSelectionEffect(1);
				generatorList[generatorOrderList[0]].destroyActiveEnemy();
				generatorList[generatorOrderList[0]].onResetEffect();
				generatorOrderList.RemoveAt(0);
				gameManager.setIsFirst(true);
				gameScore += gameManager.getScoreByGameLevel();
				scoreLabel.GetComponent<UILabel>().text ="score  "+gameScore.ToString();
			}else{
				//input parameter reset
				resetEffect();
				gameManager.setIsFirst(true);
			}
		}
	}

	public void resetEffect(){
		generatorList[generatorOrderList[0]].onResetEffect();
	}

	public void onCheckSomthing(){
		testParticle.Play();
	}

	public void onStartGameClick(){
		startGameLabel.SetActive(false);
		countDownLabel.SetActive(true);
		setDialogPlay();
	}

	public void onPauseEndGame(){
		if(!gameManager.isStarGame){
			gameManager.setIsStarGame(false);
			foreach(EnemyGenerator enemy in generatorList){
				enemy.pauseEnemy();
			}
		}
	}

	public void onPauseButton(){
		if(gameManager.isStarGame){
			gameManager.setIsStarGame(false);
			foreach(EnemyGenerator enemy in generatorList){
				enemy.pauseEnemy();
			}
			setCharacter(false);
			pauseDialog.SetActive(true);
			pauseButton.SetActive(false);
		}
	}

	public void onResume(){
		if(!gameManager.isStarGame){
			gameManager.setIsStarGame(true);
			foreach(EnemyGenerator enemy in generatorList){
				enemy.resumeEnemy();
			}
			setDialogPlay();
			setCharacter(true);
		}
	}

	private void setDialogPlay(){
		pauseDialog.SetActive(false);
		pauseButton.SetActive(true);
		foreach(GameObject character in HPgenerator.getCharacterList()){
			character.GetComponent<BoxCollider>().enabled=true;
		}
	}


	public void onCountDownEvent(){
		countDown--;
		if(countDown==0){
			gameManager.setIsStarGame(true);
			countDownLabel.SetActive(false);
			setDialogPlay();
		}
		countDownLabel.GetComponent<UILabel>().text=countDown.ToString();
		countDownLabel.GetComponent<TweenScale>().ResetToBeginning();
		countDownLabel.GetComponent<TweenScale>().PlayForward();
	}

}

