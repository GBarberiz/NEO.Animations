# NEOModuleUiAnimation

**NEOModuleUiAnimation** is a Unity extension that adds pre-built UI animations for `RectTransform` components using [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676). The module provides a variety of animations like bounce, fade, slide, and shake effects, optimized for UI elements in Unity.

## Features

- **Bounce Animations**: Apply smooth bounce-in and bounce-out effects to `RectTransform` components.
- **Fade Animations**: Control the opacity of UI elements with fade-in and fade-out animations.
- **Slide Animations**: Slide `RectTransform` elements in or out from different directions.
- **Shake Animations**: Shake elements horizontally, vertically, or randomly for attention-grabbing effects.

## Installation

1. Import the DOTween plugin from the Unity Asset Store.
2. Copy the **NEOModuleUiAnimation** script into your Unity project or Download the package [here](https://github.com/GBarberiz/NEO.Animations/blob/dev/NeoAnimations.unitypackage).
3. Ensure that your UI elements have a `RectTransform` component.

## Usage

To use the animation functions in your scripts, simply call them on any `RectTransform` object. Below are some examples of how to use the provided animations.

### Example 1: Bounce-In Animation
```csharp
RectTransform uiElement = myButton.GetComponent<RectTransform>();
uiElement.NEOBounceIn(1.0f);  // Bounce-in animation with 1 second duration
```

### Example 2: Fade-In Animation
```csharp
RectTransform uiElement = myText.GetComponent<RectTransform>();
uiElement.NEOFadeIn(0.5f);  // Fade-in animation with 0.5 second duration
```

### Example 3: Shake Animation
```csharp
RectTransform uiElement = myImage.GetComponent<RectTransform>();
uiElement.NEOShakeX(0.2f, 20f);  // Shake horizontally for 0.2 seconds with strength 20
```

## API Reference

### Bounce Animations

#### `NEOBounceIn`
```csharp
public static void NEOBounceIn(this RectTransform rectTransform, float duration)
```
Creates a bounce-in animation for a `RectTransform` using DOTween, applying a fade-in effect with `CanvasGroup` and a scale transformation.

#### `NEOBounceOut`
```csharp
public static void NEOBounceOut(this RectTransform rectTransform, float duration)
```
Creates a bounce-out animation for a `RectTransform` using DOTween, applying a fade-out effect with `CanvasGroup` and a scale transformation.

### Fade Animations

#### `NEOFadeIn`
```csharp
public static void NEOFadeIn(this RectTransform rectTransform, float duration)
```
Creates a fade-in animation for a `RectTransform` using DOTween, applying a gradual increase in opacity with `CanvasGroup`.

#### `NEOFadeOut`
```csharp
public static void NEOFadeOut(this RectTransform rectTransform, float duration)
```
Creates a fade-out animation for a `RectTransform` using DOTween, applying a gradual decrease in opacity with `CanvasGroup`.

### Slide Animations

#### `NEOSlideDownIn`
```csharp
public static void NEOSlideDownIn(this RectTransform rectTransform, float duration)
```
Slides a `RectTransform` down from a position slightly above the initial position.

#### `NEOSlideUpIn`
```csharp
public static void NEOSlideUpIn(this RectTransform rectTransform, float duration)
```
Slides a `RectTransform` up from a position slightly below the initial position.

#### `NEOSlideLeftIn`
```csharp
public static void NEOSlideLeftIn(this RectTransform rectTransform, float duration)
```
Slides a `RectTransform` in from the right side of the screen.

#### `NEOSlideRightIn`
```csharp
public static void NEOSlideRightIn(this RectTransform rectTransform, float duration)
```
Slides a `RectTransform` in from the left side of the screen.

### Shake Animations

#### `NEOShakeX`
```csharp
public static void NEOShakeX(this RectTransform rectTransform, float duration = 0.2f, float strength = 20f)
```
Creates a shake animation for a `RectTransform`, shaking it horizontally.

#### `NEOShakeY`
```csharp
public static void NEOShakeY(this RectTransform rectTransform, float duration = 0.2f, float strength = 20f)
```
Creates a shake animation for a `RectTransform`, shaking it vertically.

#### `NEORandomShake`
```csharp
public static void NEORandomShake(this RectTransform rectTransform, float duration = 0.2f, float strength = 20f)
```
Creates a shake animation for a `RectTransform`, shaking it randomly.

---

### Notes:

- Ensure DOTween is properly installed in your Unity project for the animations to work.
- You can easily extend the library by adding your own custom animations using DOTween.
