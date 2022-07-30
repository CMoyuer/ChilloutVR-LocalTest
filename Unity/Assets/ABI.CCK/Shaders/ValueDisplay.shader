Shader "Alpha Blend Interactive/Value Display"
{
	Properties
	{
		_Value("Value", Float) = 0
		_BackgroundColor("Background Color", Color) = (0,0,0,0)
		_Cutoff( "Mask Clip Value", Float ) = 0
		_DigitSheet("Digit Sheet", 2D) = "black" {}
		DigitSigns("Digit Signs", 2D) = "black" {}
		_DigitColor("Digit Color", Color) = (0,0,0,0)
		_EmissionMultiplier("Emission Multiplier", Range( 0 , 50)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Roughness("Roughness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _BackgroundColor;
		uniform float4 _DigitColor;
		uniform sampler2D DigitSigns;
		uniform float _Value;
		uniform sampler2D _DigitSheet;
		uniform float _EmissionMultiplier;
		uniform float _Metallic;
		uniform float _Roughness;
		uniform float _Cutoff = 0;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord79 = i.uv_texcoord * float2( 5,1 );
			float temp_output_4_0_g14 = 2.0;
			float temp_output_5_0_g14 = 1.0;
			float2 appendResult7_g14 = (float2(temp_output_4_0_g14 , temp_output_5_0_g14));
			float totalFrames39_g14 = ( temp_output_4_0_g14 * temp_output_5_0_g14 );
			float2 appendResult8_g14 = (float2(totalFrames39_g14 , temp_output_5_0_g14));
			float clampResult3 = clamp( _Value , -9999.0 , 9999.0 );
			float clampResult42_g14 = clamp( 0.0 , 0.0001 , ( totalFrames39_g14 - 1.0 ) );
			float temp_output_35_0_g14 = frac( ( ( (( clampResult3 > 0.0 ) ? 0.0 :  1.0 ) + clampResult42_g14 ) / totalFrames39_g14 ) );
			float2 appendResult29_g14 = (float2(temp_output_35_0_g14 , ( 1.0 - temp_output_35_0_g14 )));
			float2 temp_output_15_0_g14 = ( ( uv_TexCoord79 / appendResult7_g14 ) + ( floor( ( appendResult8_g14 * appendResult29_g14 ) ) / appendResult7_g14 ) );
			float2 uv_TexCoord53 = i.uv_texcoord * float2( 5,1 ) + float2( -1,0 );
			float temp_output_4_0_g13 = 10.0;
			float temp_output_5_0_g13 = 1.0;
			float2 appendResult7_g13 = (float2(temp_output_4_0_g13 , temp_output_5_0_g13));
			float totalFrames39_g13 = ( temp_output_4_0_g13 * temp_output_5_0_g13 );
			float2 appendResult8_g13 = (float2(totalFrames39_g13 , temp_output_5_0_g13));
			float temp_output_4_0 = floor( abs( clampResult3 ) );
			float clampResult42_g13 = clamp( 0.0 , 0.0001 , ( totalFrames39_g13 - 1.0 ) );
			float temp_output_35_0_g13 = frac( ( ( floor( ( temp_output_4_0 / 1000.0 ) ) + clampResult42_g13 ) / totalFrames39_g13 ) );
			float2 appendResult29_g13 = (float2(temp_output_35_0_g13 , ( 1.0 - temp_output_35_0_g13 )));
			float2 temp_output_15_0_g13 = ( ( uv_TexCoord53 / appendResult7_g13 ) + ( floor( ( appendResult8_g13 * appendResult29_g13 ) ) / appendResult7_g13 ) );
			float2 uv_TexCoord39 = i.uv_texcoord * float2( 5,1 ) + float2( -2,0 );
			float temp_output_4_0_g12 = 10.0;
			float temp_output_5_0_g12 = 1.0;
			float2 appendResult7_g12 = (float2(temp_output_4_0_g12 , temp_output_5_0_g12));
			float totalFrames39_g12 = ( temp_output_4_0_g12 * temp_output_5_0_g12 );
			float2 appendResult8_g12 = (float2(totalFrames39_g12 , temp_output_5_0_g12));
			float clampResult42_g12 = clamp( 0.0 , 0.0001 , ( totalFrames39_g12 - 1.0 ) );
			float temp_output_35_0_g12 = frac( ( ( floor( ( temp_output_4_0 / 100.0 ) ) + clampResult42_g12 ) / totalFrames39_g12 ) );
			float2 appendResult29_g12 = (float2(temp_output_35_0_g12 , ( 1.0 - temp_output_35_0_g12 )));
			float2 temp_output_15_0_g12 = ( ( uv_TexCoord39 / appendResult7_g12 ) + ( floor( ( appendResult8_g12 * appendResult29_g12 ) ) / appendResult7_g12 ) );
			float2 uv_TexCoord11 = i.uv_texcoord * float2( 5,1 ) + float2( -3,0 );
			float temp_output_4_0_g10 = 10.0;
			float temp_output_5_0_g10 = 1.0;
			float2 appendResult7_g10 = (float2(temp_output_4_0_g10 , temp_output_5_0_g10));
			float totalFrames39_g10 = ( temp_output_4_0_g10 * temp_output_5_0_g10 );
			float2 appendResult8_g10 = (float2(totalFrames39_g10 , temp_output_5_0_g10));
			float clampResult42_g10 = clamp( 0.0 , 0.0001 , ( totalFrames39_g10 - 1.0 ) );
			float temp_output_35_0_g10 = frac( ( ( floor( ( temp_output_4_0 / 10.0 ) ) + clampResult42_g10 ) / totalFrames39_g10 ) );
			float2 appendResult29_g10 = (float2(temp_output_35_0_g10 , ( 1.0 - temp_output_35_0_g10 )));
			float2 temp_output_15_0_g10 = ( ( uv_TexCoord11 / appendResult7_g10 ) + ( floor( ( appendResult8_g10 * appendResult29_g10 ) ) / appendResult7_g10 ) );
			float2 uv_TexCoord8 = i.uv_texcoord * float2( 5,1 ) + float2( -4,0 );
			float temp_output_4_0_g11 = 10.0;
			float temp_output_5_0_g11 = 1.0;
			float2 appendResult7_g11 = (float2(temp_output_4_0_g11 , temp_output_5_0_g11));
			float totalFrames39_g11 = ( temp_output_4_0_g11 * temp_output_5_0_g11 );
			float2 appendResult8_g11 = (float2(totalFrames39_g11 , temp_output_5_0_g11));
			float clampResult42_g11 = clamp( 0.0 , 0.0001 , ( totalFrames39_g11 - 1.0 ) );
			float temp_output_35_0_g11 = frac( ( ( temp_output_4_0 + clampResult42_g11 ) / totalFrames39_g11 ) );
			float2 appendResult29_g11 = (float2(temp_output_35_0_g11 , ( 1.0 - temp_output_35_0_g11 )));
			float2 temp_output_15_0_g11 = ( ( uv_TexCoord8 / appendResult7_g11 ) + ( floor( ( appendResult8_g11 * appendResult29_g11 ) ) / appendResult7_g11 ) );
			float4 lerpResult23 = lerp( tex2D( _DigitSheet, temp_output_15_0_g10 ) , tex2D( _DigitSheet, temp_output_15_0_g11 ) , (( uv_TexCoord8.x < 0.0 ) ? 0.0 :  1.0 ));
			float4 lerpResult49 = lerp( tex2D( _DigitSheet, temp_output_15_0_g12 ) , lerpResult23 , (( uv_TexCoord39.x < 1.0 ) ? 0.0 :  1.0 ));
			float4 lerpResult70 = lerp( tex2D( _DigitSheet, temp_output_15_0_g13 ) , lerpResult49 , (( uv_TexCoord53.x < 1.0 ) ? 0.0 :  1.0 ));
			float4 lerpResult93 = lerp( tex2D( DigitSigns, temp_output_15_0_g14 ) , lerpResult70 , (( uv_TexCoord79.x < 1.0 ) ? 0.0 :  1.0 ));
			float temp_output_24_0 = (lerpResult93).a;
			float4 temp_output_32_0 = ( _DigitColor * temp_output_24_0 );
			float4 lerpResult101 = lerp( _BackgroundColor , temp_output_32_0 , temp_output_24_0);
			o.Albedo = lerpResult101.rgb;
			o.Emission = ( temp_output_32_0 * _EmissionMultiplier ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Roughness;
			o.Alpha = 1;
			clip( temp_output_24_0 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
}