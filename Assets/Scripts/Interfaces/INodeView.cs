using System;

public interface INodeView : IColorSetter
{
    public void Init(int sendValue, Action<int> onClickCallback);
}
