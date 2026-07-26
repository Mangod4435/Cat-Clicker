using UnityEditor;
using UnityEngine;

namespace API
{
    public class ClearMissingMono : EditorWindow
    {
        [MenuItem("Tools/Mine/ClearMissingMono")]
        public static void ClearMono()
        {
            GameObject[] allObjs = Resources.FindObjectsOfTypeAll<GameObject>();
            int removedCount = 0;
            int objCount = 0;

            Undo.IncrementCurrentGroup();

            foreach (GameObject obj in allObjs)
            {
                if (
                    obj.hideFlags == HideFlags.NotEditable
                    || obj.hideFlags == HideFlags.HideAndDontSave
                    || EditorUtility.IsPersistent(obj)
                )
                    continue;

                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(obj);
                if (count > 0)
                {
                    Undo.RegisterCompleteObjectUndo(obj, "Remove missing mono script(s)");
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(obj);
                    removedCount += count;
                    objCount++;
                }
            }
        }
    }
}
