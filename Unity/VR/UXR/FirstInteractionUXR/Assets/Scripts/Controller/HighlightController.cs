//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine.InputSystem;

/// <summary>
/// Controller-Klasse f�r das Highlighting
/// </summary>
public class HighlightController : Highlighter
{
    /// <summary>
    /// Callback f�r die Action Highlight
    /// </summary>
    /// <remarks>
    /// Damit value.isPressed beim Loslassenden Wert false
    /// zur�ckgibt definieren wir die Action nicht als Button,
    /// sondern als Passthrough!
    /// </remarks>
    private void OnHighlight(InputValue value) => myMaterial.color = value.isPressed ? highlightColor : originalColor;
}
