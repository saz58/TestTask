using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int Id; //{ get; private set; }
    public bool IsOpen => !IsLearned; //&& _previousNode.IsOpened;
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
        _button.onClick.AddListener(Learn);
    }

    public bool IsNodeOpened()
    {
        foreach(var previousNode in _previousNodes)
        {
            if(!previousNode.IsLearned)
                return false;
        }

        return true;
    }

    public void Learn()
    {
        if (IsNodeOpened())
        {
            IsLearned = true;
            UpdateNodeUI();
        }
    }

    public void Unleard()
    {
        IsLearned = false;
    }

    public void UpdateNodeUI()
    {
        var buttonImage = _button.image;

        if (IsLearned)
        {
            buttonImage.color = _learnedSkillColor;
        }
        else
        {
            if (IsNodeOpened())
            {
                buttonImage.color = _openedSkillColor;
            }
            else
            {
                buttonImage.color = _unopenedSkillColor;
            }
        }

        foreach(var nextNode in _nextNodes)
        {
            nextNode.UpdateNodeUI();
        }

    }
}