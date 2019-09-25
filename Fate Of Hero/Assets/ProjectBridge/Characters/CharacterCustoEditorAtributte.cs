using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]

public class CharacterCustoEditorAtributte : CustomEditor
{
    public CharacterCustoEditorAtributte(Type inspectedType) : base(inspectedType)
    {
    }
}
#endif
