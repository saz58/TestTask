using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TreeView _treeView;
    [SerializeField] private TreeDataFile _treeDataFile;
    [SerializeField] private TopPanelView _topPanelView;
    
    private Tree _tree;
    private int _skillPoints = 0;

    private void Awake()
    {
        _tree = _treeDataFile.GenerateTreeFromData();
        _tree.GetNodeById(0).Learn();
        _treeView.Init(_tree);

    }

    private void EarnSkillPoints(int value)
    {
        _skillPoints += value;
    }

    private bool SpendSkillPoints(int value)
    {
        if(_skillPoints < value)
        {
            return false;
        }

        _skillPoints -= value;
        return true;
    }

}
