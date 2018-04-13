using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace Weather_com
{
    public static class AsyncUtil
    {
        private static AsyncBehaiver _asyncBehaiver;
        private static AsyncBehaiver asyncBehaiver
        {
            get
            {
                if (_asyncBehaiver == null || _asyncBehaiver.death)
                {
                    var go = new GameObject("AsyncBehaiver");
                    _asyncBehaiver = go.AddComponent<AsyncBehaiver>();
                }
                return _asyncBehaiver;
            }
        }
        private static Queue<IEnumerator> actionQueue = new Queue<IEnumerator>();
        private static bool isWorking;
        public static void StartCoroutine(IEnumerator action)
        {
            actionQueue.Enqueue(action);
            if(!isWorking)
            {
                asyncBehaiver.StartCoroutine(StartAsync());
            }
        }

        static IEnumerator StartAsync()
        {
            isWorking = true;
            while (actionQueue.Count > 0)
            {
                yield return actionQueue.Dequeue();
            }
            isWorking = false;
        }
    }
}
