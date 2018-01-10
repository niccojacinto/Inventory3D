using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

    public Inventory inventory;

    OpenChest openChest;

    private void Start() {
        openChest = GetComponentInChildren<OpenChest>();
        openChest.speed = 3f;

    }

    public void Open() {
        inventory.gameObject.SetActive(true);
        inventory.StartCoroutine(inventory.Open());
        openChest.opening = true;
        openChest.closing = false;
    }

    public void Close() {
        inventory.StartCoroutine(inventory.Close());
        openChest.opening = false;
        openChest.closing = true;
    }

    public void StoreItem(Item item) {
        inventory.StoreItem(item);
    }

    public void RetrieveItem(Item item) {
        inventory.RetrieveItem(item);
    }

    public void HighlightSlots(Item item) {
        inventory.HighlightSlotsOnHover(item);
    }
}
