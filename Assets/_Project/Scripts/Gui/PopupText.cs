using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Descending.Gui
{
    public class PopupText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label = null;
        [SerializeField] private float _scaleTo = 0.5f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _fadeDelay = 1f;
        [SerializeField] private float _fadeSpeed = 1f;

        private Tweener _scaleTweener = null;
        private Tweener _fadeTweener = null;
        
        public void Setup(string text, float fontSize)
        {
            _label.SetText(text);
            _label.fontSize = fontSize;
            _scaleTweener = _label.rectTransform.DOScale(_scaleTo, _duration);
            
            Destroy(gameObject, _duration);
            StartCoroutine(DelayedFade());
        }

        private IEnumerator DelayedFade()
        {
            yield return new WaitForSeconds(_fadeDelay);
            _fadeTweener = _label.DOFade(0f, _fadeSpeed);
        }

        private void OnDestroy()
        {
            _scaleTweener.Kill();
            _fadeTweener.Kill();
        }
    }
}