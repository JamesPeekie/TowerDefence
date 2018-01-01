using UnityEngine;

public class TurretSelection : MonoBehaviour
{
    private Node target;

    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetNodeTarget(Node selectedNode)
    {
        gameObject.SetActive(true);
        target = selectedNode;

        transform.position = target.GetBuildPosition();
        anim.Play("TurretUiSpawnIn");
    }
}