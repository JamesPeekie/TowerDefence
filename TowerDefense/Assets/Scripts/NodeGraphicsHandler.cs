using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeGraphicsHandler : MonoBehaviour
{
    Node parentNode;

    void Start()
    {
        parentNode = GetComponentInParent<Node>();
    }

    void OnMouseDown()
    {
        parentNode.BuildTurret();
    }

    void OnMouseEnter()
    {
        parentNode.HoverNode();
    }

    void OnMouseExit()
    {
        parentNode.UnHoverNode();
    }
}