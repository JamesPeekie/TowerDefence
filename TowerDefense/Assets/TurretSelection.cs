using UnityEngine;

public class TurretNodeSelection : MonoBehaviour {

    private Node target;

    public void SetNodeTarget (Node selectedNode)
    {
        target = selectedNode;

        transform.position = target.GetBuildPosition();
    }

}
