Shader "Stix Games/Grass Dynamics/Mesh Normal Renderer"
{
	Properties
	{
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 pos : TEXCOORD0;
				float3 normal : NORMAL;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			float _GrassDisplacementBorderArea;
			float4 _GrassRenderTextureArea;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.pos = mul(_Object2World, v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}
			
			float3 frag (v2f i) : SV_Target
			{
				//Smooth out the border of the displacement. 
				//If the displacement area too big to see the border, you should probably remove that for performance.
				float borderSmoothing = smoothstep(_GrassRenderTextureArea.x, _GrassRenderTextureArea.x + _GrassDisplacementBorderArea, i.pos.x);
				borderSmoothing *= smoothstep(_GrassRenderTextureArea.y, _GrassRenderTextureArea.y + _GrassDisplacementBorderArea, i.pos.z);

				float xBorder = _GrassRenderTextureArea.x + _GrassRenderTextureArea.z;
				borderSmoothing *= smoothstep(xBorder, xBorder - _GrassDisplacementBorderArea, i.pos.x);

				float yBorder = _GrassRenderTextureArea.y + _GrassRenderTextureArea.w;
				borderSmoothing *= smoothstep(yBorder, yBorder - _GrassDisplacementBorderArea, i.pos.z);

				return normalize(i.normal * borderSmoothing + float3(0,1,0) * (1-borderSmoothing)).xzy;
			}
			ENDCG
		}
	}
}
