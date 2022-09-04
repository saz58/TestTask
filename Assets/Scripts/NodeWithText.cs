public class NodeWithText : Node
{
    public string Text { get; private set; }

    public NodeWithText(int id, bool isLearned, int cost, string text) : base(id, isLearned, cost)
    {
        Text = text;
    }
}

