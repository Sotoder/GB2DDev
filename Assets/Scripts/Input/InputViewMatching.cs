using System;
using UnityEngine;

[Serializable]
public class InputViewMatching
{
    [SerializeField] private InputTypes _inputType;
    [SerializeField] private BaseInputView _inputView;

    public InputTypes InputType { get => _inputType; }
    public BaseInputView InputView { get => _inputView; }
}