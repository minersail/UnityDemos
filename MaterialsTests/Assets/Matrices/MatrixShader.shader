Shader "Custom/ScreenSpaceColor"
{
	Properties
	{
		_Direction("Screen Space Direction (x, y, z)", Vector) = (1, 0, 0)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard vertex:vert fullforwardshadows
		#pragma target 3.0

		struct Input
		{
			fixed3 dirColor;
		};

		half _Glossiness;
		half _Metallic;
		fixed3 _Direction;

		void vert(inout appdata_full v, out Input o)
		{
			fixed3 localSpaceDir = mul(_Direction, (float3x3)UNITY_MATRIX_IT_MV);
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.dirColor = normalize(localSpaceDir);
		}

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = IN.dirColor;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
