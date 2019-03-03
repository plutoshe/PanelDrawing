// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Float) = .005
		_ObjectScale("Texture Scale", Vector) = (1,1,1,1)
		_ObjectRect("Texture Rect", Vector) = (80, 80,0,0)
	}
	SubShader{
		Tags { "Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"}
		LOD 200

		// Physically based Standard lighting model, and enable shadows on all light types

		// Use shader model 3.0 target, to get nicer looking lighting



		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha



		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			uniform float3 _ObjectScale;
			uniform float2 _ObjectRect;
			uniform float4 _OutlineColor;
			uniform float _Outline;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			/*struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};*/
			struct a2v
			{
				fixed2 uv : TEXCOORD0;
				half4 vertex : POSITION;
				float4 color    : COLOR;
			};
			struct v2f
			{
				fixed2 uv : TEXCOORD0;
				half4 vertex : SV_POSITION;
				float4 color    : COLOR;
			};

			v2f vert(a2v i) {
				v2f o;
				
				o.vertex = UnityObjectToClipPos(i.vertex);
				o.uv = i.uv;

				o.color = i.color;
				return o;

				//v2f o;
				//o.pos = UnityObjectToClipPos(v.vertex);
				//o.uv = v.texcoord;
				//return o;
			}


			half4 frag(v2f i) : SV_Target
			{
				half4 color = tex2D(_MainTex, i.uv) * i.color;
				float outlineX = _Outline / (_ObjectRect.x *  _ObjectScale.x);
				float outlineY = _Outline / (_ObjectRect.y *  _ObjectScale.y);
				if (i.uv.x < outlineX || i.uv.y < outlineY || i.uv.x >= 1 - outlineX || i.uv.y >= 1 - outlineY)
					color = _OutlineColor;
				//half4 c = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));
			   
				return color;
			}
		ENDCG
		}
	}
	//FallBack "Unlit/Transparent"
}
