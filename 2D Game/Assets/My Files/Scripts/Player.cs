using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats{
		public int maxHP = 100;
		public int maxMP = 100;
		public int expToNextLevel = 100;
		public int level = 0;

		public PlayerStats(){
		}
	}


	float time;
	public PlayerStats stats = new PlayerStats();
	public Transform health;
	public Transform mana;
	public Transform exp;
	//public Transform gold;
	public Text healthText;
	public Text manaText;
	public Text expText;
	public Text goldText;
	int curHP;
	int curMP;
	int curExp;
	int curGold;

	// Use this for initialization
	void Start () {
		time = Time.time; //temporary
		curHP = stats.maxHP;
		curMP = stats.maxMP;
		curExp = 0;
		curGold = 0;
	}
	
	// Update is called once per frame
	void Update () {
		health.localScale = new Vector3 ((float) curHP / stats.maxHP, 1, 1);
		healthText.text = "Health: " + curHP + "/" + stats.maxHP;

		mana.localScale = new Vector3 ((float) curMP / stats.maxMP, 1, 1);
		manaText.text = "Mana: " + curMP + "/" + stats.maxMP;
		
		exp.localScale = new Vector3 ((float) curExp / stats.expToNextLevel, 1, 1);
		expText.text = "Exp: " + curExp + "/" + stats.expToNextLevel;

		goldText.text = "Gold:\n" + curGold;
	}

	//Decrease a variable by a certain amount without going Below the minimum value
	int Decrease(int var, int decrease, int min){
		if (var > min) {
			var -= decrease;
			if(var < min){
				var = min;
			}
		}
		return var;
	}

	//Increase a variable by a certain amount without going above the maximum value
	int Increase(int var, int increase, int max){
		if (var < stats.maxHP) {
			var += increase;
			if(var > max){
				var = max;
			}
		}
		return var;
	}

	void OnTriggerStay2D (Collider2D other){

		if ((Time.time - time)>=1) {
			if (other.name == "Increase") {
				curHP = Increase (curHP, 10, stats.maxHP);
				Debug.Log ("Increase");
			}
			if (other.name == "Decrease") {
				curHP = Decrease (curHP, 10, 0);
				Debug.Log ("Decrease");
			}
			time = Time.time;
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Treasure") {
			curGold = Increase(curGold, 100, int.MaxValue);
			Destroy(other.gameObject);
			Debug.Log("Treasure");
		}
	}

}

