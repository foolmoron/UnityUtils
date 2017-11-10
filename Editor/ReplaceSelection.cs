/* From: http://wiki.unity3d.com/index.php?title=ReplaceSelection
 * 
 * This wizard will replace a selection with an object or prefab.
 * Scene objects will be cloned (destroying their prefab links).
 * Original coding by 'yesfish', nabbed from Unity Forums
 * 'keep parent' added by Dave A (also removed 'rotation' option, using localRotation
 */
using UnityEngine;
using UnityEditor;
using System.Collections;

public class ReplaceSelection : ScriptableWizard {
    static GameObject replacement = null;
    static bool keep = false;

    public GameObject ReplacementObject = null;
    public bool KeepOriginals = false;

    [MenuItem("Custom Tools/Replace Selection...")]
    static void CreateWizard() {
        DisplayWizard("Replace Selection", typeof(ReplaceSelection), "Replace");
    }

    public ReplaceSelection() {
        ReplacementObject = replacement;
        KeepOriginals = keep;
    }

    void OnWizardUpdate() {
        replacement = ReplacementObject;
        keep = KeepOriginals;
    }

    void OnWizardCreate() {
        if (replacement == null) {
            Debug.LogError("You must choose an object to replace your selection with!");
            return;
        }

        Undo.RegisterSceneUndo("Replace Selection");

        Transform[] transforms = Selection.GetTransforms(
            SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);

        foreach (Transform t in transforms) {
            GameObject g;
            PrefabType pref = PrefabUtility.GetPrefabType(replacement);

            if (pref == PrefabType.Prefab || pref == PrefabType.ModelPrefab) {
                g = (GameObject) PrefabUtility.InstantiatePrefab(replacement);
            } else {
                g = (GameObject) Instantiate(replacement);
            }

            Transform gTransform = g.transform;
            gTransform.parent = t.parent;
            g.name = replacement.name;
            gTransform.localPosition = t.localPosition;
            gTransform.localScale = t.localScale;
            gTransform.localRotation = t.localRotation;
        }

        if (!keep) {
            foreach (GameObject g in Selection.gameObjects) {
                DestroyImmediate(g);
            }
        }
    }
}