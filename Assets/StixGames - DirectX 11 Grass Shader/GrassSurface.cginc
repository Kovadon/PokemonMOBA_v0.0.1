#ifndef GRASS_SURFACE
#define GRASS_SURFACE

void surf(FS_INPUT i, inout SurfaceOutputStandardSpecular o)
{
	fixed4 color = 0.0;

	#if defined(SIMPLE_GRASS) || defined(SIMPLE_GRASS_DENSITY)
		#ifndef SHADOWPASS
			color = i.color;
			o.Smoothness = _Smoothness00;
			o.Specular = _SpecColor00;
		#endif
	#else
		#if !defined(ONE_GRASS_TYPE)
		switch(i.texIndex)
		{
			case 0:
		#endif
				color = tex2D(_GrassTex00, i.uv);
				#ifndef SHADOWPASS
					o.Smoothness = _Smoothness00;
					o.Specular = _SpecColor00;
				#endif
		#if !defined(ONE_GRASS_TYPE)
				break;

			case 1:
				color = tex2D(_GrassTex01, i.uv);
				#ifndef SHADOWPASS
					o.Smoothness = _Smoothness01;
					o.Specular = _SpecColor01;
				#endif
				break;
		
		#if !defined(TWO_GRASS_TYPES)
			case 2:
				color = tex2D(_GrassTex02, i.uv);
				#ifndef SHADOWPASS
					o.Smoothness = _Smoothness02;
					o.Specular = _SpecColor02;
				#endif
				break;
		
		#if !defined(THREE_GRASS_TYPES)
			case 3:
				color = tex2D(_GrassTex03, i.uv);
				#ifndef SHADOWPASS
					o.Smoothness = _Smoothness03;
					o.Specular = _SpecColor03;
				#endif
				break;
		#endif
		#endif

			default:
				discard;
				break;
		}
		#endif

		#ifndef SHADOWPASS
		color *= i.color;
		#endif

		//Cuts off the texture when texture alpha is smaller than 0.1
		clip(color.a - 0.1f);
	#endif // !SIMPLE_GRASS

	o.Albedo = color.rgb;
	o.Alpha = color.a;
}

#endif