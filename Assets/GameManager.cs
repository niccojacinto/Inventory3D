using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Inventory inventory;

    public Item selectedItem;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.StoreItem(selectedItem);
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            inventory.RetrieveItem(selectedItem);
        }
	}
}
