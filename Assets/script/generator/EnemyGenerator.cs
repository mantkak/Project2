using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {
	public GameObject enemyLine;
	private List<GameObject> enemyList;
	private List<GameObject> usingEnemyList;
	private List<GameObject> pauseEnemyList;
	int objNumber;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initialGenerator(){
		enemyList = new List<GameObject>();
		usingEnemyList = new List<GameObject>();
		pauseEnemyList = new List<GameObject>();
		objNumber=0;
	}

	public void restartGenerator(){
		objNumber=0;
		foreach(GameObject enemy in enemyList){
			Destroy(enemy);
		}
		enemyList.Clear();
		usingEnemyList.Clear();
		pauseEnemyList.Clear();
		Debug.Log("restart usingEnemyList "+usingEnemyList.Count+"  pauseEnemyList"+pauseEnemyList.Count);
	}

	public void onSetSelectionEffect(int index){
		if(usingEnemyList[0].activeInHierarchy){
			usingEnemyList[0].GetComponent<EnemyObjectScript>().getAuraEffectSelectionList()[index].SetActive(true);
		}
	}

	public void onResetEffect(){
		for(int i=0;i<enemyList.Count;i++){
			foreach(GameObject effect in enemyList[i].GetComponent<EnemyObjectScript>().getAuraEffectSelectionList()){
				effect.SetActive(false);
			}
		}
	}

	public void creatEnemy(){
		//Debug.Log (gameObject.name+"position"+gameObject.transform.position);
		GameObject enemyTemp = enemyPooler(gameObject.transform.position);

		enemyTemp.GetComponent<EnemyObjectScript>().setEnemySign();
	}

	private GameObject enemyPooler(Vector3 position){
		//Debug.Log(gameObject+" enemyList.Count is "+enemyList.Count);
		for (int i = 0; i< enemyList.Count;i++){
			if(!enemyList[i].activeInHierarchy){
				enemyList[i].transform.position = position;
				enemyList[i].SetActive(true);
				usingEnemyList.Add(enemyList[i]);
				Debug.Log("usingEnenmyList  is  "+usingEnemyList.Count);
				return enemyList[i];
			}
			objNumber=i+1;
			//Debug.Log (gameObject+"  i is "+i+" objNum is "+objNumber);
		}
		GameObject obj =  NGUITools.AddChild(gameObject,enemyLine);
		//obj.transform.localScale = new Vector3(0.003472f,0.003472f,1f);
		obj.name = enemyLine.name+"_BeatPooler   "+objNumber  ;
		obj.SetActive(true);
		enemyList.Add (obj);
		Debug.Log("usingEnenmyList(new)  is  "+usingEnemyList.Count);
		usingEnemyList.Add(obj);
		return obj;

	}

	public void destroyActiveEnemy(){
	
		if(usingEnemyList[0].activeInHierarchy){
			//usingEnemyList[0].SetActive(false);
			//usingEnemyList[0].GetComponent<EnemyObjectScript>().boomEffect.Play();
			Debug.Log("enemy "+usingEnemyList[0].name+"  is destroying");
			usingEnemyList[0].GetComponent<EnemyObjectScript>().onActiveDestroy();
			usingEnemyList.RemoveAt(0);
		}
	}

	public void pauseEnemy(){
		foreach(GameObject obj in enemyList){
			if(obj.activeInHierarchy){
				obj.GetComponent<Animator>().speed=0;
				pauseEnemyList.Add(obj);
			}
		}
	}

	public void resumeEnemy(){
		foreach(GameObject obj in pauseEnemyList){
			obj.GetComponent<Animator>().speed=1;
		}
		pauseEnemyList.Clear();
	}


	public List<GameObject> getUsingEnemyList(){
		return usingEnemyList;
	}
}

