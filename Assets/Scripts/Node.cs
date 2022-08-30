using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int Id; //{ get; private set; }
    public bool IsOpen => NodeOpenedChech(); //&& _previousNode.IsOpened;
    public bool IsLearned; //{ get; private set; }
    public int Cost; //{ get; private set; }

    public int[] NextNodeIds;// { get; private set; }

    [SerializeField] private Node[] _previousNodes;
    [SerializeField] private Node[] _nextNodes;

    [Space]
    [SerializeField] private Button _button;
    [SerializeField] private Color _openedSkillColor;
    [SerializeField] private Color _unopenedSkillColor;
    [SerializeField] private Color _learnedSkillColor;


    /*public Node(int Id)
    {

    }*/

    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

    public bool NodeOpenedChech()
    {
        if (_previousNodes.Length == 0)
            return true;

        foreach (var previousNode in _previousNodes)
        {
            if(previousNode.IsLearned)
                return true;
        }

        return false;
    }

    public bool CanNodeBeUnlearned()
    {
        if (_previousNodes.Length == 0)
            return false;

        if (_nextNodes.Length == 0)
            return true;

        bool isAnyNextNodeLearned = false;
        bool canReachBaseNode = false;

        foreach (var nextNode in _nextNodes)
        {
            if (nextNode.IsLearned)
            {
                isAnyNextNodeLearned = true;
                canReachBaseNode |= nextNode.CanReachBaseNode(this);
            }
        }

        if (isAnyNextNodeLearned)
            return canReachBaseNode;
        else
            return true;
    }

    public bool CanReachBaseNode(Node exclude = null)
    {
        if (_previousNodes.Length == 0)
            return true;

        foreach (var previousNode in _previousNodes)
        {
            if (previousNode == exclude)
                continue;

            if (previousNode.IsLearned)
            {
                return previousNode.CanReachBaseNode(exclude);
            }
        }

        return false;
    }

    private void OnClick()
    {
        if(!IsLearned)
        {
            Learn();
        }
        else
        {
            Unleard();
        }
    }

    public void Learn()
    {
        if (NodeOpenedChech())
        {
            IsLearned = true;
            UpdateNodeUI();
        }
    }

    public void Unleard()
    {
        if(CanNodeBeUnlearned())
        IsLearned = false;
        UpdateNodeUI();
    }

    public void UpdateNodeUI()
    {
        var buttonImage = _button.image;
        var isNeedGoDeeper = false;

        if (IsLearned)
        {
            buttonImage.color = _learnedSkillColor;
            isNeedGoDeeper = true;
        }
        else
        {
            if (NodeOpenedChech())
            {
                buttonImage.color = _openedSkillColor;
                isNeedGoDeeper = true;
            }
            else
            {
                buttonImage.color = _unopenedSkillColor;
            }
        }

        if (isNeedGoDeeper)
        {
            foreach (var nextNode in _nextNodes)
            {
                nextNode.UpdateNodeUI();
            }
        }

    }
}