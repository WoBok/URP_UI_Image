Shader "URP Shader/Blur Texture" {
    Properties {
        _MainTex ("Texture", 2D) = "white" { }
        _BlurOffset ("Blur Offset", Vector) = (5, 5, 0, 0)
    }

    HLSLINCLUDE

    #pragma multi_compile_fragment _ _LINEAR_TO_SRGB_CONVERSION

    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

    SAMPLER(sampler_BlitTexture);
    float4 _BlitTexture_TexelSize;
    float4 _BlurOffset;

    half4 BlurHorizontal(Varyings input) : SV_Target {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        float2 uv = input.texcoord;
        float2 uv2px = _BlitTexture_TexelSize.xy;
        half4 color;
        int sampleDiv = 10 - 1;
        float weightSum = 0;
        for (float i = 0; i < 10; i++) {
            float weight = 0.5 + (0.5 - abs(i / sampleDiv - 0.5));
            weightSum += weight;
            color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_BlitTexture, uv + float2((i / sampleDiv - 0.5) * _BlurOffset.x, 0.0) * uv2px) * weight;
        }
        color /= weightSum;
        color.a = 1;

        #ifdef _LINEAR_TO_SRGB_CONVERSION
            color = LinearToSRGB(color);
        #endif

        return color;
    }

    half4 BlurVertical(Varyings input) : SV_Target {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        float2 uv = input.texcoord;
        float2 uv2px = _BlitTexture_TexelSize.xy;
        half4 color;
        int sampleDiv = 10 - 1;
        float weightSum = 0;
        for (float i = 0; i < 10; i++) {
            float weight = 0.5 + (0.5 - abs(i / sampleDiv - 0.5));
            weightSum += weight;
            color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_BlitTexture, uv + float2(0.0, (i / sampleDiv - 0.5) * _BlurOffset.y) * uv2px) * weight;
        }
        color /= weightSum;
        color.a = 1;

        #ifdef _LINEAR_TO_SRGB_CONVERSION
            color = LinearToSRGB(color);
        #endif

        return color;
    }

    ENDHLSL

    SubShader {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        LOD 100
        ZTest Always
        ZWrite Off
        Cull Off

        Pass {
            Name "Blur Horizontal"

            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment BlurHorizontal

            ENDHLSL
        }

        Pass {
            Name "Blur Vertical"

            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment BlurVertical

            ENDHLSL
        }
    }
}
