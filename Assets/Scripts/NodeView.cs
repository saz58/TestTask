using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class NodeView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _lable;

    private int _sendValue;
    private Image _buttonImage;
    private event Action<int> _buttonCallback;

    private void Awake()
    {
        _buttonImage = _button.image;
        _button.onClick.AddListener(OnButtonClick);
    }

    public void Init(int sendValue, Action<int> onClickCallback) 
    {
        _sendValue = sendValue;
        _buttonCallback = onClickCallback;
    }

    public void SetCollor(Color color)
    {
        if (_buttonImage == null)
            return;

        _buttonImage.color = color;
    }

    private void OnButtonClick()
    {
        _buttonCallback?.Invoke(_sendValue);
    }
    
}
