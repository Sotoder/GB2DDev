using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(CustomButton))]
public class CustomButtonEditor : ButtonEditor
{  
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        var transition = new PropertyField(serializedObject.FindProperty(CustomButton.TransitionFieldName));
        var easing = new PropertyField(serializedObject.FindProperty(CustomButton.EasingFieldName));
        var duration = new PropertyField(serializedObject.FindProperty(CustomButton.DurationFieldName));
        var power = new PropertyField(serializedObject.FindProperty(CustomButton.PowerFieldName));
        var label = new Label("Tween settings");
        var button = new Button(StopTween);
        button.text = "Stop Tween";

        root.Add(new IMGUIContainer(OnInspectorGUI));
        root.Add(label);
        root.Add(transition);
        root.Add(easing);
        root.Add(duration);
        root.Add(power);
        root.Add(button);


        return root;
    }

    private void StopTween()
    {
        CustomButton button = (CustomButton)target;
        button.ActiveTween.Complete(true);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        serializedObject.ApplyModifiedProperties();
    }
}