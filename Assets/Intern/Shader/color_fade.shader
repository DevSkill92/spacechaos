Shader "color_fade" {
	Properties{
		_MainTex( "Texture", 2D ) = "white" {}
		_Color( "Color", Color ) = (1,1,1,1)
		_Fade( "Fade", float ) = 1
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		Cull Off
		CGPROGRAM
#pragma surface surf Lambert alpha vertex:vert
	struct Input {
		float2 uv_MainTex;
	};
	float3 _StartPosition;
	void vert( inout appdata_full v, out Input o ) {
		UNITY_INITIALIZE_OUTPUT( Input , o );
	}
	sampler2D _MainTex;
	float4  _Color;
	float _Fade;
	
	void surf( Input IN, inout SurfaceOutput o ) {
		float4 tex = tex2D( _MainTex, IN.uv_MainTex );
		
		_Color.a = 1;
		o.Emission = tex.rgb * _Color;
		
		o.Alpha = tex.a * ( 1 +  sin( _Time * _Fade ) );
	}
	ENDCG
	}
		Fallback "Diffuse"
}