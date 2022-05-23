Shader "ErbGameArt/Particles/Add_CenterGlow" {
	Properties {
		_MainTex ("MainTex", 2D) = "white" {}
		_Noise ("Noise", 2D) = "white" {}
		_TintColor ("Color", Vector) = (0.5,0.5,0.5,1)
		_Emission ("Emission", Float) = 2
		_SpeedMainTexUVNoiseZW ("Speed MainTex U/V + Noise Z/W", Vector) = (0,0,0,0)
		[MaterialToggle] _Usedepth ("Use depth?", Float) = 0
		_Depthpower ("Depth power", Float) = 1
		[MaterialToggle] _Usecenterglow ("Use center glow?", Float) = 0
		_Mask ("Mask", 2D) = "white" {}
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
}