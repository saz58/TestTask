using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

public class Tree<T>: ITree<T> where T : Node
{
    private T _baseNode;

    public Dictionary<int, T> NodeIdToNode { get; private set; }

    public Tree(T baseNode)
    {
        _baseNode = baseNode;
        GenerateNodesDictionary();
    }

    public T GetNodeById(int id)
    {
        if(NodeIdToNode.TryGetValue(id, out T value))
        {
            return value;
        }

        Debug.LogError($"[Tree] There is no Node with id {id}");
        return null;
    }

    private void GenerateNodesDictionary()
    {
        NodeIdToNode = new Dictionary<int, T>();
        NodeIdToNode.Add(_baseNode.Id, _baseNode);

        Queue<T> nodesToCheck = new Queue<T>();
        nodesToCheck.Enqueue(_baseNode);

        while(nodesToCheck.Count > 0)
        {
            var currentNode = nodesToCheck.Dequeue();

            foreach(var nextNode in currentNode.NextNodes)
            {
                if(!NodeIdToNode.ContainsKey(nextNode.Id))
                {
                    NodeIdToNode.Add(nextNode.Id, nextNode as T);
                    nodesToCheck.Enqueue(nextNode as T);
                }
            }
        }
    }
}
