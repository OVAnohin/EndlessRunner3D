﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIScreen : MonoBehaviour
{
    [SerializeField] protected Button PlayButton;
    [SerializeField] protected Button OptionButton;

    protected virtual void OnEnable()
    {
        PlayButton.onClick.AddListener(OnPlayButtonClick);
        OptionButton.onClick.AddListener(OnOptionButtonClick);
    }

    protected virtual void OnDisable()
    {
        PlayButton.onClick.RemoveListener(OnPlayButtonClick);
        OptionButton.onClick.RemoveListener(OnOptionButtonClick);
    }

    protected abstract void OnPlayButtonClick();

    protected abstract void OnOptionButtonClick();
}