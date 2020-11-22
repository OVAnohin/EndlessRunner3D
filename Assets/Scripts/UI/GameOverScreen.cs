using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverScreen : Screen
{
    public event UnityAction ReStartButtonButtonClick;
    public event UnityAction ExitMainMenuButtonButtonClick;

    protected override void OnPlayButtonClick()
    {
        ReStartButtonButtonClick?.Invoke();
    }

    protected override void OnOptionButtonClick()
    {
        ExitMainMenuButtonButtonClick?.Invoke();
    }
}

