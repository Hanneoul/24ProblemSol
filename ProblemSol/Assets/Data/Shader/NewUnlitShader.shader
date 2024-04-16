Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                // Apply average filter (blur)
                float blurAmount = 0.01; // Adjust this value for desired blur intensity
                float2 texelSize = 1.0 / float2(100,100);
                fixed4 blurredColor = fixed4(0, 0, 0, 0);

              

                for (int x = -15; x <= 15; x++)
                {
                    for (int y = -15; y <= 15; y++)
                    {
                        float2 offset = float2(x, y) * texelSize * blurAmount;
                        blurredColor += tex2D(_MainTex, i.uv + offset);
                    }
                }

                col = blurredColor / 961; // Divide by number of samples (3x3 grid)

                return col;
            }
            ENDCG
        }
    }
}
