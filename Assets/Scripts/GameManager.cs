using UnityEditor.Experimental.GraphView;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TreeView _treeView;
    [SerializeField] private TreeDataFile _treeDataFile;
    [SerializeField] private TopPanelView _topPanelView; 
    
    private ITree<NodeWithText> _tree;
    private int _skillPoints = 0;

    private void Start()
    {
        _tree = _treeDataFile.GenerateTreeFromData();
        var baseNode = _tree.GetNodeById(0);
        NodeHandler.Learn(baseNode);
        _treeView.Init(_tree);
        _treeView.UpdateState(baseNode);
        AddSubscriptions();
        UpdateTopPanelButtonState();
    }

    private void AddSubscriptions()
    {
        _treeView.ChangeSelectedNode += UpdateTopPanelButtonState;
        _topPanelView.EarnButtonClickEvent += EarnSkillPoint;
        _topPanelView.LearnButtonClickEvent += OnLearnClick;
        _topPanelView.UnlearnButtonClickEvent += OnUnlearnClick;
        _topPanelView.UnlearnAllButtonClickEvent += OnUnlearnAllClick;
    }

    private void UpdateTopPanelButtonState()
    {
        var currentNode = _tree.GetNodeById(_treeView.SelectedId);

        var isLearnInteractable = NodeHandler.CanNodeBeLearned(currentNode) && _skillPoints >= currentNode.Cost;
        var isUnlearnInteractable = NodeHandler.CanNodeBeUnlearned(currentNode);

        _topPanelView.UpdateButtonsState(isLearnInteractable, isUnlearnInteractable);
    }

    private void EarnSkillPoint()
    {
        EarnSkillPoints(1);
    }

    private void EarnSkillPoints(int value)
    {
        _skillPoints += value;
        UpdateTopPanelButtonState();
        _topPanelView.UpdateSkillPoints(_skillPoints); // Better do it with interfaces, but i dont realize how to create link to updated object without SerializeField
    }

    private void SpendSkillPoints(int value)
    {
        _skillPoints -= value;
        _topPanelView.UpdateSkillPoints(_skillPoints);
    }

    private void OnLearnClick()
    {
        var node = _tree.GetNodeById(_treeView.SelectedId);
        SpendSkillPoints(node.Cost);
        NodeHandler.Learn(node);
        _treeView.UpdateState(node);
        UpdateTopPanelButtonState();
    }

    private void OnUnlearnClick()
    {
        var node = _tree.GetNodeById(_treeView.SelectedId);
        NodeHandler.Unleard(node);
        _treeView.UpdateState(node);
        EarnSkillPoints(node.Cost);
        UpdateTopPanelButtonState();
    }

    private void OnUnlearnAllClick()
    {
        var nodes = _tree.NodeIdToNode.Values;
        Node baseNode = null;
        int cashback = 0;

        foreach(var node in nodes)
        {
            if(node.PreviousNodes.Length == 0)
            {
                baseNode = node;
                continue;
            }

            if(node.IsLearned)
                cashback += node.Cost;

            node.SetLearned(false);
        }

        _treeView.UpdateState(baseNode, true);
        EarnSkillPoints(cashback);
        UpdateTopPanelButtonState();
    }
    private void DeleteSubscriptions()
    {
        if(_tree != null)
            _treeView.ChangeSelectedNode -= UpdateTopPanelButtonState;
        
        if (_topPanelView != null)
        {
            _topPanelView.EarnButtonClickEvent -= EarnSkillPoint;
            _topPanelView.LearnButtonClickEvent -= OnLearnClick;
            _topPanelView.UnlearnButtonClickEvent -= OnUnlearnClick;
            _topPanelView.UnlearnAllButtonClickEvent -= OnUnlearnAllClick;
        }
    }

    private void OnDestroy()
    {
        DeleteSubscriptions();
    }
}
