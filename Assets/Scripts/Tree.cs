using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private Node _baseNode;

    private void Start()
    {
        _baseNode.Learn();
    }
}
