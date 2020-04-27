Shader "JamieDisciplined/Toon"
{
	Properties
	{
		// defined values for colors, gloss to control the amount of refelction and rim variables
		_Color("Color", Color) = (0.1, 0.1, .1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		[HDR]
		_Ambient("Ambient Color", Color) = (0.2,0.2,0.2,1)
		[HDR]
		_SpecularColour("Specular Color", Color) = (0.9,0.9,0.9,1)
		_Gloss("Glossiness", Float) = 8
		[HDR]
		rimColor("Rim Color", Color) = (.1,.1,.1,.1)
		rimAmount("Rim Amount", Range(0, 1)) = 0.1
		rimThreshold("Rim Threshold", Range(0, 1)) = 0.1
	}
		SubShader
		{
			Pass
			{
			Tags
				{
			// Passing Light Data into shader and specifying as Directional Light
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
		}

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fwdbase

		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "AutoLight.cginc"

		struct VertexShaderInput
		{
			float4 vertex : POSITION;
			float4 uv : TEXCOORD0;
			float3 normal : NORMAL;
		};

		struct VertexShaderOutput
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float3 worldNormal : NORMAL;
			float3 viewDir : TEXCOORD1;
			SHADOW_COORDS(2)
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;

		// Vertex Shader
		VertexShaderOutput vert(VertexShaderInput vertexData)
		{
			// create instance of struct for Vertex Shader output
			// convert vertex to camera clip space
			// ensure texture is scaled to new vertex position
			// convert the Vertex normal to world space
			// store the WorldSpaceView Direction
			VertexShaderOutput output;
			output.pos = UnityObjectToClipPos(vertexData.vertex);
			output.uv = TRANSFORM_TEX(vertexData.uv, _MainTex);
			output.worldNormal = UnityObjectToWorldNormal(vertexData.normal);
			output.viewDir = WorldSpaceViewDir(vertexData.vertex);
			TRANSFER_SHADOW(output)
			return output;
		}

		//variables for defining output color of shader
		float4 _Color;
		float4 _Ambient;
		float _Gloss;
		float4 _SpecularColour;
		float4 rimColor;
		float rimAmount;
		float rimThreshold;

		// Fragment/Pixel Shader
		float4 frag(VertexShaderOutput vertexData) : SV_Target
		{
			// get the Dot product of Normal and Light Direction(NdotL)
			// use smoothstep to blend between light and dark edge of shapes(lightIntensity), 
			// keep upper bound low to maintain sharp toony edge
			// multiply light intensity by the color of the main directional light and store the result(light)
			float3 normal = normalize(vertexData.worldNormal);
			float NdotL = dot(_WorldSpaceLightPos0, normal);
			float shadow = SHADOW_ATTENUATION(vertexData);
			float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
			float4 light = lightIntensity * _LightColor0;

			// calculate HalfVector for Blinn - Phong Speculare Reflection, Normailzed vector between 
			// the viewingDirection and light source(halfVector)
			float3 viewDir = normalize(vertexData.viewDir);
			float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
			float NdotH = dot(normal, halfVector);

			// size of Specular Reflection is calculated by mutiplying NdotH and lightInensity
			// this is to ensure only lit surface receives  specular 
			// this is then multiplied by the Gloss value squared to allow smaller values to greater effect
			// smoothstep used again to create the sharp toon-like effect for the edges of the reflection
			float specularIntensity = pow(NdotH * lightIntensity, _Gloss * _Gloss);
			float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
			float4 specular = specularIntensitySmooth * _SpecularColour;

			// rim is applied to surface facing away from camera, calculated by inverting the Dot product of View direction and Normal(rimDot)
			// apply rim to surface that is recieving light(rimIntensity)
			// smoothstep used to toonify the edge and bend between rim and object
			float4 rimDot = 1 - dot(viewDir, normal);
			float rimIntensity = rimDot * pow(NdotL, rimThreshold);
			rimIntensity = smoothstep(rimAmount - 0.01, rimAmount + 0.01, rimIntensity);
			float4 rim = rimIntensity * rimColor;

			float4 sample = tex2D(_MainTex, vertexData.uv);

			return _Color * sample * (_Ambient + light + specular + rim);
		}
		ENDCG
	}
			// adding ability for shadows to be cast
			UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
		}
}