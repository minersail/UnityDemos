Shader "Custom/ImageShader" 
{	
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_TintColor("Tint Color", Color) = (1, 1, 1, 1)
		_Tiling("Tiling", Range(1, 4)) = 1
	}

	SubShader 
	{
		Tags
		{
			"Queue" = "Transparent"
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _TintColor;
			float _Tiling;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv * _Tiling;
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv) * _TintColor;
				return color;
			}
			ENDCG
		}
	}
}
