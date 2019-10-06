Shader "Hidden/TrippyBalls"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

float pulse(float time) {
    const float pi = 3.1415;
    const float frequency = _SinTime.x; // Frequency in Hz
    return 0.5*(1+sin(2 * pi * frequency * time));
}

            fixed4 frag (v2f i) : SV_Target
            {
//                float x = frac(saturate(sin(i.uv.x * _SinTime.z) * 1.8) - 0.5); 
//                float y = sin(saturate(i.uv.y * _SinTime.x * _CosTime.y) + 1.2);  
//                                              float x = pulse(i.uv.x) * 0.3 + saturate(sin(i.uv.x));  DRUNK MODE
//                float y = pulse(i.uv.y)  * 0.3+ saturate(sin(i.uv.y)); 
                float x = pulse(i.uv.x) * 0.3 + saturate(sin(i.uv.x)); 
                float y = pulse(i.uv.y)  * 0.3+ saturate(sin(i.uv.y)); 
//                float x = saturate(sin(i.uv.x)) / (saturate(cos(i.uv.x))); 
//                float y = saturate(sin(i.uv.y)) / (saturate(cos(i.uv.y)));
//                float y = saturate(sin(i.uv.y)) / (saturate(cos(i.uv.y)));
                fixed4 col = tex2D(_MainTex, float2(x,y));// * tex2D(_MainTex, 1 / i.uv.xy) / tex2D(_MainTex, -i.uv.xy);
//                fixed4 col = tex2D(_MainTex, Distort(i.uv.xy));// * tex2D(_MainTex, 1 / i.uv.xy) / tex2D(_MainTex, -i.uv.xy);
//                fixed4 col = tex2D(_MainTex, float2(x,y) * i.uv.yx % 10);
//                fixed4 col = tex2D(_MainTex, i.uv.xy);
                // just invert the colors
//                col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
