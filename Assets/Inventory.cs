using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour {

    [SerializeField]
    GameObject inventorySlotPrefab;

    [SerializeField]
    InventorySlot[,,] inventorySlots;

    Vector3 endScale;

    private void Start() {
        endScale = transform.localScale;
        transform.localScale = Vector3.zero;
        

        SlotShape ss = new SlotShape(new int[,,]
        {
            { // Layer 1
                {1,1,1},
                {1,1,1},
            },
            { // Layer 2
                {1,1,1},
                {1,1,1},
            }
        });
        GenerateInventory(ss);
        gameObject.SetActive(false);
    }

    public IEnumerator Open() {
        float elapsed = 0f;
        float duration = 0.5f;
        while (elapsed < duration) {
            transform.localScale = Vector3.Lerp(Vector3.zero, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator Close() {
        float elapsed = 0f;
        float duration = 0.5f;
        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }


    // Should be optimized, currently no idea the best way to do it.
    public void HighlightSlotsOnHover(Item item) {
        foreach (InventorySlot iSlot in inventorySlots)
        {
            iSlot.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.3f);
        }

        foreach (IntVector3 slotPoint in item.slotPoints)
        {
            InventorySlot slot = GetInventorySlot(slotPoint, item);
            if (slot != null)
                slot.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.3f);
        }

    }


    public void GenerateInventory(SlotShape slotShape) {
        inventorySlots = new InventorySlot[slotShape.slotShape.GetLength(0), 
            slotShape.slotShape.GetLength(1), 
            slotShape.slotShape.GetLength(2)];

        foreach (IntVector3 position in slotShape.slotPoints)
        {
            CreateInventorySlot(position);
        }
    }

    void CreateInventorySlot(IntVector3 position) {
        GameObject go = Instantiate(inventorySlotPrefab, transform);
        inventorySlots[position.z,position.y,position.x] = go.GetComponent<InventorySlot>();
        go.transform.localPosition = position.vector3;
        go.name = string.Format("InventorySlot({0},{1},{2})", position.z, position.y, position.x);
    }

    public InventorySlot GetInventorySlot(IntVector3 position, Item item) {
        if (item == null) return null;
        Matrix4x4 rotMatrix = Matrix4x4.Rotate(item.transform.rotation);
        Vector3 pointWithRot = rotMatrix.MultiplyPoint3x4(position.vector3);
        // Convert To the inventory's localPoint and use it's scale
        IntVector3 localPoint = new IntVector3(transform.InverseTransformPoint(
            item.transform.position + pointWithRot * transform.localScale.x)); // only using scale.x because its a cube

        try {
            return inventorySlots[localPoint.z, localPoint.y, localPoint.x];
        } catch (IndexOutOfRangeException)
        {
            return null;
        } catch (NullReferenceException)
        {
            return null;
        }
    }

    public void StoreItem(Item item) {
        if (CanStoreItem(item))
        {
            List<InventorySlot> slotsTaken = new List<InventorySlot>();
            foreach (IntVector3 slotPoint in item.slotPoints)
            {
                InventorySlot slot = GetInventorySlot(slotPoint, item);
                slot.free = false;
                slot.gameObject.SetActive(false);
            }
            item.transform.SetParent(transform);
        }
    }

    public void RetrieveItem(Item item) {
        List<InventorySlot> slotsTaken = new List<InventorySlot>();
        foreach (IntVector3 slotPoint in item.slotPoints)
        {
            InventorySlot slot = GetInventorySlot(slotPoint, item);
            slot.free = true;
            slot.gameObject.SetActive(true);
        }
        item.transform.SetParent(null);
    }

    public bool CanStoreItem(Item item) {
        List<InventorySlot> availableSlots = new List<InventorySlot>();
        foreach (IntVector3 slotPoint in item.slotPoints)
        {
            InventorySlot slot = GetInventorySlot(slotPoint, item);
            if (slot != null && slot.free && !availableSlots.Contains(slot))
            {
                availableSlots.Add(slot);
            } else
            {
                return false;
            }
        }

        return true;
    }

    /*
    private void OnDrawGizmos() {

        foreach (IntVector3 iv3 in selectedItem.slotPoints)
        {
            Matrix4x4 rotMatrix = Matrix4x4.Rotate(selectedItem.transform.rotation);

            Vector3 pointWithRot = rotMatrix.MultiplyPoint3x4(iv3.vector3);

            // Convert To the inventory's localPoint and use it's scale
            IntVector3 localPoint = new IntVector3(transform.InverseTransformPoint(
                selectedItem.transform.position + pointWithRot * transform.localScale.x)); // only using scale.x because its a cube

            // Convert Back to World Point for debugging, but not necessary
            Gizmos.DrawSphere(transform.TransformPoint(localPoint.GetIntVector()), 0.05f);

        }


    }
    */

}
