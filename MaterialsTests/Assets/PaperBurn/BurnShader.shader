Shader "Custom/BurnShader" 
{	
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_BurnTex("Burn Texture", 2D) = "white" {}
		_BurnAmount("Burn Amount", Range(0, 1)) = 1
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
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _BurnTex;
			float _BurnAmount;

			float4 frag(v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);
				float colorVal = tex2D(_BurnTex, i.uv).r;
				color.a *= floor(_BurnAmount + min(0.99, colorVal));
				return color;
			}
			ENDCG
		}
	}
}
