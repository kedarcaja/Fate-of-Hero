using UnityEngine;
using System;


namespace Unity.Extensions
{
    public static class UnityExtensions
    {
        public static void Active(this CanvasGroup cg, bool withRayCast)
        {
            cg.alpha = 1;
            cg.blocksRaycasts = true;
        }
        public static void Deactive(this CanvasGroup cg, bool withRayCast)
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
        }
        public static bool IsActive(this CanvasGroup cg, bool withRaycast)
        {
            return (withRaycast ? cg.blocksRaycasts == true : true) && cg.alpha == 1;
        }
    }
}