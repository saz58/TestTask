public class Node
{
    public int Id { get; private set; }
    public bool IsOpen => !IsLearn; //&& _previousNode.IsOpened;
    public bool IsLearn { get; private set; }
    public int Cost { get; private set; }

    public int[] NextNodeIds { get; private set }

    private Node[] _previousNodes;
    private Node[] _nextNodes;

    public Node(int Id, )
    {

    }


    public void Learn()
    {
        IsLearn = true;
    }

    public void Unleard()
    {
        IsLearn = false;
    }
}