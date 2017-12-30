using UnityEngine;

public class TurretSelection : MonoBehaviour
{
    private Node target;

    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void SetNodeTarget(Node selectedNode)
    {
        target = selectedNode;

        transform.position = target.GetBuildPosition();
        anim.Play("TurretUiSpawnIn");
    }
}