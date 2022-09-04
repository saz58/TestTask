using System;
using UnityEngine;
using UnityEngine.UI;

public class NodeView : MonoBehaviour, INodeView, ISelectableView
{
    public int SendValue { get; private set; }

    [SerializeField] private Button _button;
    [SerializeField] private Text _label; // its better be TMP, i know
    [SerializeField] private GameObject _selectedMarker;

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

    public void SetSelect(bool isSelected)
    {
        _selectedMarker.SetActive(isSelected);
    }

    public void SetText(string text)
    {
        _label.text = text;
    }
}
