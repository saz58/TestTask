using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class IdToNodeView
{
    public int Id => _id;
    public NodeView NodeView => _nodeView;

    [SerializeField] private int _id;
    [SerializeField] private NodeView _nodeView;
}

public class TreeView : MonoBehaviour
{
    [SerializeField] private IdToNodeView[] _nodeViews;
    [SerializeField] private Color _openedSkillColor;
    [SerializeField] private Color _unopenedSkillColor;
    [SerializeField] private Color _learnedSkillColor;
    
    private ITree _tree;

    public void Init(ITree tree)
    {
        _tree = tree;

        var listOfNodeIds = tree.NodeIdToNode.Keys;

        foreach(var id in tree.NodeIdToNode.Keys)
        {
            var nodeTulpe = _nodeViews.FirstOrDefault(x=> x.Id == id);

            if (nodeTulpe == default)
            {
                Debug.LogError($"[TreeView] Not enought INodeViews for this tree.\ntree.NodeIdToNode.Keys.Count = {tree.NodeIdToNode.Keys.Count}; _nodeViews.Length = {_nodeViews.Length}");
                continue;
            }

            nodeTulpe.NodeView.Init(id, OnNodeClick); 
        }

        OnNodeClick(0);
    }

    private void OnNodeClick(int nodeId) // ToDo: move this functional to Tree class
    {
        if (_tree.NodeIdToNode.TryGetValue(nodeId, out Node node))
        {
            if(!node.IsLearned)
            {
                node.Learn();
            }
            else
            {
                node.Unleard();
            }

            UpdateState(node);
        }
        else
        {
            Debug.LogError($"[TreeView] There is no node in dictionary with id = {nodeId}");
        }
    }

    private void UpdateState(Node node)
    {
        Queue<Node> nodesToUpdate = new Queue<Node>();
        nodesToUpdate.Enqueue(node);

        while (nodesToUpdate.Count > 0)
        {
            var nodeToUpdate = nodesToUpdate.Dequeue();
            var nodeView = _nodeViews.First(x => x.Id == nodeToUpdate.Id).NodeView;
            bool isNeedGoDeeper = false;

            if (nodeToUpdate.IsLearned)
            {
                nodeView.SetColor(_learnedSkillColor);
                isNeedGoDeeper = true;
            }
            else
            {
                if (nodeToUpdate.NodeOpenedChech())
                {
                    nodeView.SetColor(_openedSkillColor);
                    isNeedGoDeeper = true;
                }
                else
                {
                    nodeView.SetColor(_unopenedSkillColor);
                }
            }

            if (isNeedGoDeeper)
            {
                foreach (var nextNode in nodeToUpdate.NextNodes)
                {
                    nodesToUpdate.Enqueue(nextNode);
                }
            }
        }
    }
}
