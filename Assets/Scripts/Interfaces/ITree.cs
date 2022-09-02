using System.Collections.Generic;

public interface ITree
{
    Dictionary<int, Node> NodeIdToNode { get; }
    Node GetNodeById(int id);
}

