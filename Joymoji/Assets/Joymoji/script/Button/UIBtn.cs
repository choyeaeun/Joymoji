using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonExample)), CanEditMultipleObjects]
public class UIBtn : Editor
{
    protected virtual void OnSceneGUI()
    {
        ButtonExample buttonExample = (ButtonExample)target;
    }
}
