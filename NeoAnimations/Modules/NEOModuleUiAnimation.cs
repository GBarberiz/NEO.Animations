// Author: Gabriel Barberiz
// Created: 2024/09/20

using UnityEngine;
using DG.Tweening; //Required for Animations

namespace NEO.Animations
{
    public static class NEOModuleUiAnimation
    {
        #region EntranceAnimations
        ///<summary> 
        /// Creates a bounce-in animation for a RectTransform using DOTween, applying a fade-in effect with CanvasGroup and a scale transformation.
        ///</summary>
        ///<param name = "duration" > The duration of the animation.</param>
        public static void NEOBounceIn(this RectTransform rectTransform,float duration)
        {
            if (rectTransform == null) return;  // Ensure the object exists before starting the animation

            CanvasGroup canvasGroup = CheckCanvasGroup(rectTransform);
            Vector2 scale = rectTransform.localScale;
            Sequence sequence = DOTween.Sequence();

            sequence
            // Fade in the CanvasGroup alpha over half the duration
            .Join(DOVirtual.Float(0, 1, duration * 0.5f, (value) => canvasGroup.alpha = value))
            // Scale the RectTransform using an OutElastic easing
            .Join(rectTransform.DOScale(scale, duration).SetEase(Ease.OutElastic,1.7f,0.25f))
            // OnStart: Shrink the scale of the RectTransform by 20% at the beginning of the animation
            .OnStart(() => 
            { 
                rectTransform.localScale *= 0.8f; 
            })
            // OnUpdate: Continuously check if the RectTransform or CanvasGroup is destroyed during the animation
            .OnUpdate(() =>
            {
                if (rectTransform == null || canvasGroup == null)
                {
                    sequence.Kill();
                }
            })
            //OnComplete: Ensure CanvasGroup stays enabled after animation finishes
            .OnComplete(() =>
            {
                canvasGroup.enabled = true;
            });
        }
        ///<summary> 
        /// Creates a fade-in animation for a RectTransform using DOTween, applying a gradual increase in opacity with CanvasGroup.
        ///</summary>
        ///<param name = "duration" > The duration of the animation.</param>
        public static void NEOFadeIn(this RectTransform rectTransform, float duration)
        {
            if (rectTransform == null) return;  // Ensure the object exists before starting the animation

            CanvasGroup canvasGroup = CheckCanvasGroup(rectTransform);
            Sequence sequence = DOTween.Sequence();

            sequence
            // Fade in the CanvasGroup alpha over the duration
            .Join(DOVirtual.Float(0, 1, duration, (value) => canvasGroup.alpha = value))
            // OnUpdate: Continuously check if the RectTransform or CanvasGroup is destroyed during the animation
            .OnUpdate(() =>
            {
                if (rectTransform == null || canvasGroup == null)
                {
                    sequence.Kill();
                }
            })
            //OnComplete: Ensure CanvasGroup stays enabled after animation finishes
            .OnComplete(() =>
            {
                canvasGroup.enabled = true;
            });
        }
        ///<summary> 
        /// Creates a slide-down animation for a RectTransform using DOTween, starting from a position slightly above the initial position.
        ///</summary>
        ///<param name = "duration" > The duration of the animation.</param>
        public static void NEOSlideDownIn(this RectTransform rectTransform, float duration)
        {
            float rectHeight = rectTransform.rect.height * rectTransform.localScale.y;
            SlideIn(rectTransform, duration, new Vector2(rectTransform.anchoredPosition.x, rectHeight * 0.5f));
        }
        ///<summary> 
        /// Creates a big slide-down animation for a RectTransform using DOTween, starting from a position completely above the screem.
        ///</summary> 
        ///<param name="duration">The duration of the slide-down animation.</param>
        public static void NEOBigSlideDownIn(this RectTransform rectTransform, float duration)
        {
            if (rectTransform == null) return;  // Ensure the RectTransform exists before starting the animation

            Canvas canvas = rectTransform.GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("RectTransform is not inside a Canvas.");
                return;
            }

            RectTransform canvasRect = canvas.GetComponent<RectTransform>();

            float rectHeight = rectTransform.rect.height * rectTransform.localScale.y;

            // Calculate the offset to move the RectTransform completely above the canvas view
            float offset = (canvasRect.rect.height * 0.5f) + rectTransform.pivot.y * rectHeight;

            SlideIn(rectTransform, duration, new Vector2(rectTransform.anchoredPosition.x,offset));
        }
        #endregion

        #region ExitAnimations
        ///<summary> 
        /// Creates a bounce-out animation for a RectTransform using DOTween, applying a fade-out effect with CanvasGroup and a scale transformation.
        ///</summary>
        ///<param name = "duration" > The duration of the animation.</param>
        public static void NEOBounceOut(this RectTransform rectTransform, float duration)
        {
            if (rectTransform == null) return;  // Ensure the object exists before starting the animation

            Vector2 originalScale = rectTransform.localScale;
            Sequence sequence = DOTween.Sequence();
            CanvasGroup canvasGroup = CheckCanvasGroup(rectTransform);

            sequence
            // Scale the RectTransform using an InElastic easing
            // Step 1: Scale to 0.9 at 20% of the total duration
            .Append(rectTransform.DOScale(originalScale * 0.9f, duration * 0.1f).SetEase(Ease.Linear))

            // Step 2: Scale up to 1.1 and hold the opacity at 1 (50%-55% of the total duration)
            .Append(rectTransform.DOScale(originalScale * 1.2f, duration * 0.4f).SetEase(Ease.Linear))

            // Step 3: Fade out the CanvasGroup alpha from 1 to 0 and shrink the RectTransform to 0.3
            .Join(rectTransform.DOScale(originalScale * 0.3f, duration * 0.5f).SetEase(Ease.InBack))

            .Join(DOVirtual.Float(1, 0, duration * 0.5f, value => canvasGroup.alpha = value))

            // OnUpdate: Continuously check if the RectTransform or CanvasGroup is destroyed during the animation
            .OnUpdate(() =>
            {
                if (rectTransform == null)
                {
                    sequence.Kill();
                }
            })
            //OnComplete: Ensure reset local scale
            .OnComplete(() =>
            {
                canvasGroup.alpha = 0;
                rectTransform.localScale = originalScale;
            });
        }
        ///<summary> 
        /// Creates a fade-out animation for a RectTransform using DOTween, applying a gradual increase in opacity with CanvasGroup.
        ///</summary>
        ///<param name = "duration" > The duration of the animation.</param>
        public static void NEOFadeOut(this RectTransform rectTransform, float duration)
        {
            if (rectTransform == null) return;  // Ensure the object exists before starting the animation

            CanvasGroup canvasGroup = CheckCanvasGroup(rectTransform);
            Sequence sequence = DOTween.Sequence();

            sequence
            // Fade out the CanvasGroup alpha over the duration
            .Join(DOVirtual.Float(1, 0, duration, (value) => canvasGroup.alpha = value))
            // OnUpdate: Continuously check if the RectTransform or CanvasGroup is destroyed during the animation
            .OnUpdate(() =>
            {
                if (rectTransform == null || canvasGroup == null)
                {
                    sequence.Kill();
                }
            })
            //OnComplete: Ensure CanvasGroup stays enabled after animation finishes
            .OnComplete(() =>
            {
                canvasGroup.enabled = true;
            });
        }
        ///<summary> 
        /// Creates a slide-down animation for a RectTransform using DOTween, ending at a position slightly below the initial position.
        ///</summary>
        ///<param name = "duration" > The duration of the animation.</param>
        public static void NEOSlideDownOut(this RectTransform rectTransform, float duration)
        {
            float rectHeight = -1 * rectTransform.rect.height * rectTransform.localScale.y;
            SlideOut(rectTransform, duration, new Vector2(rectTransform.anchoredPosition.x, rectHeight * 0.5f));
        }
        ///<summary> 
        /// Creates a big slide-down animation for a RectTransform using DOTween, moving it far below the screen.
        ///</summary>
        ///<param name = "duration" > The duration of the animation.</param>
        public static void NEOBigSlideDownOut(this RectTransform rectTransform, float duration)
        {
            if (rectTransform == null) return;  // Ensure the RectTransform exists before starting the animation

            Canvas canvas = rectTransform.GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("RectTransform is not inside a Canvas.");
                return;
            }

            RectTransform canvasRect = canvas.GetComponent<RectTransform>();

            float rectHeight = rectTransform.rect.height * rectTransform.localScale.y;

            // Calculate the offset to move the RectTransform completely above the canvas view
            float offset = (canvasRect.rect.height * 0.5f) + rectTransform.pivot.y * rectHeight;

            SlideOut(rectTransform, duration, new Vector2(rectTransform.anchoredPosition.x, -offset));
        }
        #endregion
        #region Utils
        private static void SlideIn(RectTransform rectTransform, float duration,Vector2 startPosition)
        {
            if (rectTransform == null) return;  // Ensure the object exists before starting the animation

            CanvasGroup canvasGroup = CheckCanvasGroup(rectTransform);
            Vector2 originalPosition = rectTransform.anchoredPosition;
            Sequence sequence = DOTween.Sequence();

            sequence
            // Add the fade-in animation over
            .Join(DOVirtual.Float(0, 1, duration , value => canvasGroup.alpha = value))
            // Add the slide-down animation to move the RectTransform back to its original position
            .Join(rectTransform.DOAnchorPos(originalPosition, duration).SetEase(Ease.OutBack,1.5f))
            // OnStart: Move the RectTransform to a position above the original position
            .OnStart(() =>
            {
                rectTransform.anchoredPosition = startPosition;
            })
            // OnUpdate: Continuously check if the RectTransform or CanvasGroup is destroyed during the animation
            .OnUpdate(() =>
            {
                if (rectTransform == null || canvasGroup == null)
                {
                    sequence.Kill();
                }
            })
            //OnComplete: Ensure CanvasGroup stays enabled after animation finishes
            .OnComplete(() =>
            {
                canvasGroup.enabled = true;
            });
        }
        private static void SlideOut(RectTransform rectTransform, float duration, Vector2 endPosition)
        {
            if (rectTransform == null) return;  // Ensure the object exists before starting the animation

            CanvasGroup canvasGroup = CheckCanvasGroup(rectTransform);
            Vector2 originalPosition = rectTransform.anchoredPosition;
            Sequence sequence = DOTween.Sequence();

            sequence
            // Add the fade-in animation over
            .Join(DOVirtual.Float(1, 0, duration, value => canvasGroup.alpha = value))
            // Add the slide-down animation to move the RectTransform back to its original position
            .Join(rectTransform.DOAnchorPos(endPosition, duration).SetEase(Ease.InBack,1.5f))
            // OnUpdate: Continuously check if the RectTransform or CanvasGroup is destroyed during the animation
            .OnUpdate(() =>
            {
                if (rectTransform == null || canvasGroup == null)
                {
                    sequence.Kill();
                }
            })
            //OnComplete: Ensure CanvasGroup stays enabled after animation finishes
            .OnComplete(() =>
            {
                rectTransform.anchoredPosition = originalPosition;
                canvasGroup.enabled = true;
            });
        }
        private static CanvasGroup CheckCanvasGroup(RectTransform rectTransform)
        {
            CanvasGroup canvasGroup = rectTransform.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                // If the CanvasGroup doesn't exist, add one to control the alpha (fade) of the RectTransform
                canvasGroup = rectTransform.gameObject.AddComponent<CanvasGroup>();
                Debug.LogWarning("CanvasGroup added to " + rectTransform.name);
            }

            canvasGroup.enabled = true;  // Make sure the CanvasGroup is active
            return canvasGroup;
        }
        #endregion
    }
}
