using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;

public static class NodeHandler
{
    public static bool CanNodeBeLearned(Node node)
    {
        if (node.IsLearned)
            return false;

        if (node.PreviousNodes.Length == 0)
            return true;

        foreach (var previousNode in node.PreviousNodes)
        {
            if (previousNode.IsLearned)
                return true;
        }

        return false;
    }

    public static bool CanNodeBeUnlearned(Node node)
    {
        if (!node.IsLearned)
            return false;

        if (node.PreviousNodes.Length == 0)
            return false;

        if (node.NextNodes.Length == 0)
            return true;

        bool isAnyNextNodeLearned = false;
        bool canReachBaseNode = false;

        foreach (var nextNode in node.NextNodes)
        {
            if (nextNode.IsLearned)
            {
                isAnyNextNodeLearned = true;
                canReachBaseNode |= CanReachBaseNode(nextNode, node);
            }
        }

        if (isAnyNextNodeLearned)
            return canReachBaseNode;
        else
            return true;
    }

    public static bool CanReachBaseNode(Node nodeToCheck, Node exclude = null)
    {
        if (nodeToCheck.PreviousNodes.Length == 0)
            return true;

        foreach (var previousNode in nodeToCheck.PreviousNodes)
        {
            if (previousNode == exclude)
                continue;

            if (previousNode.IsLearned)
            {
                return CanReachBaseNode(previousNode, exclude);
            }
        }

        return false;
    }

    public static void Learn(Node node)
    {
        if (CanNodeBeLearned(node))
        {
            node.SetLearned(true);
        }
    }

    public static void Unleard(Node node)
    {
        if (CanNodeBeUnlearned(node))
        {
            node.SetLearned(false);
        }
    }
}

