using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "InputConfig", menuName = "Configs/Input", order = 0)]
public class InputConfig : ScriptableObject
{
    [SerializeField] InputTypes _selectedInputType;
    [SerializeField] List<InputViewMatching> _inputViewMatchings;

    public InputTypes SelectedInputType { get => _selectedInputType; }
    public List<InputViewMatching> InputViewMatchings { get => _inputViewMatchings; }
}
