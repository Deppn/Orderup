Shader "Custom/ToonShaderWithRamp"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,1,1)
        _MainTex ("Main Texture", 2D) = "white" {}
        _Ramp ("Toon Ramp (1D)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _Ramp;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // Normal direction
            float3 worldNormal = normalize(IN.worldNormal);
            float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
            float ndotl = dot(worldNormal, lightDir);

            // Remap from [-1..1] to [0..1]
            ndotl = saturate(ndotl);

            // Use ramp texture (1D gradient)
            fixed4 rampColor = tex2D(_Ramp, float2(ndotl, 0.5));

            o.Albedo = c.rgb * rampColor.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
