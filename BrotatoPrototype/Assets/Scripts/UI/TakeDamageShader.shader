Shader "TakeDamageShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    CGINCLUDE
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

    ENDCG

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass //0
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float4 _MainTex_ST;
            float _Radius;
            float _Feather;
            float4 _TintColor;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float2 newUV = i.uv * 2 - 1;
                float circle = length(newUV);
                float mask = 1 - smoothstep(_Radius, _Radius + _Feather, circle);
                float invert_mask = 1 - mask;

                float3 disp_color = col.rgb * mask + col.rgb * invert_mask * _TintColor;

                return fixed4(disp_color, 1);
            }
            ENDCG
        }
    }
}
