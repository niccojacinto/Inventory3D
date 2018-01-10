using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Storage storage;
    public Item selectedItem;

    void Update () {
		if (Input.GetKeyDown(KeyCode.O))
        {
            storage.Open();
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            storage.Close();
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            storage.StoreItem(selectedItem);
        } else if(Input.GetKeyDown(KeyCode.R))
        {
            storage.RetrieveItem(selectedItem);
        }


        if (selectedItem != null)
            storage.HighlightSlots(selectedItem);
    }
}
