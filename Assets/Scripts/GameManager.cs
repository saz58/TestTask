using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TreeView _treeView;
    [SerializeField] private TreeDataFile TreeDataFile;

    private Tree _tree;

    private void Start()
    {
        _tree = TreeDataFile.GenerateTreeFromData();
        _tree.GetNodeById(0).Learn();
        _treeView.Init(_tree);
    }


}
