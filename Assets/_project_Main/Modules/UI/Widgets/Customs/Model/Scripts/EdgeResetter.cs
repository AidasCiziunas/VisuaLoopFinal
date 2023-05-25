using Doozy.Runtime.Reactor.Animators;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class EdgeResetter : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] Transform _thisT;

        private Vector3 _initLPos;
        private Vector3 _initLEuler;
        private Vector3 _initLScale;

        private void Start()
        {
            _initLPos = _thisT.localPosition;
            _initLEuler = _thisT.localEulerAngles;
            _initLScale = _thisT.localScale;
        }

        public void Reset()
        {
            _thisT.localPosition = _initLPos;
            _thisT.localEulerAngles = _initLEuler;
            _thisT.localScale = _initLScale;
        }
    }
}
