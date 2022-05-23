Shader "Custom/Grass1" {
	Properties {
		_MainTex ("Grass Texture", 2D) = "white" {}
		_TimeScale ("Time Scale", Float) = 1
		_alphaValue ("alphavalue", Range(0, 1)) = 0.5
		_Color ("Color", Vector) = (1,1,1,1)
	}
	
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard fullforwardshadows
#pragma target 3.0
		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
		}
		ENDCG
	}
	Fallback "Particles/Standard Unlit"
}