using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

public class Tree: ITree
{
    private Node _baseNode;

    public Dictionary<int, Node> NodeIdToNode { get; private set; }

    public Tree(Node baseNode)
    {
        _baseNode = baseNode;
        GenerateNodesDictionary();
    }

    public Node GetNodeById(int id)
    {
        if(NodeIdToNode.TryGetValue(id, out Node value))
        {
            return value;
        }

        Debug.LogError($"[Tree] There is no Node with id {id}");
        return null;
    }

    private void GenerateNodesDictionary()
    {
        NodeIdToNode = new Dictionary<int, Node>();
        NodeIdToNode.Add(_baseNode.Id, _baseNode);

        Queue<Node> nodesToCheck = new Queue<Node>();
        nodesToCheck.Enqueue(_baseNode);

        while(nodesToCheck.Count > 0)
        {
            var currentNode = nodesToCheck.Dequeue();

            foreach(var nextNode in currentNode.NextNodes)
            {
                if(!NodeIdToNode.ContainsKey(nextNode.Id))
                {
                    NodeIdToNode.Add(nextNode.Id, nextNode);
                    nodesToCheck.Enqueue(nextNode);
                }
            }
        }
    }
}
