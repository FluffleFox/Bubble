Shader "Custom/Bubble"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _Fresnel("Fresnel", Range(0,10)) = 1.0
        _Mask("Mask", 2D) = "white" {}
        _Dissolve("Dissolve", Range(0,1)) = 0.0

     _Amount("Displacement scale", Range(0,0.5)) = 0.2
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
        Cull back

        CGPROGRAM
        #pragma surface surf Standard alpha vertex:vert
        #pragma target 3.0

        sampler2D _Mask;
        float _Fresnel;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        half _Amount;
        half _Dissolve;

        void vert(inout appdata_full v) {
            v.vertex.xyz = v.vertex.xyz * (1.0 + _Amount * sin(_Time.y));
        }


        struct Input
        {
            float2 uv_Mask;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float x = IN.uv_Mask.x - 0.5;
            float y = IN.uv_Mask.y - 0.5;
            float dist = x * x + y * y;

            fixed4 m = tex2D(_Mask, IN.uv_Mask);

            if (dist>=0.25 || m.r<=_Dissolve)
                discard;


            fixed4 c = _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            float fres=pow(dist * 4.0, _Fresnel);
            o.Alpha = 0.0;
            o.Emission = fres*0.2*_Color;

            fixed4 tc;
            tc.x = IN.uv_Mask.x;
            tc.y = IN.uv_Mask.y;
            tc.z = 1.0 - dist * 2.0;
            tc.w = 1.0;
            o.Normal = UnpackNormal(tc);
        }
        ENDCG
    }
}
