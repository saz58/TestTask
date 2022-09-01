using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class NodeData
{
    public string LableText => _lableText;
    public bool IsLearned => _isLearned;
    public int Cost => _cost;
    public int[] PreviousNodeIds => _previousNodeIds;
    public int[] NextNodeIds => _nextNodeIds;


    [SerializeField] private string _lableText;
    [SerializeField] private bool _isLearned;
    [SerializeField] private int _cost;
    [SerializeField] private int[] _previousNodeIds;
    [SerializeField] private int[] _nextNodeIds;
}

[CreateAssetMenu(fileName = "TreeDataFile", menuName = "ScriptableObjects/TreeDataFile")]
public class TreeDataFile : ScriptableObject
{
    [SerializeField] private NodeData[] NodeData;

    public Tree GenerateTreeFromData()
    {
        Node baseNode = null;
        int currentId = 1;

        var nodeDictionary = new Dictionary<int, Node>();

        foreach(var nodeData in NodeData)
        {
            var node = new Node(currentId, nodeData.IsLearned, nodeData.Cost);
            nodeDictionary.Add(node.Id, node);

            if (baseNode == null)
                baseNode = node;
        }

        //Generate connections
        currentId = 1;

        foreach (var nodeData in NodeData)
        {
            var currentNode = nodeDictionary[currentId];
            Node[] previousNodes = new Node[nodeData.PreviousNodeIds.Length];

            for(int i = 0; i < nodeData.PreviousNodeIds.Length; i++)
            {
                previousNodes[i] = nodeDictionary[nodeData.PreviousNodeIds[i]];
            }

            Node[] nextNodes = new Node[nodeData.NextNodeIds.Length];

            for (int i = 0; i < nodeData.NextNodeIds.Length; i++)
            {
                nextNodes[i] = nodeDictionary[nodeData.NextNodeIds[i]];
            }

            currentNode.SetPreviousNodes(previousNodes);
            currentNode.SetNextNodes(nextNodes);
        }

        Tree tree = new Tree(baseNode);
        return tree;
    }
}
