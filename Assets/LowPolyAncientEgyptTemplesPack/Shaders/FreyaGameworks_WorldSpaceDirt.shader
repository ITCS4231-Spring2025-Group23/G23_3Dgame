Shader "FreyaGameworks/WorldSpaceDirt" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_DirtTex("Dirt (Alpha)", 2D) = "white" {}

	_DirtAmount("Dirt Amount",Range(0,1)) = 1.0


	_BaseScale ("Base Tiling", Vector) = (1,1,1,0)


}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 150

CGPROGRAM
#pragma surface surf Lambert 

sampler2D _MainTex;
sampler2D _DirtTex;


fixed4 _Color;
fixed3 _BaseScale;
half _DirtAmount;

struct Input {
	float2 uv_MainTex;
	float2 uv_DirtTex;

	float3 worldPos;
	float3 worldNormal;

};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 texXY = tex2D(_DirtTex, IN.worldPos.xy * _BaseScale.z);// IN.uv_MainTex);
	fixed4 texXZ = tex2D(_DirtTex, IN.worldPos.xz * _BaseScale.y);// IN.uv_MainTex);
	fixed4 texYZ = tex2D(_DirtTex, IN.worldPos.yz * _BaseScale.x);// IN.uv_MainTex);
	fixed3 mask = fixed3(
		dot (IN.worldNormal, fixed3(0,0,1)),
		dot (IN.worldNormal, fixed3(0,1,0)),
		dot (IN.worldNormal, fixed3(1,0,0)));

	fixed4 dirt =
		texXY * abs(mask.x) +
		texXZ * abs(mask.y) +
		texYZ * abs(mask.z);

	dirt.a /= (abs(mask.x) + abs(mask.y) + abs(mask.z));

	dirt.rgb = clamp(dirt.rgb, 0, 1);


	fixed4 d = tex2D(_MainTex, IN.uv_MainTex);


	fixed4 c = d * _Color;

	c.rgb = lerp(c.rgb,dirt.rgb, _DirtAmount * dirt.a);


	o.Albedo = c.rgb;
}
ENDCG
}

FallBack "Diffuse"
}
