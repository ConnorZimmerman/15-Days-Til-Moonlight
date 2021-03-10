using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	string type;
	int amount;
	GameObject pouchObject;
	public Item(string type = "", int amount = 0)
	{
		this.type = type;
		this.amount = amount;
		this.pouchObject = GameObject.Find("Item_Pouch");
	}
}
