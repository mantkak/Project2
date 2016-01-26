using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyObjectScript : MonoBehaviour {
	public List<GameObject>enemySign1;
	public List<GameObject>enemySign2;
	public GameObject mainGame;
	public List<GameObject> auraEffectSelectionList;
	public ParticleSystem boomEffect;
	private GameManager gameManager=GameManager.getSingleton();
//	private Dictionary<int,int>enemySignDict1=new Dictionary<int, int>();
//	private Dictionary<int,int>enemySignDict2=new Dictionary<int, int>();
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void destroyEnemy(){
		if(gameObject.activeInHierarchy){
			foreach(GameObject effect in auraEffectSelectionList){
				effect.SetActive(false);
			}
			gameObject.SetActive(false);
			Debug.Log(gameObject+" MISS !!");
			gameManager.getEnemyList().RemoveRange(0,2);
			gameManager.setIsFirst(true);
			gameObject.GetComponentInParent<EnemyGenerator>().getUsingEnemyList().RemoveAt(0);
			mainGame.GetComponent<MainGame>().getGenaratorOrderList().RemoveAt(0);
			mainGame.GetComponent<MainGame>().HPgenerator.onDestroyHP();
		}
	}

	public void onActiveDestroy(){
		boomEffect.transform.position=gameObject.transform.position;
		boomEffect.Play();
		Debug.Log(gameObject+"    active destroy!!!!!    "+boomEffect.isPlaying);
		gameObject.SetActive(false);
		gameManager.getEnemyList().RemoveRange(0,2);
		//boomEffect.Play();
	}

	public void setEnemySign(){
		//set position1
		int position1 = Random.Range(0,enemySign1.Count);
		foreach(GameObject enemySing1 in enemySign1){
			enemySing1.SetActive(false);
		}
		enemySign1[position1].SetActive(true);
		gameManager.addEnemyList(position1);

		//set position2
		int position2 = Random.Range(0,enemySign2.Count);
		foreach(GameObject enemySing2 in enemySign2){
			enemySing2.SetActive(false);
		}
		enemySign2[position2].SetActive(true);
		gameManager.addEnemyList(position2);
	
	}

	public List<GameObject> getAuraEffectSelectionList(){
		return auraEffectSelectionList;
	}
}
