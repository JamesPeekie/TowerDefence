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
        parentNode.HandleMouseDown();
    }

    void OnMouseEnter()
    {
        parentNode.HandleMouseEnter();
    }

    void OnMouseExit()
    {
        parentNode.HandleMouseExit();
    }
}