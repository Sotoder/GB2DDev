using System.Collections.Generic;
using UnityEngine;

public class CarView : MonoBehaviour
{
    [SerializeField] private GameObject _forwardWheel;
    [SerializeField] private GameObject _backWheel;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private int _rotateLoopCount;

    public GameObject ForwardWheel { get => _forwardWheel; }
    public GameObject BackWheel { get => _backWheel; }
    public float RotateSpeed { get => _rotateSpeed; }
    public int RotateLoopCount { get => _rotateLoopCount;}
} 

