// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
Shader"ShaderToyConverter/PlanetShader"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white"{}
		_RotateSpeed("Rotate Speed", float) = 0.01
	}

	Category
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		BlendOp Max
		

		SubShader
		{
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				sampler2D _MainTex;
				fixed4 fragColor;
				float _RotateSpeed;
				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float4 screenCoord : TEXCOORD1;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.screenCoord.xy = ComputeScreenPos(o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float2 invertedUV = i.uv;
					invertedUV.y = 1 - invertedUV.y;

					float2 P = (i.uv - 0.5) * 2.5;
					float3 R = float3(P, sqrt(1.0 - dot(P, P)));
					float4 output = lerp(
						tex2D(
								_MainTex, _RotateSpeed * _Time.y + 0.3 * R.xy / sqrt(R.z)
								) * max(R.x*.01 + R.y*.01 + R.z*.6 + .2, 1),
						float4(0.0, 0.0, 0.0, 1) / dot(P, P*0.9), pow(1 - sqrt(max(1. - dot(P, P), 0.0)), 2.0)
										);
					
					return output;
				}
				ENDCG
			}
		}
	}
}

