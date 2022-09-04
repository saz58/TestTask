using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
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
    public int SelectedId { get; private set; }
    public event Action ChangeSelectedNode;

    [SerializeField] private string _nodeTextMask = "{0}\nCost {1}";
    [SerializeField] private IdToNodeView[] _nodeViews;
    [SerializeField] private Color _openedSkillColor;
    [SerializeField] private Color _unopenedSkillColor;
    [SerializeField] private Color _learnedSkillColor;
    
    private ITree<NodeWithText> _tree;
    private ISelectableView _currentSelected;
    private Dictionary<int, NodeView> _idToNodeViewDictionary;

    public void Init(ITree<NodeWithText> tree) // looks horrible, i know
    {
        _tree = tree;

        var listOfNodeIds = tree.NodeIdToNode.Keys;

        if(_idToNodeViewDictionary == null)
        {
            InitNodeViewDictionary();
        }

        foreach(var id in tree.NodeIdToNode.Keys)
        {   
            if (_idToNodeViewDictionary.TryGetValue(id, out NodeView value))
            {
                var currentNode = tree.NodeIdToNode[id];
                value.Init(id, OnNodeClick);
                value.SetText(string.Format(_nodeTextMask, currentNode.Text, currentNode.Cost));
            }
            else
            {
                Debug.LogError($"[TreeView] Not enought INodeViews for this tree.\ntree.NodeIdToNode.Keys.Count = {tree.NodeIdToNode.Keys.Count}; _nodeViews.Length = {_nodeViews.Length}");
                continue;
            }

            
        }

        OnNodeClick(0);
    }

    private void InitNodeViewDictionary()
    {
        _idToNodeViewDictionary = new Dictionary<int, NodeView>(_nodeViews.Length);

        foreach(var nodeViewKeyValue in _nodeViews)
        {
            _idToNodeViewDictionary[nodeViewKeyValue.Id] = nodeViewKeyValue.NodeView;
        }
    }

    private void OnNodeClick(int nodeId) // ToDo: move this functional to Tree class
    {
        if(_idToNodeViewDictionary.TryGetValue(nodeId, out NodeView value))
        {
            if (_currentSelected != null)
            {
                _currentSelected.SetSelect(false);
            }

            _currentSelected = value;
            _currentSelected.SetSelect(true);
            SelectedId = nodeId;

            ChangeSelectedNode?.Invoke();
        }
        else
        {
            Debug.LogError($"[TreeView] There is no node view in dictionary with id = {nodeId}");
            return;
        }
    }

    public void UpdateState(Node node, bool forseDeepUpdate = false)
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
                if (NodeHandler.CanNodeBeLearned(nodeToUpdate))
                {
                    nodeView.SetColor(_openedSkillColor);
                    isNeedGoDeeper = true;
                }
                else
                {
                    nodeView.SetColor(_unopenedSkillColor);
                }
            }

            if (isNeedGoDeeper || forseDeepUpdate)
            {
                foreach (var nextNode in nodeToUpdate.NextNodes)
                {
                    nodesToUpdate.Enqueue(nextNode);
                }
            }
        }
    }
}
