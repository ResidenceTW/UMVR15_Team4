Shader "Custom/BossDissolve"
{
    Properties
    {
        _Brightness("Brightness", Range(0.5, 2.0)) = 1.2
        _MainTex("Base Texture", 2D) = "white" {}
        _DeathTex("Death Texture", 2D) = "black" {}
        _DissolveAmount("Dissolve Amount", Range(0,1)) = 0
        _Alpha("Alpha", Range(0,1)) = 1.0
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
            LOD 100
            ZWrite On

            CGINCLUDE
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1) // [�s�W] FOG �ܼ�
            };

            sampler2D _MainTex;
            sampler2D _DeathTex;
            float _DissolveAmount;
            float _Alpha;
            float _Brightness;

            v2f vert(appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                UNITY_TRANSFER_FOG(o, o.pos); // [�s�W] �ǻ� FOG �ƾ�
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 deathCol = tex2D(_DeathTex, i.uv);

                // �p�G DissolveAmount ���� 1�A��� DeathTex
                col = lerp(col, deathCol, _DissolveAmount);
                col.rgb *= _Brightness;
                // [�s�W] �M�� Unity �������ĪG
                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG

            Pass {
                Tags { "LightMode" = "ForwardBase" }
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog // [�T�O FOG �i��]
                ENDCG
            }

            // Shadow Caster �� Shader �i�H��v�v�l
            Pass {
                Tags { "LightMode" = "ShadowCaster" }
                CGPROGRAM
                #pragma vertex vertShadow
                #pragma fragment fragShadow
                #include "UnityCG.cginc"

                struct v2fShadow {
                    V2F_SHADOW_CASTER;
                };

                v2fShadow vertShadow(appdata_base v) {
                    v2fShadow o;
                    TRANSFER_SHADOW_CASTER(o)
                    return o;
                }

                float4 fragShadow(v2fShadow i) : SV_Target {
                    return 0;
                }
                ENDCG
            }
        }
}