using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace NodeEditor
{
    public enum ECondition { IsThunder, IsSunnyDay, IsAlive, IsTimeToGoToWork, IsDead, IsNight, IsMorning, IsPlayerClose, ReachedDestination, }
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Condition")]
    public class ConditionNode : DrawNode
    {

        public override void DrawCurve(BaseNode b)
        {

        }

        public override void DrawWindow(BaseNode b)
        {
#if UNITY_EDITOR

            b.condition = (ECondition)EditorGUILayout.EnumPopup(b.condition);

#endif
        }
        public static bool IsChecked(ECondition c, CharacterScript character)
        {
            switch (c)
            {
                case ECondition.IsThunder:
                    return MainOpossum.GetWeather(character) == EWeather.Thunder;
                case ECondition.IsSunnyDay:
                    return MainOpossum.GetWeather(character) == EWeather.SunnyDay;

                case ECondition.IsAlive:

                    return character.CharacterData.IsAlive;

                case ECondition.IsTimeToGoToWork:

                    break;

                case ECondition.IsDead:
                    return character.CharacterData.IsAlive == false;

                case ECondition.IsNight:
                    break;

                case ECondition.IsMorning:


                case ECondition.IsPlayerClose:
                    return (character as EntityScript).PlayerIsClose();

                case ECondition.ReachedDestination:
                    return character.AgentReachedTarget();

            }
            return false;
        }

    }
}
