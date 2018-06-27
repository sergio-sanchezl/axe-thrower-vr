﻿Shader "Custom/ReplaceColorShaderWithoutAlpha" {
	Properties {
		_PrimaryColor ("Primary Color", Color) = (1,1,1,1)
		_SecondaryColor ("Secondary Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Alpha("Alpha", Range(0,1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque"}
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _PrimaryColor;
		fixed4 _SecondaryColor;
		fixed _Alpha;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			// Albedo comes from a texture tinted by color
			// if(c.rgb == fixed3(0,0,0)) {
			// 	c = _SecondaryColor;
			// } else if (c.rgb == fixed3(255,255,255)) {
			// 	c = _PrimaryColor;
			// }
			if(IN.uv_MainTex.x > 0.5) {
				c = _SecondaryColor;
			} else {
				c = _PrimaryColor;
			}
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
