using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class NodeView : MonoBehaviour, INodeView
{
    public int SendValue { get; private set; }

    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _lable;

    private Image _buttonImage;
    private event Action<int> _buttonCallback;

    private void Awake()
    {
        _buttonImage = _button.image;
        _button.onClick.AddListener(OnButtonClick);
    }

    public void Init(int sendValue, Action<int> onClickCallback) 
    {
        SendValue = sendValue;
        _buttonCallback = onClickCallback;
    }

    public void SetColor(Color color)
    {
        if (_buttonImage == null)
            return;

        _buttonImage.color = color;
    }

    private void OnButtonClick()
    {
        _buttonCallback?.Invoke(SendValue);
    }
}
