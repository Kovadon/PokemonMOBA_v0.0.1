#ifndef GRASS_FRAG
#define GRASS_FRAG

float4x4 _World2Light;

#include "GrassSurface.cginc"

//Not really pbr lighting any more. I tried to get a similar effect as the trailers of the Wii U Zelda game.
//I used the Unity PBR as a basis and modified it to fit the rest of the shader.
half4 FakeGrassLighting(half3 diffColor, half3 specColor, half oneMinusRoughness,
						half3 normal, half3 viewDir,
						UnityLight light, UnityIndirect gi)
{
	half roughness = 1 - oneMinusRoughness;

	half3 halfDir = normalize(light.dir + viewDir);
	half diffuseLH = DotClamped(light.dir, halfDir);
	half diffuseNL = light.ndotl;
	half diffuseNV = DotClamped(normal, viewDir);

	//Mirror light dir and normal to get specular lighting on the same side as the sun/light. 
	//Just because it's not realistic, doesn't mean it doesn't look nice!
	half3 lightDir = half3(-light.dir.x, light.dir.y, -light.dir.z);
	normal = half3(-normal.x, normal.y, -normal.z);

	halfDir = normalize(lightDir + viewDir);
	half nl = DotClamped(normal, lightDir);
	half lh = DotClamped(lightDir, halfDir);
	half nv = DotClamped(normal, viewDir);
	half nh = BlinnTerm(normal, halfDir);
	

#if UNITY_BRDF_GGX
	half V = SmithGGXVisibilityTerm(diffuseNL, nv, roughness);
	half D = GGXTerm(nh, roughness);
#else
	half V = SmithBeckmannVisibilityTerm(diffuseNL, nv, roughness);
	half D = NDFBlinnPhongNormalizedTerm(nh, RoughnessToSpecPower(roughness));
#endif

	half diffuseNLPow5 = Pow5(1 - diffuseNL);
	half diffuseNVPow5 = Pow5(1 - diffuseNV);
	half Fd90 = 0.5 + 2 * diffuseLH * diffuseLH * roughness;
	half disneyDiffuse = (1 + (Fd90 - 1) * diffuseNLPow5) * (1 + (Fd90 - 1) * diffuseNVPow5);

	// HACK: theoretically we should divide by Pi diffuseTerm and not multiply specularTerm!
	// BUT 1) that will make shader look significantly darker than Legacy ones
	// and 2) on engine side "Non-important" lights have to be divided by Pi to in cases when they are injected into ambient SH
	// NOTE: multiplication by Pi is part of single constant together with 1/4 now

	half specularTerm = max(0, (V * D * nl) * unity_LightGammaCorrectionConsts_PIDiv4);// Torrance-Sparrow model, Fresnel is applied later (for optimization reasons)
	half diffuseTerm = disneyDiffuse * diffuseNL;

	//I removed the specular global illumination term. It might be a nice effect, but it looked weird on the grass.
	half3 color = diffColor * (gi.diffuse + light.color * diffuseTerm)
		+ specularTerm * light.color * FresnelTerm(specColor, lh)
		;// +gi.specular * FresnelLerp(specColor, grazingTerm, nv);

	return half4(color, 1);
}

#ifdef UNITY_PASS_FORWARDBASE
fixed4 frag(FS_INPUT i) : SV_Target
{
	float3 worldPos = i.worldPos;

	#ifdef UNITY_COMPILER_HLSL
		SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
	#else
		SurfaceOutputStandardSpecular o;
	#endif

	o.Albedo = 0.0;
	o.Normal = i.normal;
	o.Emission = 0.0;
	o.Specular = 0;
	o.Smoothness = 1;
	o.Occlusion = 1.0;
	o.Alpha = 0.0;

	surf(i, o);

	half4 c = 0;

	#if defined(UNLIT_GRASS_LIGHTING)
		c = half4(o.Albedo, 1);
	#else //Not unlit
		UNITY_LIGHT_ATTENUATION(atten, i, worldPos);

		UnityGI gi;
		UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
		gi.indirect.diffuse = 0;
		gi.indirect.specular = 0;
		#if !defined(LIGHTMAP_ON)
			gi.light.color = _LightColor0.rgb;
			gi.light.dir = i.lightDir;
			gi.light.ndotl = LambertTerm (o.Normal, gi.light.dir);
		#endif
		// Call GI (lightmaps/SH/reflections) lighting function
		UnityGIInput giInput;
		UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
		giInput.light = gi.light;
		giInput.worldPos = worldPos;
		giInput.worldViewDir = i.viewDir;
		giInput.atten = atten;
		#if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
			giInput.lightmapUV = i.lmap;
		#else
			giInput.lightmapUV = 0.0;
		#endif
		#if UNITY_SHOULD_SAMPLE_SH
			giInput.ambient = i.sh;
		#else
			giInput.ambient.rgb = 0.0;
		#endif
			giInput.probeHDR[0] = unity_SpecCube0_HDR;
			giInput.probeHDR[1] = unity_SpecCube1_HDR;
		#if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
			giInput.boxMin[0] = unity_SpecCube0_BoxMin; // .w holds lerp value for blending
		#endif
		#if UNITY_SPECCUBE_BOX_PROJECTION
			giInput.boxMax[0] = unity_SpecCube0_BoxMax;
			giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
			giInput.boxMax[1] = unity_SpecCube1_BoxMax;
			giInput.boxMin[1] = unity_SpecCube1_BoxMin;
			giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
		#endif
		LightingStandardSpecular_GI(o, giInput, gi);

		#if defined(PBR_GRASS_LIGHTING)
			//Fix normals for reflection, the lighting won't work, because I am faking a lot of it...
			o.Normal = i.reflectionNormal;

			// realtime lighting: call lighting function
			c += LightingStandardSpecular(o, i.viewDir, gi);
		#else //Use fake lighting
			//Taken from LightingStandardSpecular
			half oneMinusReflectivity;
			o.Albedo = EnergyConservationBetweenDiffuseAndSpecular(o.Albedo, o.Specular, /*out*/ oneMinusReflectivity);

			// shader relies on pre-multiply alpha-blend (_SrcBlend = One, _DstBlend = OneMinusSrcAlpha)
			// this is necessary to handle transparency in physically correct way - only diffuse component gets affected by alpha
			half outputAlpha;
			o.Albedo = PreMultiplyAlpha(o.Albedo, o.Alpha, oneMinusReflectivity, /*out*/ outputAlpha);

			c = FakeGrassLighting(o.Albedo, o.Specular, o.Smoothness, o.Normal, i.viewDir, gi.light, gi.indirect);
			c.rgb += UNITY_BRDF_GI(o.Albedo, o.Specular, oneMinusReflectivity, o.Smoothness, o.Normal, i.viewDir, o.Occlusion, gi);
			c.a = outputAlpha;
		#endif
	#endif //End not unlit block

	UNITY_APPLY_FOG(i.fogCoord, c); // apply fog
	UNITY_OPAQUE_ALPHA(c.a);
	return c;
}
#endif

#ifdef UNITY_PASS_FORWARDADD
fixed4 frag(FS_INPUT i) : SV_Target
{
	float3 worldPos = i.worldPos;

	#ifdef UNITY_COMPILER_HLSL
		SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
	#else
		SurfaceOutputStandardSpecular o;
	#endif

	o.Albedo = 0.0;
	o.Normal = i.normal;
	o.Emission = 0.0;
	o.Specular = 0;
	o.Smoothness = 0.5;
	o.Occlusion = 1.0;
	o.Alpha = 0.0;

	surf(i, o);

	fixed4 c = 0;

	#if !defined(UNLIT_GRASS_LIGHTING)
		UNITY_LIGHT_ATTENUATION(atten, i, worldPos)

		// Setup lighting environment
		UnityGI gi;
		UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
		gi.indirect.diffuse = 0;
		gi.indirect.specular = 0;
		#if !defined(LIGHTMAP_ON)
			gi.light.color = _LightColor0.rgb;
			gi.light.dir = i.lightDir;
			gi.light.ndotl = LambertTerm (o.Normal, gi.light.dir);
		#endif
		gi.light.color *= atten;

		c += LightingStandardSpecular(o, i.viewDir, gi);
		c.a = 0.0;

		#if defined(PBR_GRASS_LIGHTING)
			//Fix normals for reflection, the lighting won't work, because I am faking a lot of it...
			o.Normal = i.reflectionNormal;

			// realtime lighting: call lighting function
			c += LightingStandardSpecular(o, i.viewDir, gi);
		#else //Use fake lighting
			//Taken from LightingStandardSpecular
			half oneMinusReflectivity;
			o.Albedo = EnergyConservationBetweenDiffuseAndSpecular(o.Albedo, o.Specular, /*out*/ oneMinusReflectivity);

			// shader relies on pre-multiply alpha-blend (_SrcBlend = One, _DstBlend = OneMinusSrcAlpha)
			// this is necessary to handle transparency in physically correct way - only diffuse component gets affected by alpha
			half outputAlpha;
			o.Albedo = PreMultiplyAlpha(o.Albedo, o.Alpha, oneMinusReflectivity, /*out*/ outputAlpha);

			c = FakeGrassLighting(o.Albedo, o.Specular, o.Smoothness, o.Normal, i.viewDir, gi.light, gi.indirect);
			c.rgb += UNITY_BRDF_GI(o.Albedo, o.Specular, oneMinusReflectivity, o.Smoothness, o.Normal, i.viewDir, o.Occlusion, gi);
		#endif
	#endif

	c.a = 0.0;

	UNITY_APPLY_FOG(i.fogCoord, c); // apply fog
	UNITY_OPAQUE_ALPHA(c.a);
	return c;
}
#endif

#ifdef UNITY_PASS_SHADOWCASTER
fixed4 frag(FS_INPUT i) : SV_Target
{
	// prepare and unpack data
	#ifdef UNITY_COMPILER_HLSL
		SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
	#else
		SurfaceOutputStandardSpecular o;
	#endif
	fixed3 normalWorldVertex = fixed3(0, 0, 1);
	o.Albedo = 0.0;
	o.Normal = normalWorldVertex;
	o.Emission = 0.0;
	o.Specular = 0;
	o.Smoothness = 1;
	o.Occlusion = 1.0;
	o.Alpha = 0.0;

	// call surface function
	surf(i, o);

	SHADOW_CASTER_FRAGMENT(i)
}
#endif
#endif