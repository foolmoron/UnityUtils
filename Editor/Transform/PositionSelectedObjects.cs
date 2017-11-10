using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

public class PositionSelectedObjects : ScriptableWizard {
    static Transform[] objectsToPosition = { };
    static Vector3 initialPosition = Vector3.zero;
    static Vector3 positionStep = Vector3.zero;
    static bool local = true;

    public Transform[] ObjectsToPosition = { };
    public Vector3 InitialPosition = Vector3.zero;
    public Vector3 PositionStep = Vector3.zero;
    public bool Local = true;

    [MenuItem("Transform/Position Selected Objects...")]
    static void CreateWizard() {
        DisplayWizard("Position Selected Objects", typeof(PositionSelectedObjects), "Done");
    }

    public PositionSelectedObjects() {
        InitialPosition = initialPosition;
        PositionStep = positionStep;
        Local = local;
    }

    void OnEnable() {
        ObjectsToPosition = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable).OrderBy(obj => obj.GetSiblingIndex()).ToArray();
        InitialPosition = ObjectsToPosition.Length > 0 ? ObjectsToPosition[0].localPosition : Vector3.zero;
        Undo.RecordObjects(ObjectsToPosition, "Position Selected Objects");
    }

    void OnWizardUpdate() {
        objectsToPosition = ObjectsToPosition;
        initialPosition = InitialPosition;
        positionStep = PositionStep;
        local = Local;

        // process results in update for better UX
        for (int i = 0; i < objectsToPosition.Length; i++) {
            var obj = objectsToPosition[i];
            if (local) {
                obj.localPosition = initialPosition + positionStep * i;
            } else {
                obj.position = initialPosition + positionStep * i;
            }
        }
    }

    void OnWizardCreate() {
    }
}