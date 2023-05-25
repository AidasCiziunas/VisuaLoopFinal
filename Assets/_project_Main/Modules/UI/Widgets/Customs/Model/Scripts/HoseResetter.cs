using Doozy.Runtime.Reactor.Animators;
using MegaFiers;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class HoseResetter : MonoBehaviour
    {
        [SerializeField] private MegaHose _megaHose;
        [SerializeField] private Transform _hoseT;

        private Vector3 _initRotate;
        private Vector3 _initRotate1;

        private Vector3 _lPos;
        private Vector3 _lEuler;
        private Vector3 _lScale;

        private void Start()
        {
            _initRotate = _megaHose.rotate;
            _initRotate1 = _megaHose.rotate1;

            _lPos = _hoseT.localPosition;
            _lEuler = _hoseT.localEulerAngles;
            _lScale = _hoseT.localScale;
        }

        public void Reset()
        {
            _megaHose.rotate = _initRotate;
            _megaHose.rotate1 = _initRotate1;

            _hoseT.localPosition = _lPos;
            _hoseT.localEulerAngles = _lEuler;
            _hoseT.localScale = _lScale;
        }
    }
}
