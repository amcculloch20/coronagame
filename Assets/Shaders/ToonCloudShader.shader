Shader "Toon/Cloud"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}

		_Opacity("Opacity", float) = 0.5

		_EdgeThreshold("Edge Threshold", float) = 0.2
		_EdgeDarkness("Edge Darkness", float) = 0.1

		_AmbientLight("Ambient Light", float) = 0.5
	}
	SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			// Setup our pass to use Forward rendering, and only receive
			// data on the main directional light and ambient light.
			Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
				"IgnoreProjector" = "True"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// Compile multiple versions of this shader depending on lighting settings.
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			// Files below include macros and functions to assist
			// with lighting and shadows.
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				// Macro found in Autolight.cginc. Declares a vector4
				// into the TEXCOORD2 semantic with varying precision
				// depending on platform target.
				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				// Defined in Autolight.cginc. Assigns the above shadow coordinate
				// by transforming the vertex from world space to shadow-map space.
				TRANSFER_SHADOW(o)
				return o;
			}

			float4 _Color;

			float _EdgeThreshold;
			float _EdgeDarkness;

			float _Opacity;

			float _AmbientLight;

			float PI = 3.14159;

			float4 frag (v2f i) : SV_Target
			{
				float3 normal = normalize(i.worldNormal);
				float3 viewDir = normalize(i.viewDir);

				float angle = acos(smoothstep(0, 1, dot(viewDir, normal)));

				// Calculate illumination from directional light.
				// _WorldSpaceLightPos0 is a vector pointing the OPPOSITE
				// direction of the main directional light.
				float NdotL = dot(_WorldSpaceLightPos0, normal);

				// Samples the shadow map, returning a value in the 0...1 range,
				// where 0 is in the shadow, and 1 is not.
				float shadow = SHADOW_ATTENUATION(i);
				// Partition the intensity into light and dark, smoothly interpolated
				// between the two to avoid a jagged break.
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
				// Multiply by the main directional light's intensity and color.
				float4 light = (lightIntensity + _AmbientLight) * _Color;

				if (!(angle - (_EdgeThreshold) < 0)) light = _EdgeDarkness;

				float4 sample = tex2D(_MainTex, i.uv);
				float4 col = sample * _Color * light;

				col.a = _Opacity;

				return col;
			}

			ENDCG
		}

		// Shadow casting support.
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}
