Shader "Custom/UIBlur"
{
    Properties
    {
        _Color ("Tint Color", Color) = (0, 0, 0, 0.5)
        _Size ("Blur Size", Range(0, 50)) = 15.0
        
        // UI Masking Properties
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        GrabPass { "_GrabTexture" }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float4 grabPos  : TEXCOORD1;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;
            fixed4 _Color;
            float _Size;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(v.vertex);
                // Compute grab position based on vertex position
                OUT.grabPos = ComputeGrabScreenPos(OUT.vertex);
                OUT.color = v.color * _Color;
                OUT.texcoord = v.texcoord;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 sum = half4(0,0,0,0);
                
                // High Quality Spiral Blur (Poisson Disk-like)
                #define SAMPLES 30
                float angle = 0;
                float goldenAngle = 2.39996323;

                for(int i = 0; i < SAMPLES; i++)
                {
                    // Square root function spreads the samples evenly across a circle
                    float radius = sqrt((float)i / (float)SAMPLES) * _Size;
                    angle += goldenAngle;
                    
                    float2 offset = float2(cos(angle), sin(angle)) * radius;
                    
                    float4 uv = IN.grabPos;
                    // We multiply by uv.w to scale the offset correctly in projection space
                    uv.xy += offset * _GrabTexture_TexelSize.xy * uv.w; 
                    
                    sum += tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(uv));
                }
                
                sum /= (float)SAMPLES;

                // Blend the UI's tint color over the sampled blurred background!
                // The Alpha of the image controls how strong the Tint Color is overlaid.
                return half4(lerp(sum.rgb, IN.color.rgb, IN.color.a), 1.0);
            }
            ENDCG
        }
    }
}
