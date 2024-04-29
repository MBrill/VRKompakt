using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using WVR_Log;

[CustomEditor(typeof(WaveVR_ShowIndicator))]
public class WaveVR_ShowIndicatorEditor : Editor
{
	private static string LOG_TAG = "WaveVR_ShowIndicatorEditor";

	WaveVR_ShowIndicator indicatorScript;

	private bool _buttonList = false;
	private bool _element = false;

	public override void OnInspectorGUI()
	{

		indicatorScript = (WaveVR_ShowIndicator)target;

		EditorGUILayout.LabelField("Indication feature", EditorStyles.boldLabel);
		indicatorScript.showIndicator = EditorGUILayout.Toggle("Show Indicator", indicatorScript.showIndicator);

		if (indicatorScript.showIndicator)
		{
			indicatorScript.autoLayout = EditorGUILayout.Toggle("Auto Layout", indicatorScript.autoLayout);

			if(!indicatorScript.autoLayout)
			{
				indicatorScript.showIndicatorAngle = EditorGUILayout.Slider("Show Indicator Angle", indicatorScript.showIndicatorAngle, 0.0f, 90.0f);
				indicatorScript.hideIndicatorByRoll = EditorGUILayout.Toggle("Hide Indicator By Roll", indicatorScript.hideIndicatorByRoll);
				indicatorScript.basedOnEmitter = EditorGUILayout.Toggle("Based On Emitter", indicatorScript.basedOnEmitter);
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Line customization", EditorStyles.boldLabel);
				indicatorScript.lineLength = EditorGUILayout.Slider("Line Length", indicatorScript.lineLength, 0.01f, 0.1f);
				indicatorScript.lineStartWidth = EditorGUILayout.Slider("Line Start Width", indicatorScript.lineStartWidth, 0.0001f, 0.1f);
				indicatorScript.lineEndWidth = EditorGUILayout.Slider("Line End Width", indicatorScript.lineEndWidth, 0.0001f, 0.1f);
				indicatorScript.lineColor = EditorGUILayout.ColorField("Line Color", indicatorScript.lineColor);
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Text customization", EditorStyles.boldLabel);
				indicatorScript.textCharacterSize = EditorGUILayout.Slider("Text Character Size", indicatorScript.textCharacterSize, 0.01f, 0.2f);
				indicatorScript.zhCharactarSize = EditorGUILayout.Slider("Zh Charactar Size", indicatorScript.zhCharactarSize, 0.01f, 0.2f);
				indicatorScript.textFontSize = EditorGUILayout.IntSlider("Text Font Size", indicatorScript.textFontSize, 50, 200);
				indicatorScript.textColor = EditorGUILayout.ColorField("Text Color", indicatorScript.textColor);
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Indications", EditorStyles.boldLabel);
				_buttonList = EditorGUILayout.Foldout(_buttonList, "Button Indication List");
				if (_buttonList)
				{
					var list = indicatorScript.buttonIndicationList;

					int newCount = Mathf.Max(0, EditorGUILayout.IntField("  Size", list.Count));

					while (newCount < list.Count)
						list.RemoveAt(list.Count - 1);
					while (newCount > list.Count)
						list.Add(new ButtonIndication());

					for (int i = 0; i < list.Count; i++)
					{
						_element = EditorGUILayout.Foldout(_element, "  Element " + i);
						if (_element)
						{
							indicatorScript.buttonIndicationList[i].keyType = (ButtonIndication.KeyIndicator)EditorGUILayout.EnumPopup("		Key Type", indicatorScript.buttonIndicationList[i].keyType);
							indicatorScript.buttonIndicationList[i].alignment = (ButtonIndication.Alignment)EditorGUILayout.EnumPopup("		Alignment", indicatorScript.buttonIndicationList[i].alignment);
							indicatorScript.buttonIndicationList[i].indicationOffset = EditorGUILayout.Vector3Field("		Indication offset", indicatorScript.buttonIndicationList[i].indicationOffset);
							indicatorScript.buttonIndicationList[i].useMultiLanguage = EditorGUILayout.Toggle("		Use multi-language", indicatorScript.buttonIndicationList[i].useMultiLanguage);
							indicatorScript.buttonIndicationList[i].indicationText = EditorGUILayout.TextField("		Indication text", indicatorScript.buttonIndicationList[i].indicationText);
							indicatorScript.buttonIndicationList[i].followButtonRotation = EditorGUILayout.Toggle("		Follow button rotation", indicatorScript.buttonIndicationList[i].followButtonRotation);
							EditorGUILayout.Space();
						}
					}
				}
			}

			else
			{
				indicatorScript.showIndicatorAngle = EditorGUILayout.Slider("Show Indicator Angle", indicatorScript.showIndicatorAngle, 0.0f, 90.0f);
				indicatorScript.hideIndicatorByRoll = EditorGUILayout.Toggle("Hide Indicator By Roll", indicatorScript.hideIndicatorByRoll);
				indicatorScript.basedOnEmitter = EditorGUILayout.Toggle("Based On Emitter", indicatorScript.basedOnEmitter);
				indicatorScript._displayPlane = (DisplayPlane)EditorGUILayout.EnumPopup("Display Plane", indicatorScript._displayPlane);
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Line customization", EditorStyles.boldLabel);
				indicatorScript.lineStartWidth = EditorGUILayout.Slider("Line Start Width", indicatorScript.lineStartWidth, 0.0001f, 0.1f);
				indicatorScript.lineEndWidth = EditorGUILayout.Slider("Line End Width", indicatorScript.lineEndWidth, 0.0001f, 0.1f);
				indicatorScript.lineColor = EditorGUILayout.ColorField("Line Color", indicatorScript.lineColor);
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Text customization", EditorStyles.boldLabel);
				indicatorScript.textCharacterSize = EditorGUILayout.Slider("Text Character Size", indicatorScript.textCharacterSize, 0.01f, 0.2f);
				indicatorScript.zhCharactarSize = EditorGUILayout.Slider("Zh Charactar Size", indicatorScript.zhCharactarSize, 0.01f, 0.2f);
				indicatorScript.textFontSize = EditorGUILayout.IntSlider("Text Font Size", indicatorScript.textFontSize, 50, 200);
				indicatorScript.textColor = EditorGUILayout.ColorField("Text Color", indicatorScript.textColor);
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Indications", EditorStyles.boldLabel);
				_buttonList = EditorGUILayout.Foldout(_buttonList, "Button Indication List");
				if (_buttonList)
				{
					var list = indicatorScript.autoButtonIndicationList;

					int newCount = Mathf.Max(0, EditorGUILayout.IntField("  Size", list.Count));

					while (newCount < list.Count)
						list.RemoveAt(list.Count - 1);
					while (newCount > list.Count)
						list.Add(new AutoButtonIndication());

					for (int i = 0; i < list.Count; i++)
					{
						_element = EditorGUILayout.Foldout(_element, "  Element " + i);
						if (_element)
						{
							indicatorScript.autoButtonIndicationList[i].keyType = (AutoButtonIndication.KeyIndicator)EditorGUILayout.EnumPopup("    Key Type", indicatorScript.autoButtonIndicationList[i].keyType);
							indicatorScript.autoButtonIndicationList[i].alignment = (AutoButtonIndication.Alignment)EditorGUILayout.EnumPopup("    Alignment", indicatorScript.autoButtonIndicationList[i].alignment);
							indicatorScript.autoButtonIndicationList[i].distanceBetweenButtonAndText = EditorGUILayout.Slider("    Distance Between Button And Text", indicatorScript.autoButtonIndicationList[i].distanceBetweenButtonAndText, 0.0f, 0.1f);
							indicatorScript.autoButtonIndicationList[i].distanceBetweenButtonAndLine = EditorGUILayout.Slider("    Distance Between Button And Line", indicatorScript.autoButtonIndicationList[i].distanceBetweenButtonAndLine, 0.0f, 0.1f);
							indicatorScript.autoButtonIndicationList[i].lineLengthAdjustment = EditorGUILayout.Slider("    Line Length Adjustment", indicatorScript.autoButtonIndicationList[i].lineLengthAdjustment, -0.1f, 0.1f);
							indicatorScript.autoButtonIndicationList[i].useMultiLanguage = EditorGUILayout.Toggle("    Use multi-language", indicatorScript.autoButtonIndicationList[i].useMultiLanguage);
							indicatorScript.autoButtonIndicationList[i].indicationText = EditorGUILayout.TextField("    Indication text", indicatorScript.autoButtonIndicationList[i].indicationText);
							EditorGUILayout.Space();
						}
					}
				}
			}
		}

		if (GUI.changed)
			EditorUtility.SetDirty((WaveVR_ShowIndicator)target);
	}
}

