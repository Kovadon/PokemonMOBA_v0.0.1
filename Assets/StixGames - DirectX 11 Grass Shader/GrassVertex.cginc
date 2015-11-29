#ifndef GRASS_VERTEX
#define GRASS_VERTEX

void vert(inout appdata v)
{
	#ifdef GRASS_OBJECT_MODE
		v.objectSpacePos = v.vertex.xyz;
	#endif

	v.vertex = mul(_Object2World, v.vertex);
	v.uv = TRANSFORM_TEX(v.uv, _Density);

	//Camera, or rather renderer pos
	v.cameraPos = getCameraPos();

	//LOD
	fixed dist = distance(v.vertex.xyz, v.cameraPos);
	fixed lod = max(smoothstep(_LODEnd, _LODStart, dist)*_LODMax, 1);
	
	v.lod = lod;

	#ifdef USING_DIRECTIONAL_LIGHT
		v.lightDir = normalize(_WorldSpaceLightPos0.xyz);
	#else
		v.lightDir = 0;
	#endif
}
#endif