using NodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using InventorySystem;

namespace DialogEditor
{
    public enum EDialogEvents { AddItemToPlayersInventory, AddQuest }
    [CreateAssetMenu(menuName = "DialogEditor/Nodes/Event")]
    public class DialogEventNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
            var list = b.dialogEvents;
#if UNITY_EDITOR
            b.eventListSize = EditorGUILayout.IntField("size", list.Count);
#endif

            if (!b.collapse)
            {
                while (b.eventListSize < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                    b.addItems.RemoveAt(b.addItems.Count - 1);
                    b.WindowRect.height = b.drawNode.Height;

                }
                while (b.eventListSize > list.Count)
                {
                    list.Add(EDialogEvents.AddItemToPlayersInventory);
                    b.addItems.Add(new ItemReward());


                }
                for (int i = 0; i < list.Count; i++)
                {
#if UNITY_EDITOR
                    list[i] = (EDialogEvents)EditorGUILayout.EnumPopup(list[i]);
#endif
                    switch (list[i])
                    {
                        case EDialogEvents.AddItemToPlayersInventory:
#if UNITY_EDITOR
                            EditorGUILayout.LabelField("Item: ");
#endif
                            ItemReward it = b.addItems[i];
                            it.Item = EditorGUILayout.ObjectField(it.Item, typeof(Item), false) as Item;
#if UNITY_EDITOR
                            EditorGUILayout.LabelField("count: ");
                            it.Count = EditorGUILayout.IntField(it.Count);
#endif
                            b.addItems[i] = it;
                            break;

                    }
#if UNITY_EDITOR
                    EditorGUILayout.LabelField("");
#endif
                }
                b.WindowRect.height = b.drawNode.Height + (list.Count * 90);

            }
        }

        public override void Execute(BaseNode b)
        {
            foreach (EDialogEvents e in b.dialogEvents)
            {
                switch (e)
                {
                    case EDialogEvents.AddItemToPlayersInventory:

                        foreach (ItemReward r in b.addItems)
                        {
                           InventoryManager.Instance.Inventory.Add(r.Item, r.Count);
                        }
                        b.nodeCompleted = true;
                        break;
                    case EDialogEvents.AddQuest:


                        break;
                }
            }
        }
    }
}