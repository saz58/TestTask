using UnityEngine;
using UnityEngine.UI;

public class Node
{
    public int Id { get; private set; }
    public bool IsLearned { get; private set; }
    public int Cost { get; private set; }

    public Node[] PreviousNodes { get; private set; }
    public Node[] NextNodes { get; private set; }

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

    public void SetLearned(bool isLearned)
    {
        IsLearned = isLearned;
    }

}