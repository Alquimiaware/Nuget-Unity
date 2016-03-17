using UnityEngine;
using UnityEditor;

// Credits to dodo:
// http://forum.unity3d.com/threads/is-there-a-unity-editor-gui-skin-template-floating-around-somewhere.170632/
public class DummyEditor : EditorWindow
{
    [MenuItem("Assets/Clone Editor Skin")]
    static public void CloneEditorSkin()
    {
        GUISkin skin = ScriptableObject.Instantiate(
            EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector)) as GUISkin;
        AssetDatabase.CreateAsset(skin, "Assets/EditorSkinClone.guiskin");
        Selection.activeObject = skin;
    }
}
