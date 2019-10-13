
using UnityEngine;
using System.Extensions;
using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public enum ECharacterStats { }
    public enum EItemStats {__Damage }
    public enum EOperator { plus, minus }

    [CreateAssetMenu(menuName = "Inventory/Items/Consumeable")]
    public class Consumeable : Item
    {
        [SerializeField]
        private float effect, duration;
        [SerializeField]
        private EOperator operation;
        [SerializeField]
        private List<ConsumeableItemTarget> itemTargets;
        public override void Use(Slot slot)
        {
            foreach (ConsumeableItemTarget t in itemTargets)
            {
                t.Use(effect);
            }
            slot.Remove();
            Debug.Log($"<color=green>item: {name} was used</color>");

            //TODO add all target properties
            //TODO timing for effect, constant || timeRanged
            //TODO loading effect in time
        }
    }
    [Serializable]
    public class ConsumeableItemTarget
    {
        [SerializeField]
        private Item item;
        [SerializeField]
        private List<EItemStats> targetStats = new List<EItemStats>();

        public void Use(float effect)
        {
            foreach (EItemStats s in targetStats)
            {
                Debug.Log($"item: {s} value befor effect {item.GetPropertyValueByName(s.ToString()).ToString()}");

                item.SetPropertyValueByName(s.ToString(),int.Parse(item.GetPropertyValueByName(s.ToString()).ToString()) + effect);

                Debug.Log($"item: {s} value after effect {item.GetPropertyValueByName(s.ToString()).ToString()}");

            }
        }
    }
    [Serializable]
    public class ConsumeableCharacterTarget
    {
        /* [SerializeField]
          private CharacterData data;
          [SerializeField]
          private List<ECharacterStats> targetStats = new List<ECharacterStats>();

          public void Use(float effect)
          {
              foreach (ECharacterStats s in targetStats)
              {
                  data.SetPropertyValueByName(s.ToString(), (float)data.GetPropertyValueByName(s.ToString()) + effect);
              }
          } */


    }
}