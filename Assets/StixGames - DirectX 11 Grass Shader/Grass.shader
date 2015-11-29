 Shader "Stix Games/Grass" 
 {
    Properties 
	{
		_EdgeLength ("Density Falloff", Range(3,50)) = 8

		_MaxTessellation("Max Density", Range(1, 6)) = 6

		_LODStart("LOD Start", float) = 20
		_LODEnd("LOD End", float) = 100
		_LODMax("Max LOD", Range(1,5)) = 5

		_GrassFadeStart("Grass Fade Start", float) = 50
		_GrassFadeEnd("Grass Fade End", float) = 100

		_Disorder("Disorder", float) = 0.3

		_ColorMap("Color Texture (RGB), Height(A)", 2D) = "white" {}

		_Displacement("Displacement Texture (RG)", 2D) = "bump" {}

		_GrassBottomColor("Grass Bottom Color", Color) = (0.35, 0.35, 0.35, 1)

		_Density("Grass Density 1(R) 2(G) 3(B) 4(A)", 2D) = "red" {}
		_DensityValues("Grass Density Values", Vector) = (1, 1, 1, 1)

		_WindParams("Wind WaveStrength(X), WaveSpeed(Y), RippleStrength(Z), RippleSpeed(W)", Vector) = (0.3, 1.2, 0.15, 1.3)
		_WindRotation("Wind Rotation", Range(0, 6.28318530718)) = 0

		_GrassTex00	 ("Grass Texture", 2D)		= "white" {}
		_Color00	 ("Color", Color)			= (0.5, 0.7, 0.3, 1)
		_SecColor00	 ("Secondary Color", Color) = (0.2, 0.5, 0.15, 1)
		_SpecColor00 ("Specular Color", Color)	= (0.2, 0.2, 0.2, 1)
		_Smoothness00("Smoothness", Range(0,1)) = 0.5
		_Softness00	 ("Softness", Range(0,1))	= 0.5
		_Width00	 ("Width", float)			= 0.1
		_MinHeight00 ("Min Height", float)		= 0.2
		_MaxHeight00("Max Height", float)		= 1.5

		_GrassTex01	 ("Grass Texture", 2D)		= "white" {}
		_Color01	 ("Color", Color)			= (0.5, 0.7, 0.3, 1)
		_SecColor01	 ("Secondary Color", Color) = (0.2, 0.5, 0.15, 1)
		_SpecColor01 ("Specular Color", Color)	= (0.2, 0.2, 0.2, 1)
		_Smoothness01("Smoothness", Range(0,1)) = 0.5
		_Softness01	 ("Softness", Range(0,1))	= 0.5
		_Width01	 ("Width", float)			= 0.1
		_MinHeight01 ("Min Height", float)		= 0.2
		_MaxHeight01("Max Height", float)		= 1.5

		_GrassTex02	 ("Grass Texture", 2D)		= "white" {}
		_Color02	 ("Color", Color)			= (0.5, 0.7, 0.3, 1)
		_SecColor02	 ("Secondary Color", Color) = (0.2, 0.5, 0.15, 1)
		_SpecColor02 ("Specular Color", Color)	= (0.2, 0.2, 0.2, 1)
		_Smoothness02("Smoothness", Range(0,1)) = 0.5
		_Softness02	 ("Softness", Range(0,1))	= 0.5
		_Width02	 ("Width", float)			= 0.1
		_MinHeight02 ("Min Height", float)		= 0.2
		_MaxHeight02("Max Height", float)		= 1.5

		_GrassTex03	 ("Grass Texture", 2D)		= "white" {}
		_Color03	 ("Color", Color)			= (0.5, 0.7, 0.3, 1)
		_SecColor03	 ("Secondary Color", Color) = (0.2, 0.5, 0.15, 1)
		_SpecColor03 ("Specular Color", Color)	= (0.2, 0.2, 0.2, 1)
		_Smoothness03("Smoothness", Range(0,1)) = 0.5
		_Softness03	 ("Softness", Range(0,1))	= 0.5
		_Width03	 ("Width", float)			= 0.1
		_MinHeight03 ("Min Height", float)		= 0.2
		_MaxHeight03("Max Height", float)		= 1.5

		//_GrassRenderTextureArea("RenderTextureArea", Vector) = (0,0,0,0)
    }

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 1000

		Pass 
		{
			Name "FORWARD"
			Tags {"LightMode" = "ForwardBase"}
			ColorMask RGB
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma hull hull
			#pragma domain domain
			#pragma geometry geom
			#pragma fragment frag
			#pragma target 5.0
			#pragma multi_compile_fog
			#pragma multi_compile_fwdbase

			// ================= Shader_feature block start =================
			#pragma shader_feature SIMPLE_GRASS SIMPLE_GRASS_DENSITY ONE_GRASS_TYPE TWO_GRASS_TYPES THREE_GRASS_TYPES FOUR_GRASS_TYPES
			#pragma shader_feature __ UNLIT_GRASS_LIGHTING PBR_GRASS_LIGHTING
			#pragma shader_feature __ UNIFORM_DENSITY
			#pragma shader_feature __ GRASS_HEIGHT_SMOOTHING
			#pragma shader_feature __ GRASS_WIDTH_SMOOTHING
			#pragma shader_feature __ GRASS_OBJECT_MODE
			#pragma multi_compile  __ GRASS_RENDERTEXTURE_DISPLACEMENT
			// ================= Shader_feature block end  =================

			#define UNITY_PASS_FORWARDBASE
			#include "UnityCG.cginc"
			#include "Tessellation.cginc"
			#include "UnityPBSLighting.cginc"
			#include "AutoLight.cginc"
			#include "UnityShaderVariables.cginc"

			#include "GrassDefinitionsAndFunctions.cginc"

			#include "GrassVertex.cginc"
			#include "GrassTessellation.cginc"
			#include "GrassGeom.cginc"
			#include "GrassFrag.cginc"

			ENDCG
		}

		Pass 
		{
			Name "FORWARD"
			Tags {"LightMode" = "ForwardAdd"}
			ZWrite Off Blend One One
			ColorMask RGB
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma hull hull
			#pragma domain domain
			#pragma geometry geom
			#pragma fragment frag
			#pragma target 5.0
			#pragma multi_compile_fog
			#pragma multi_compile_fwdadd_fullshadows

			// ================= Shader_feature block start =================
			#pragma shader_feature SIMPLE_GRASS SIMPLE_GRASS_DENSITY ONE_GRASS_TYPE TWO_GRASS_TYPES THREE_GRASS_TYPES FOUR_GRASS_TYPES
			#pragma shader_feature __ UNLIT_GRASS_LIGHTING PBR_GRASS_LIGHTING
			#pragma shader_feature __ UNIFORM_DENSITY
			#pragma shader_feature __ GRASS_HEIGHT_SMOOTHING
			#pragma shader_feature __ GRASS_WIDTH_SMOOTHING
			#pragma shader_feature __ GRASS_OBJECT_MODE
			#pragma multi_compile  __ GRASS_RENDERTEXTURE_DISPLACEMENT
			// ================= Shader_feature block end  =================

			#define UNITY_PASS_FORWARDADD
			#include "UnityCG.cginc"
			#include "Tessellation.cginc"
			#include "UnityPBSLighting.cginc"
			#include "AutoLight.cginc"
			#include "UnityShaderVariables.cginc"

			#include "GrassDefinitionsAndFunctions.cginc"

			#include "GrassVertex.cginc"
			#include "GrassTessellation.cginc"
			#include "GrassGeom.cginc"
			#include "GrassFrag.cginc"

			ENDCG
		}

		Pass 
		{
			Name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }
			ZWrite On ZTest LEqual
			Cull Off
			Offset 1, 0

			CGPROGRAM
			#pragma vertex vert
			#pragma hull hull
			#pragma domain domain
			#pragma geometry geom
			#pragma fragment frag
			#pragma target 5.0
			#pragma multi_compile_shadowcaster

			// ================= Shader_feature block start =================
			#pragma shader_feature SIMPLE_GRASS SIMPLE_GRASS_DENSITY ONE_GRASS_TYPE TWO_GRASS_TYPES THREE_GRASS_TYPES FOUR_GRASS_TYPES
			#pragma shader_feature __ UNLIT_GRASS_LIGHTING PBR_GRASS_LIGHTING
			#pragma shader_feature __ UNIFORM_DENSITY
			#pragma shader_feature __ GRASS_HEIGHT_SMOOTHING
			#pragma shader_feature __ GRASS_WIDTH_SMOOTHING
			#pragma shader_feature __ GRASS_OBJECT_MODE
			#pragma multi_compile  __ GRASS_RENDERTEXTURE_DISPLACEMENT
			// ================= Shader_feature block end  =================

			#define UNITY_PASS_SHADOWCASTER
			
			#include "UnityCG.cginc"
			#include "Tessellation.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			#include "AutoLight.cginc"
			#include "UnityShaderVariables.cginc"

			#include "GrassDefinitionsAndFunctions.cginc"

			#include "GrassVertex.cginc"
			#include "GrassTessellation.cginc"
			#include "GrassGeom.cginc"
			#include "GrassFrag.cginc"
			ENDCG
		}
	}

	CustomEditor "GrassEditor"
}