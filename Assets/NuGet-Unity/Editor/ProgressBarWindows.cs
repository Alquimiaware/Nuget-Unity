using UnityEditor;
using UnityEngine;

public class ProgressBarWindows : EditorWindow
{
    private bool shouldShowProgressBar;

    [MenuItem("Examples/Cancelable Progress Bar Usage")]
    static void Init()
    {
        var window = GetWindow<ProgressBarWindows>();
        window.Show();
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Display bar"))
            this.shouldShowProgressBar = true;

        if (this.shouldShowProgressBar)
        {
            if (EditorUtility.DisplayCancelableProgressBar(
                "Simple Progress Bar",
                "Shows some random info",
                0.99f))
            {
                Debug.Log("Progress bar canceled by the user");
                this.shouldShowProgressBar = false;
                EditorUtility.ClearProgressBar();
            }
        }
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }
}