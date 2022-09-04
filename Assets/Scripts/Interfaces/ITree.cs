using System.Collections.Generic;

public interface ITree<T> where T: Node
{
    Dictionary<int, T> NodeIdToNode { get; }
    T GetNodeById(int id);
}

