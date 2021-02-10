Shader "Custom/Triplanar"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTexUp ("Albedo Up (RGB)", 2D) = "white" {}
        _MainTexSide ("Albedo Side (RGB)", 2D) = "white" {}
        _Noise("Noise (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTexUp;
        sampler2D _MainTexSide;
        sampler2D _Noise;

        struct Input
        {
            float2 uv_MainTexUp;
            float2 uv_MainTexSide;
            float3 worldNormal;
            float3 worldPos;
        };

        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

      

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float n = tex2D(_Noise, IN.worldPos.xz*0.25).r * 0.5;
            float up = saturate(dot(IN.worldNormal, float3(0, 1, 0)));
            up = 1-step(up-n, 0.45);
            float4 upC = tex2D(_MainTexUp, IN.worldPos.xz*0.5) * _Color;
            float right = abs(dot(IN.worldNormal, float3(1, 0, 0)));
            float4 side1 = tex2D(_MainTexSide, IN.worldPos.yx) * _Color;
            float4 side2 = tex2D(_MainTexSide, IN.worldPos.zy) * _Color;
            float4 sideC = lerp(side1, side2, right);
            fixed4 c = lerp(sideC, upC, up);
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = 0;
            o.Smoothness = 0;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
