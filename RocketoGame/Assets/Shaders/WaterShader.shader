﻿Shader "Unlit/WaterShader"
{
	Properties
	{
		_DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
		_DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
		_DepthMaxDistance("Depth Maximum Distance", Float) = 1
		_SurfaceNoise("Surface Noise", 2D) = "white" {}
		_SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.777
		_FoamMaxDistance("Foam Maximum Distance", Float) = 0.4
		_FoamMinDistance("Foam Minimum Distance", Float) = 0.04
		_SurfaceNoiseScroll("Surface Noise Scroll Amount", Vector) = (0.03, 0.03, 0, 0)
		_SurfaceDistortion("Surface Distortion", 2D) = "white" {}

		_WaveSpeed("Wave Speed", Float) = 0.04
		_WaveAmplitude("Wave Amplitude", Float) = 0.04
		_WaveFrequency("Wave Frequency", Float) = 0.04

	// Control to multiply the strength of the distortion.
	_SurfaceDistortionAmount("Surface Distortion Amount", Range(0, 1)) = 0.27

	}
		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
	}
		Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		CGPROGRAM
#define SMOOTHSTEP_AA 0.01
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

			struct appdata
		{
			float4 vertex : POSITION;
			float4 uv : TEXCOORD0;
			float3 normal : NORMAL;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			float4 screenPosition : TEXCOORD2;
			float2 noiseUV : TEXCOORD0;
			float2 distortUV : TEXCOORD1;
			float3 viewNormal : NORMAL;
		};

		sampler2D _SurfaceNoise;
		float4 _SurfaceNoise_ST;
		sampler2D _SurfaceDistortion;
		float4 _SurfaceDistortion_ST;
		float _WaveSpeed;
		float _WaveAmplitude;
		float _WaveFrequency;

		float _SurfaceDistortionAmount;
		v2f vert(appdata v)
		{
			v2f o;
			o.noiseUV = TRANSFORM_TEX(v.uv, _SurfaceNoise);
			float4 tmpVertex = v.vertex;
			tmpVertex.y = sin((_Time.y * _WaveSpeed) + (tmpVertex.x * tmpVertex.z * _WaveFrequency)) * _WaveAmplitude;

			o.vertex = UnityObjectToClipPos(tmpVertex);
			
			o.screenPosition = ComputeScreenPos(o.vertex);
			o.distortUV = TRANSFORM_TEX(v.uv, _SurfaceDistortion);
			o.viewNormal = COMPUTE_VIEW_NORMAL;
			return o;
		}

		float4 _DepthGradientShallow;
		float4 _DepthGradientDeep;
		float _DepthMaxDistance;
		sampler2D _CameraDepthTexture;
		float _SurfaceNoiseCutoff;
		float _FoamMaxDistance;
		float _FoamMinDistance;
		float2 _SurfaceNoiseScroll;
		sampler2D _CameraNormalsTexture;
		float4 frag(v2f i) : SV_Target
		{
			float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
		float existingDepthLinear = LinearEyeDepth(existingDepth01);
		float depthDifference = existingDepthLinear - i.screenPosition.w;
		float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
		float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

		// Add the fragment shader, just above the current noiseUV declaration line.
		float2 distortSample = (tex2D(_SurfaceDistortion, i.distortUV).xy * 2 - 1) * _SurfaceDistortionAmount;

		float2 noiseUV = float2((i.noiseUV.x + _Time.y * _SurfaceNoiseScroll.x) + distortSample.x, (i.noiseUV.y + _Time.y * _SurfaceNoiseScroll.y) + distortSample.y);
		float surfaceNoiseSample = tex2D(_SurfaceNoise, noiseUV).r;
		float3 existingNormal = tex2Dproj(_CameraNormalsTexture, UNITY_PROJ_COORD(i.screenPosition));
		float3 normalDot = saturate(dot(existingNormal, i.viewNormal));
		float foamDistance = lerp(_FoamMaxDistance, _FoamMinDistance, normalDot);
		float foamDepthDifference01 = saturate(depthDifference / foamDistance);
		float surfaceNoiseCutoff = foamDepthDifference01 * _SurfaceNoiseCutoff;

		float surfaceNoise = smoothstep(surfaceNoiseCutoff - SMOOTHSTEP_AA, surfaceNoiseCutoff + SMOOTHSTEP_AA, surfaceNoiseSample);

		return waterColor + surfaceNoise;
		}
			ENDCG
		}
	}
}
