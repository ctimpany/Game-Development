using UnityEngine;
using System.Collections;

public class ItemDatabase : MonoBehaviour {

	[System.Serializable]
	public class Item{
		public string itemName;
		public string itemDesc;
		public enum ItemType{sword, armour, consumable};
		public ItemType itemType = ItemType.sword;
		public int effectDuration; //0 effectDuration means effect is indefinite, i.e. health potion.
		public Sprite itemIcon;
		public GameObject itemPrefab;
		public static int numStats = 0;
		public ItemStat[] itemStats = new ItemStat[numStats];
		public Item(){
		}
	}
	/*[System.Serializable]
	public class ItemWearable : Item{
		public enum ItemType{sword, armour};
		public ItemType itemType = ItemType.sword;
	}
	[System.Serializable]
	public class ItemConsumable : Item{
		public int effectDuration; //0 effectDuration means effect is indefinite, i.e. health potion.

		public ItemConsumable(){
		}
	}*/
	[System.Serializable]
	public class ItemStat{
		public enum StatType{attack, defense};
		public StatType statType = StatType.attack;
		public int statValue;
	}

	public static int numItems = 0;
	public Item[] items = new Item[numItems];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
