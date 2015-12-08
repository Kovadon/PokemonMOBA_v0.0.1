using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class GrassEditor : MaterialEditor
{
    private static readonly string[] grassTypeLabels =
    {
        "Simple", "Simple with density", "1 Texture", "2 Textures",
        "3 Textures", "4 Textures"
    };

    private static readonly string[] grassTypeString =
    {
        "SIMPLE_GRASS", "SIMPLE_GRASS_DENSITY", "ONE_GRASS_TYPE", "TWO_GRASS_TYPES", "THREE_GRASS_TYPES",
        "FOUR_GRASS_TYPES"
    };

    private static readonly string[] lightingModeLabels = {"Unlit", "Inverted Specular PBR", "Default PBR"};
    private static readonly string[] lightingModes = {"UNLIT_GRASS_LIGHTING", "", "PBR_GRASS_LIGHTING"};

    private static readonly string[] defaultKeywords = {"SIMPLE_GRASS", "GRASS_WIDTH_SMOOTHING"};

    public override void OnInspectorGUI()
    {
        if (!isVisible)
        {
            return;
        }

        Material targetMat = target as Material;
        string[] originalKeywords = targetMat.shaderKeywords;

        //Set default values when the material is newly created
        if (originalKeywords.Length == 0)
        {
            originalKeywords = defaultKeywords;

            targetMat.shaderKeywords = defaultKeywords;
            EditorUtility.SetDirty(targetMat);
        }

        int grassType;

        if (originalKeywords.Contains("SIMPLE_GRASS"))
        {
            grassType = 0;
        }
        else if (originalKeywords.Contains("SIMPLE_GRASS_DENSITY"))
        {
            grassType = 1;
        }
        else if (originalKeywords.Contains("ONE_GRASS_TYPE"))
        {
            grassType = 2;
        }
        else if (originalKeywords.Contains("TWO_GRASS_TYPES"))
        {
            grassType = 3;
        }
        else if (originalKeywords.Contains("THREE_GRASS_TYPES"))
        {
            grassType = 4;
        }
        else if (originalKeywords.Contains("FOUR_GRASS_TYPES"))
        {
            grassType = 5;
        }
        else
        {
            grassType = 0;
            var l = originalKeywords.ToList();
            l.Add("SIMPLE_GRASS");
            targetMat.shaderKeywords = l.ToArray();
            EditorUtility.SetDirty(targetMat);
        }

        int lightingMode = 0;
        if (originalKeywords.Contains("UNLIT_GRASS_LIGHTING"))
        {
            lightingMode = 0;
        }
        else if (originalKeywords.Contains("PBR_GRASS_LIGHTING"))
        {
            lightingMode = 2;
        }
        else //No lighting keyword, so it's the inverted specular PBR mode
        {
            lightingMode = 1;
        }

        bool uniformDensity = originalKeywords.Contains("UNIFORM_DENSITY");
        bool widthSmoothing = originalKeywords.Contains("GRASS_WIDTH_SMOOTHING");
        bool heightSmoothing = originalKeywords.Contains("GRASS_HEIGHT_SMOOTHING");
        bool objectMode = originalKeywords.Contains("GRASS_OBJECT_MODE");

        EditorGUI.BeginChangeCheck();

        grassType = EditorGUILayout.Popup("Grass type:", grassType, grassTypeLabels);
        GUILayout.Space(10);

        lightingMode = EditorGUILayout.Popup("Lighting mode:", lightingMode, lightingModeLabels);
        GUILayout.Space(10);

        uniformDensity = GUILayout.Toggle(uniformDensity, "Use density values instead of texture");
        GUILayout.Space(10);

        widthSmoothing = GUILayout.Toggle(widthSmoothing, "Smooth grass width");
        heightSmoothing = GUILayout.Toggle(heightSmoothing, "Smooth grass height");
        GUILayout.Space(20);

        objectMode = GUILayout.Toggle(objectMode, "Object space mode");
        GUILayout.Space(20);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(targetMat, "Changed grass shader keywords");

            var keywords = new List<string>();

            keywords.Add(grassTypeString[grassType]);
            keywords.Add(lightingModes[lightingMode]);

            if (uniformDensity)
            {
                keywords.Add("UNIFORM_DENSITY");
            }

            if (widthSmoothing)
            {
                keywords.Add("GRASS_WIDTH_SMOOTHING");
            }

            if (heightSmoothing)
            {
                keywords.Add("GRASS_HEIGHT_SMOOTHING");
            }

            if (objectMode)
            {
                keywords.Add("GRASS_OBJECT_MODE");
            }

            targetMat.shaderKeywords = keywords.ToArray();
            EditorUtility.SetDirty(targetMat);
        }

        serializedObject.Update();
        var theShader = serializedObject.FindProperty("m_Shader");
        if (isVisible && !theShader.hasMultipleDifferentValues && theShader.objectReferenceValue != null)
        {
            EditorGUIUtility.fieldWidth = 64;
            EditorGUI.BeginChangeCheck();

            bool grassTypesStarted = false;

            var properties = GetMaterialProperties(new Object[] {targetMat});
            foreach (var property in properties)
            {
                //Non standard settings
                if (property.name == "_MaxTessellation")
                {
                    property.floatValue = EditorGUILayout.IntSlider(property.displayName, (int) property.floatValue,
                        1, 6);
                    continue;
                }

                //Density texture
                if (property.name == "_Density")
                {
                    grassTypesStarted = true;

                    if (grassType == 0 || uniformDensity)
                    {
                        continue;
                    }
                }

                //Unity terrain density slider
                if (!uniformDensity && property.name.Contains("_DensityValues"))
                {
                    continue;
                }
                else
                {
                    if ((grassType <= 2 && property.name.Contains("01")) ||
                        (grassType <= 3 && property.name.Contains("02")) ||
                        (grassType <= 4 && property.name.Contains("03")))
                    {
                        continue;
                    }
                }

                //If it's simple grass, don't include textures
                if (grassType == 0)
                {
                    if (property.name.Contains("_GrassTex") || property.name.Contains("Density"))
                    {
                        continue;
                    }
                }

                //If the grass type is not included, don't draw its properties
                if (grassTypesStarted)
                {
                    if ((grassType == 1 && !property.name.Contains("_DensityValues") && property.name.Contains("01")) ||
                        (grassType == 2 && !property.name.Contains("_DensityValues") && property.name.Contains("02")) ||
                        (grassType == 3 && !property.name.Contains("_DensityValues") && property.name.Contains("03")) ||
                        property.name.Contains("_RenderTexture"))
                    {
                        break;
                    }
                }

                DrawProperty(property);
            }

            if (EditorGUI.EndChangeCheck())
            {
                PropertiesChanged();
            }
        }
    }

    private void DrawProperty(MaterialProperty property)
    {
        switch (property.type)
        {
            case MaterialProperty.PropType.Range: // float ranges
                RangeProperty(property, property.displayName);
                break;

            case MaterialProperty.PropType.Float: // floats
                FloatProperty(property, property.displayName);
                break;

            case MaterialProperty.PropType.Color: // colors
                ColorProperty(property, property.displayName);
                break;

            case MaterialProperty.PropType.Texture: // textures
                TextureProperty(property, property.displayName);

                GUILayout.Space(6);
                break;

            case MaterialProperty.PropType.Vector: // vectors
                VectorProperty(property, property.displayName);
                break;

            default:
                GUILayout.Label("ARGH" + property.displayName + " : " + property.type);
                break;
        }
    }
}
