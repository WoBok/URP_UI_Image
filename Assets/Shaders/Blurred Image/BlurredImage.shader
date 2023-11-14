Shader "URP Shader/Blurred Image" {
    Properties {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" { }
    }

    SubShader {
        Tags { "RenderPipeline" = "UniversalPipeline" "Queue" = "Transparent" }

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            HLSLPROGRAM

            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes {
                float4 positionOS : POSITION;
                half4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct Varyings {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                half4 color : COLOR;
            };
            
            sampler2D _MainTex;
            sampler2D _BlurredImageRT;

            CBUFFER_START(UnityPerMaterial)

            CBUFFER_END

            Varyings Vertex(Attributes input) {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.color = input.color ;
                output.screenPos = ComputeScreenPos(output.positionCS);
                output.uv = input.texcoord;
                return output;
            }

            float4 Fragment(Varyings input) : SV_Target {
                float2 uv = input.screenPos.xy / input.screenPos.w;
                half4 color = tex2D(_BlurredImageRT, uv);
                half alpha = tex2D(_MainTex, input.uv).a;
                color*= input.color;
                color.a *= alpha;
                return color;
            }
            ENDHLSL
        }
    }
}