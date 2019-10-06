using System;
using DG.Tweening;
using UnityEngine;

namespace Extensions
{
    public static class SequenceExtensions
    {
        public static Sequence PauseAndDoAction(this Sequence sequence, Tween action)
        {
            return sequence
                .AppendCallback(() => Time.timeScale = 0.0f)
                .Append(action)
                .AppendCallback(() => Time.timeScale = 1.0f);
        }

    }
}