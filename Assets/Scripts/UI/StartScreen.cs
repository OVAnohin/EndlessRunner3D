using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreen : UIScreen
{
    [SerializeField] private Button _exitButton;

    public event UnityAction PlayButtonClick;
    public event UnityAction StoreButtonClick;
    public event UnityAction ExitButtonClick;

    protected override void OnEnable()
    {
        base.OnEnable();
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected override void OnPlayButtonClick()
    {
        PlayButtonClick?.Invoke();
    }

    protected override void OnOptionButtonClick()
    {
        StoreButtonClick?.Invoke();
    }

    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }
}
