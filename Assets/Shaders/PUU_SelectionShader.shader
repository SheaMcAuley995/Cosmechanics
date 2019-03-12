// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:6033,x:32719,y:32712,varname:node_6033,prsc:2|emission-9139-OUT,voffset-6201-OUT;n:type:ShaderForge.SFN_Tex2d,id:217,x:31262,y:33329,varname:node_217,prsc:2,tex:ea90d36e8e1a5034fae55f6d495405af,ntxv:0,isnm:False|UVIN-9832-OUT,TEX-4473-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:4473,x:30669,y:32370,ptovrint:False,ptlb:Mix,ptin:_Mix,varname:node_4473,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ea90d36e8e1a5034fae55f6d495405af,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:33,x:31262,y:33504,ptovrint:False,ptlb:offsetAmmount,ptin:_offsetAmmount,varname:node_33,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:200,x:31449,y:33436,varname:node_200,prsc:2|A-217-R,B-33-OUT;n:type:ShaderForge.SFN_Append,id:6201,x:31606,y:33540,varname:node_6201,prsc:2|A-9180-OUT,B-200-OUT;n:type:ShaderForge.SFN_Vector1,id:9180,x:31422,y:33611,varname:node_9180,prsc:2,v1:0;n:type:ShaderForge.SFN_TexCoord,id:6100,x:30669,y:32529,varname:node_6100,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:2248,x:30673,y:33279,ptovrint:False,ptlb:V_Speed,ptin:_V_Speed,varname:node_2248,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:8864,x:30673,y:33202,ptovrint:False,ptlb:U_Speed,ptin:_U_Speed,varname:node_8864,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Append,id:9938,x:30893,y:33235,varname:node_9938,prsc:2|A-8864-OUT,B-2248-OUT;n:type:ShaderForge.SFN_Time,id:5634,x:30669,y:32670,varname:node_5634,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8930,x:31077,y:33235,varname:node_8930,prsc:2|A-9938-OUT,B-6100-UVOUT;n:type:ShaderForge.SFN_Add,id:9832,x:31262,y:33191,varname:node_9832,prsc:2|A-8930-OUT,B-5634-T;n:type:ShaderForge.SFN_Tex2d,id:8545,x:31528,y:32552,varname:node_8545,prsc:2,tex:ea90d36e8e1a5034fae55f6d495405af,ntxv:0,isnm:False|TEX-4473-TEX;n:type:ShaderForge.SFN_ValueProperty,id:2672,x:30796,y:32300,ptovrint:False,ptlb:V_LineSpeed,ptin:_V_LineSpeed,varname:_V_Speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:306,x:30796,y:32223,ptovrint:False,ptlb:U_LineSpeed,ptin:_U_LineSpeed,varname:_U_Speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:6057,x:30998,y:32223,varname:node_6057,prsc:2|A-306-OUT,B-2672-OUT;n:type:ShaderForge.SFN_Multiply,id:7499,x:31203,y:32223,varname:node_7499,prsc:2|A-6057-OUT,B-5634-TSL;n:type:ShaderForge.SFN_Add,id:7371,x:31401,y:32223,varname:node_7371,prsc:2|A-7499-OUT,B-6100-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:6198,x:31549,y:32385,varname:node_6198,prsc:2,tex:ea90d36e8e1a5034fae55f6d495405af,ntxv:0,isnm:False|UVIN-7371-OUT,TEX-4473-TEX;n:type:ShaderForge.SFN_Multiply,id:4282,x:31751,y:32490,varname:node_4282,prsc:2|A-6198-B,B-8545-G;n:type:ShaderForge.SFN_Tex2d,id:6285,x:31585,y:32807,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_6285,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a61c31d33defa4747824212c9234b61c,ntxv:0,isnm:False|UVIN-1247-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8536,x:31127,y:32865,varname:node_8536,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:1247,x:31277,y:32865,varname:node_1247,prsc:2,spu:0,spv:0.1|UVIN-8536-UVOUT;n:type:ShaderForge.SFN_Add,id:7308,x:31858,y:32956,varname:node_7308,prsc:2|A-6285-RGB,B-8127-OUT;n:type:ShaderForge.SFN_Multiply,id:3444,x:31948,y:32606,varname:node_3444,prsc:2|A-4282-OUT,B-7252-OUT;n:type:ShaderForge.SFN_Noise,id:4385,x:31468,y:32996,varname:node_4385,prsc:2|XY-1247-UVOUT;n:type:ShaderForge.SFN_Vector1,id:4530,x:31468,y:33120,varname:node_4530,prsc:2,v1:1;n:type:ShaderForge.SFN_Divide,id:8127,x:31628,y:33036,varname:node_8127,prsc:2|A-4385-OUT,B-4530-OUT;n:type:ShaderForge.SFN_Multiply,id:2146,x:31858,y:33077,varname:node_2146,prsc:2|A-6285-RGB,B-8127-OUT;n:type:ShaderForge.SFN_Color,id:4418,x:32140,y:31976,ptovrint:False,ptlb:GlowColor,ptin:_GlowColor,varname:node_4418,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:978,x:32328,y:31895,varname:node_978,prsc:2|A-3444-OUT,B-4418-RGB;n:type:ShaderForge.SFN_Multiply,id:9139,x:32398,y:32051,varname:node_9139,prsc:2|A-978-OUT,B-1548-OUT;n:type:ShaderForge.SFN_Slider,id:5914,x:31831,y:32179,ptovrint:False,ptlb:GlowIntensity,ptin:_GlowIntensity,varname:node_5914,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:2000;n:type:ShaderForge.SFN_Time,id:9453,x:31890,y:32250,varname:node_9453,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1548,x:32207,y:32194,varname:node_1548,prsc:2|A-5914-OUT,B-5907-OUT;n:type:ShaderForge.SFN_Sin,id:3893,x:32136,y:32356,varname:node_3893,prsc:2|IN-5462-OUT;n:type:ShaderForge.SFN_RemapRange,id:5907,x:32291,y:32356,varname:node_5907,prsc:2,frmn:-1,frmx:1,tomn:0.1,tomx:1|IN-3893-OUT;n:type:ShaderForge.SFN_Multiply,id:5462,x:31954,y:32373,varname:node_5462,prsc:2|A-9453-TTR,B-2981-OUT;n:type:ShaderForge.SFN_Slider,id:2981,x:32239,y:32596,ptovrint:False,ptlb:GlowPulseFrequency,ptin:_GlowPulseFrequency,varname:node_2981,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:1,max:20;n:type:ShaderForge.SFN_Multiply,id:7252,x:32035,y:32999,varname:node_7252,prsc:2|A-7308-OUT,B-2146-OUT;proporder:4473-4418-5914-6285-33-2248-8864-2672-306-2981;pass:END;sub:END;*/

Shader "Custom/PUU_SelectionShader" {
    Properties {
        _Mix ("Mix", 2D) = "white" {}
        _GlowColor ("GlowColor", Color) = (1,1,1,1)
        _GlowIntensity ("GlowIntensity", Range(0, 2000)) = 2
        _Noise ("Noise", 2D) = "white" {}
        _offsetAmmount ("offsetAmmount", Float ) = 1
        _V_Speed ("V_Speed", Float ) = 0
        _U_Speed ("U_Speed", Float ) = 0.1
        _V_LineSpeed ("V_LineSpeed", Float ) = 0.1
        _U_LineSpeed ("U_LineSpeed", Float ) = 0
        _GlowPulseFrequency ("GlowPulseFrequency", Range(0.1, 20)) = 1
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Mix; uniform float4 _Mix_ST;
            uniform float _offsetAmmount;
            uniform float _V_Speed;
            uniform float _U_Speed;
            uniform float _V_LineSpeed;
            uniform float _U_LineSpeed;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float4 _GlowColor;
            uniform float _GlowIntensity;
            uniform float _GlowPulseFrequency;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 node_5634 = _Time;
                float2 node_9832 = ((float2(_U_Speed,_V_Speed)*o.uv0)+node_5634.g);
                float4 node_217 = tex2Dlod(_Mix,float4(TRANSFORM_TEX(node_9832, _Mix),0.0,0));
                v.vertex.xyz += float3(float2(0.0,(node_217.r*_offsetAmmount)),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_5634 = _Time;
                float2 node_7371 = ((float2(_U_LineSpeed,_V_LineSpeed)*node_5634.r)+i.uv0);
                float4 node_6198 = tex2D(_Mix,TRANSFORM_TEX(node_7371, _Mix));
                float4 node_8545 = tex2D(_Mix,TRANSFORM_TEX(i.uv0, _Mix));
                float4 node_5746 = _Time;
                float2 node_1247 = (i.uv0+node_5746.g*float2(0,0.1));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_1247, _Noise));
                float2 node_4385_skew = node_1247 + 0.2127+node_1247.x*0.3713*node_1247.y;
                float2 node_4385_rnd = 4.789*sin(489.123*(node_4385_skew));
                float node_4385 = frac(node_4385_rnd.x*node_4385_rnd.y*(1+node_4385_skew.x));
                float node_8127 = (node_4385/1.0);
                float4 node_9453 = _Time;
                float3 emissive = ((((node_6198.b*node_8545.g)*((_Noise_var.rgb+node_8127)*(_Noise_var.rgb*node_8127)))*_GlowColor.rgb)*(_GlowIntensity*(sin((node_9453.a*_GlowPulseFrequency))*0.45+0.55)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Mix; uniform float4 _Mix_ST;
            uniform float _offsetAmmount;
            uniform float _V_Speed;
            uniform float _U_Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 node_5634 = _Time;
                float2 node_9832 = ((float2(_U_Speed,_V_Speed)*o.uv0)+node_5634.g);
                float4 node_217 = tex2Dlod(_Mix,float4(TRANSFORM_TEX(node_9832, _Mix),0.0,0));
                v.vertex.xyz += float3(float2(0.0,(node_217.r*_offsetAmmount)),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
