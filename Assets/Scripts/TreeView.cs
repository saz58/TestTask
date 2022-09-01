using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeView : MonoBehaviour
{
    [SerializeField] private TreeDataFile _treeDataFile;
    [SerializeField] private INodeView[] _nodeViews;

    private Tree _tree;

    private void Start()
    {
        _treeDataFile.GenerateTreeFromData();
    }

    private void Init(Tree tree)
    {
        var listOfNodeIds = tree.NodeIdToNode.Keys;
        var nodeViewIndex = 0;

        foreach(var id in tree.NodeIdToNode.Keys)
        {
            _nodeViews[nodeViewIndex].Init(id, OnNodeClick);
            nodeViewIndex++;

            if(nodeViewIndex > _nodeViews.Length)
            {
                Debug.LogError($"[TreeView] Not enought INodeViews for this tree.\ntree.NodeIdToNode.Keys.Count = {tree.NodeIdToNode.Keys.Count}; _nodeViews.Length = {_nodeViews.Length}");
            }
        }
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
        }
        else
        {
            Debug.LogError($"[TreeView] There is no node in dictionary with id = {nodeId}");
        }
    }
}
