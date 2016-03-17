namespace Alquimiaware.NuGetUnity
{
    using UnityEngine;
    using System;
    using UnityEditor;

    public class GUILayoutEx
    {
        public static IDisposable Vertical()
        {
            EditorGUILayout.BeginVertical();
            return new ActionOnDispose(() => EditorGUILayout.EndVertical());
        }

        public static IDisposable Vertical(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(options);
            return new ActionOnDispose(() => EditorGUILayout.EndVertical());
        }

        public static IDisposable Vertical(
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(style, options);
            return new ActionOnDispose(() => EditorGUILayout.EndVertical());
        }

        public static IDisposable Horizontal()
        {
            EditorGUILayout.BeginHorizontal();
            return new ActionOnDispose(() => EditorGUILayout.EndHorizontal());
        }

        public static IDisposable Horizontal(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal(options);
            return new ActionOnDispose(() => EditorGUILayout.EndHorizontal());
        }

        public static IDisposable Horizontal(
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal(style, options);
            return new ActionOnDispose(() => EditorGUILayout.EndHorizontal());
        }

        public static IDisposable ChangeCheck(Action onChanged)
        {
            EditorGUI.BeginChangeCheck();
            return new ActionOnDispose(() =>
            {
                if (EditorGUI.EndChangeCheck())
                    onChanged();
            });
        }

        private class ActionOnDispose : IDisposable
        {
            private Action action;
            private bool isDisposed;

            public ActionOnDispose(Action action)
            {
                this.action = action;
            }

            public void Dispose()
            {
                if (this.isDisposed) return;

                this.isDisposed = true;
                this.action();
            }
        }
    }
}