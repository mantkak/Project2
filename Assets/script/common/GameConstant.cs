using UnityEngine;
using System.Collections;

public class GameConstant {
	private const int easyScore = 200;
	private const int normalScore = 300;
	private const int hardScore = 500;
	private const int HPIndexBegin = 3;


	public int getHPIndex(){
		return HPIndexBegin;
	}

	public int getEasyScore(){
		return easyScore;
	}

	public int getNormalScore(){
		return normalScore;
	}

	public int getHardScore(){
		return hardScore;
	}

}
