Shader "Custom/BubbleLite"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _Mask("Mask", 2D) = "white" {}
        _Dissolve("Dissolve", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            Cull back

            CGPROGRAM
            #pragma surface surf Standard alpha 
            #pragma target 3.0

            sampler2D _Mask;
            half _Glossiness;
            half _Metallic;
            half _Dissolve;
            fixed4 _Color;


            struct Input
            {
                float2 uv_Mask;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                float x = IN.uv_Mask.x - 0.5;
                float y = IN.uv_Mask.y - 0.5;
                float dist = x * x + y * y;

                fixed4 m = tex2D(_Mask, IN.uv_Mask);
                if (dist >= 0.25 || m.r <= _Dissolve)
                //if((m.g<1.1- _Dissolve && m.g>0.9-_Dissolve))
                    discard;

                fixed4 c = _Color;
                o.Albedo = c.rgb;
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = 0.0;

                fixed4 tc;
                tc.x = IN.uv_Mask.x;
                tc.y = IN.uv_Mask.y;
                tc.z = 1.0 - dist * 2.0;
                tc.w = 1.0;
                o.Normal = UnpackNormal(tc);

                o.Emission = dist * 0.8 * _Color * tc + (0.1, 0.1, 0.1);
            }
            ENDCG
        }
}