Shader "Custom/Water"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _DepthColor("DepthColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _FlowTex ("Flow (RGB)", 2D) = "white" {}
        _FlowStrength("FlowStrength", Float) = 1
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

        sampler2D _MainTex;
        sampler2D _FlowTex;
        float _FlowStrength;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_FlowTex;
            float3 worldPos;
            float3 worldNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _DepthColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 viewDir = normalize(IN.worldPos - _WorldSpaceCameraPos);

            // Albedo comes from a texture tinted by color
            float depth = dot(-viewDir, IN.worldNormal);
            float4 color = lerp(_Color, _DepthColor, depth);
            float4 flow = tex2D(_FlowTex, IN.uv_FlowTex + _Time.x*2);
            fixed c = tex2D (_MainTex, IN.uv_MainTex + _Time.x + flow * _FlowStrength).r;
            fixed c2 = tex2D(_MainTex, IN.uv_MainTex*0.5 + _Time.x + flow * _FlowStrength).r;
            
            float foam = 1 - (c + pow(c2, 2));
            o.Albedo = step(foam,0.5)*0.05 + step(foam, 0.15) * 0.25 + color;
            
            o.Metallic = 0;
            o.Smoothness = 0;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
