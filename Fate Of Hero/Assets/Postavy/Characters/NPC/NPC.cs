
using UnityEngine;
namespace Data
{
    [CreateAssetMenu(menuName = "CharacterData/NPC", fileName = "NewNPCDATA")]
    public class NPC : Character, IEnemyNPC
    {
        [Space]
        [Header("NPC")]
        //[SerializeField]
      //  private EntityTimePlan timePlan;
        [Tooltip("HearRadius")]
        [SerializeField]
        private float hearRadius;
        [Tooltip("CanSwim")]
        [SerializeField]
        private bool canSwim;
        [Tooltip("CanDive")]
        [SerializeField]
        private bool canDive;

        public float HearRadius { get => hearRadius > 0 ? hearRadius : 0; set => hearRadius = value; }
        public bool CanSwim { get => canSwim; }
        public bool CanDive { get => canDive; }
       //public EntityTimePlan TimePlan { get => timePlan; set => timePlan = value; }
    }
}