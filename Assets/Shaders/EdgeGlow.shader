Shader "Custom/EdgeGlow" {
	Properties {
		_Color ("Color", Color) = (0,0,0,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		[HDR]_GlowColor("Glow Color", color) = (1,1,1,1)
		[PowerSlider(4)]_GlowAmmount("Glow Ammount", Range(.1,6)) = 1
		[HDR] _Emission ("Emission Color", color) = (0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Transparent" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;

		half _Glossiness;
		half _Metallic;
		half3 _Emission;
		float3 _GlowColor;
		float _GlowAmmount;

		struct Input {
			float2 uv_MainTex;
			float3 worldNormal;
			float3 viewDir;
			INTERNAL_DATA
		};


		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			c *= _Color;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			//o.Alpha = c.a;

			//Edge Glow
			float glow = dot(IN.worldNormal, IN.viewDir);	//dot product from view to surface normal
			glow = saturate(1 - glow);							//invert glow to edge
			glow = pow(glow, _GlowAmmount);
			float3 glowColor = _GlowColor * glow;
			o.Emission = _Emission + glowColor;


		}
		ENDCG
	}
	FallBack "Stanard"
}
