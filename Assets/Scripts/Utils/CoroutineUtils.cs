using System;
using System.Collections;
using UnityEngine;

namespace Scripts
{
    public static class CoroutineUtils
    {
        public static IEnumerator DelayAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            
            action?.Invoke();
        }
    }
}