using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace Weather
{
    public class AsyncBehaiver : MonoBehaviour
    {
        internal bool death;
        private void OnDestroy(){
            death = true;
        }

    }
}