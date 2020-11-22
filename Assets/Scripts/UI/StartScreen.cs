using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : UIScreen
{
    public event UnityAction PlayButtonClick;
    public event UnityAction StoreButtonClick;

    protected override void OnPlayButtonClick()
    {
        PlayButtonClick?.Invoke();
    }

    protected override void OnOptionButtonClick()
    {
        StoreButtonClick?.Invoke();
    }
}
