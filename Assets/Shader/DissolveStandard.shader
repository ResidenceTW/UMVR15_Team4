Shader "Custom/DissolveStandard"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _MetallicGlossMap ("Metallic", 2D) = "black" {}
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0.0
        _EmissionColor ("Emission Color", Color) = (0, 0, 0, 1) // 預設不發光
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _MetallicGlossMap;
        sampler2D _DissolveTex;
        float _DissolveAmount;
        fixed4 _EmissionColor; // 由程式控制發光

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float dissolve = tex2D(_DissolveTex, IN.uv_MainTex).r;
            clip(dissolve - _DissolveAmount);

            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
            o.Metallic = tex2D(_MetallicGlossMap, IN.uv_MainTex).r;

            // 加入發光效果 (可由程式碼控制)
            o.Emission = _EmissionColor.rgb;
        }
        ENDCG
    }
}