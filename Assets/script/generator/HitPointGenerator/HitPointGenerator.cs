using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitPointGenerator : MonoBehaviour {
	private List<GameObject> HitPointList= new List<GameObject>();
	private List<GameObject> HitPointIndexList= new List<GameObject>();
	private GameManager gameManager;

	public List<GameObject> CharacterHP;
	public GameObject HpObjectParent;
	public GameObject HpObjectChild;
	private int hpTemp;
	// Use this for initialization
	void Start () {
//		HPinitial();
//		//HpObjectParent.SetActive(true);
//		createHP();
	}

	public void resetHP(){
		if(HitPointList!=null){
			foreach(GameObject hp in HitPointList){
				Destroy(hp);
			}
			HitPointList.Clear();
			HitPointIndexList.Clear();

		}
	}

	public void HPinitial(){
		gameManager=GameManager.getSingleton();
		hpTemp=1;
//		HitPointList.Clear();
//		HitPointIndexList.Clear();
	}

	public void createHP(){
		float posY=0;
		for(int i =0;i<CharacterHP.Count;i++){
		GameObject obj =  NGUITools.AddChild(gameObject,HpObjectParent);
		obj.name = HpObjectParent.name+"_BeatPooler   "+i  ;
			obj.transform.localPosition= new Vector2(0,posY);
		obj.SetActive(true);
		createHPIndex(obj,HpObjectChild);
		HitPointList.Add (obj);
			posY +=(-20);
		}
	}


	public List<GameObject> getCharacterList(){
		return CharacterHP;
	}

	public void createHPIndex(GameObject parent, GameObject child){
		float posX=-40f;
		for(int i =0;i < gameManager.getGameConstant().getHPIndex();i++){
			GameObject obj =  NGUITools.AddChild(parent,child);
			//HpObjectChild.transform.localPosition= new Vector2(posX,0);
			//obj.transform.localScale = new Vector3(0.003472f,0.003472f,1f);
			obj.name = HpObjectChild.name+"_BeatPooler   "+i  ;
			obj.transform.localPosition= new Vector2(posX,0);
			obj.SetActive(true);
			HitPointIndexList.Add (obj);
			Debug.Log("add "+obj.name);
			posX+=40f;
		}
	}

	public void onDestroyHP(){
		Debug.Log("HitpointIndexList.Count "+HitPointIndexList.Count);
		if(HitPointIndexList.Count-hpTemp>=0){
			Debug.Log("decrease HP");
			HitPointIndexList[HitPointIndexList.Count-hpTemp].SetActive(false);
			if(HitPointIndexList.Count-hpTemp==0){
				Debug.Log("GAME OVER");
				gameManager.setIsStarGame(false);
				gameManager.setIsGameOver(true);

			}
			hpTemp++;
		}
	}



	// Update is called once per frame
	void Update () {
	
	}
}
