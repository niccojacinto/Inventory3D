using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        Inventory inventory = (Inventory)target;

        if (GUILayout.Button("Generate Inventory Slots"))
        {
            SlotShape ss = new SlotShape(new int[,,]
            {
                { // Layer 1
                    {1,1},
                    {1,1},
                    {1,1}
                },
                { // Layer 2
                    {1,1},
                    {1,1},
                    {1,1}
                }
            });
            inventory.GenerateInventory(ss);

        }

    }
}
