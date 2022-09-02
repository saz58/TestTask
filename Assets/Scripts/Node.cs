using UnityEngine;
using UnityEngine.UI;

public class Node
{
    public int Id { get; private set; }
    public bool IsOpen => NodeOpenedChech();
    public bool IsLearned { get; private set; }
    public int Cost { get; private set; }

    public Node[] PreviousNodes { get; private set; }
    public Node[] NextNodes { get; private set; }

    //[SerializeField] private Node[] _previousNodes;
    //[SerializeField] private Node[] _nextNodes;

    //[Space]
    //[SerializeField] private Button _button;
    //[SerializeField] private Color _openedSkillColor;
    //[SerializeField] private Color _unopenedSkillColor;
    //[SerializeField] private Color _learnedSkillColor;


    public Node(int id, bool isLearned, int cost)
    {
        Id = id;
        IsLearned = isLearned;
        Cost = cost;
    }

    public void SetPreviousNodes(Node[] previousNodes)
    {
        PreviousNodes = previousNodes;
    }

    public void SetNextNodes(Node[] nextNodes)
    {
        NextNodes = nextNodes;
    }

    public bool NodeOpenedChech()
    {
        if (PreviousNodes.Length == 0)
            return true;

        foreach (var previousNode in PreviousNodes)
        {
            if(previousNode.IsLearned)
                return true;
        }

        return false;
    }

    public bool CanNodeBeUnlearned()
    {
        if (PreviousNodes.Length == 0)
            return false;

        if (NextNodes.Length == 0)
            return true;

        bool isAnyNextNodeLearned = false;
        bool canReachBaseNode = false;

        foreach (var nextNode in NextNodes)
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
        if (PreviousNodes.Length == 0)
            return true;

        foreach (var previousNode in PreviousNodes)
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

    public void Learn()
    {
        if (NodeOpenedChech())
        {
            IsLearned = true;
        }
    }

    public void Unleard()
    {
        if(CanNodeBeUnlearned())
            IsLearned = false;
    }
}