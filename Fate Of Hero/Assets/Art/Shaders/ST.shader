Shader "Super Shader"
{
	Properties
	{
		//surface("Color Map", 2D) = "white" {}
		_MainTex("Texture", 2D) = "white" {}
		_BumpMap("Bumpmap", 2D) = "bump" {}
		_Mask("Mask", 2D) = "white" {}
	}

	Subshader
	{
		Pass
	{
		CGPROGRAM
#pragma vertex vertex_shader
#pragma fragment pixel_shader
#pragma target 3.0

	struct structure
	{
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	sampler2D _MainTex, _Mask;

	structure vertex_shader(float4 vertex:POSITION, float2 uv : TEXCOORD0)
	{
		structure vs;
		vs.vertex = UnityObjectToClipPos(vertex);
		vs.uv = uv;
		return vs;
	}

	float4 pixel_shader(structure ps) : COLOR
	{
		float2 mask_uv = ps.vertex.xy / _ScreenParams.xy;
		float4 color = tex2D(_Mask,mask_uv);
		if (color.x>0.1)
			discard;
		return tex2D(_MainTex,ps.uv.xy);
	}
		ENDCG
	}

		GrabPass{ "image" }

		Pass
	{
		CGPROGRAM
#pragma vertex vertex_shader
#pragma fragment pixel_shader
#pragma target 3.0

		sampler2D _Mask, image;

	float4 vertex_shader(float4 vertex:position) : SV_POSITION
	{
		return UnityObjectToClipPos(vertex);
	}

	float4 colorFull(float4 color, float2 uv)
	{
		float strength = 16.0;
		float x = (uv.x + 4.0) * (uv.y + 4.0) * (_Time.g * 10.0);
		float t = fmod((fmod(x, 17.0) + 1.0) * (fmod(x, 117.0) + 1.0), 0.01) - 0.005;
		float4 colorFull = float4(t,t,t,1) * strength;
		return color;
	}

	float4 pixel_shader(float4 vertex:SV_POSITION) : SV_TARGET
	{
		float2 mask_uv = vertex.xy / _ScreenParams.xy;
		float4 color = tex2D(_Mask,mask_uv);
		if (color.x>0.1)
		{
			return colorFull(tex2D(image,mask_uv),mask_uv);
		}
		else
			return tex2D(image,mask_uv);
	}

		ENDCG
	}
	}

	/*Properties{
		_MainTex("Texture", 2D) = "white" {}
		_BumpMap("Bumpmap", 2D) = "bump" {}
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};
		sampler2D _MainTex;
		sampler2D _BumpMap;
		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	}
	Fallback "Diffuse"*/
}
