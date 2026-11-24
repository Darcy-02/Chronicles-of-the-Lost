using System.Collections.Generic;
using UnityEngine;

public class AnimationLibrary : MonoBehaviour
{
    [SerializeField] private List<AnimationData> animations = new List<AnimationData>();

    public List<AnimationData> Animations => animations;
}