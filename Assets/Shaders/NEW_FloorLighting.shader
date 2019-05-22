// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:1,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:6610,x:32719,y:32712,varname:node_6610,prsc:2|emission-9724-OUT;n:type:ShaderForge.SFN_Time,id:266,x:30213,y:32698,varname:node_266,prsc:2;n:type:ShaderForge.SFN_FragmentPosition,id:396,x:30571,y:32623,varname:node_396,prsc:2;n:type:ShaderForge.SFN_Add,id:1142,x:30843,y:32800,varname:node_1142,prsc:2|A-8923-OUT,B-6243-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5775,x:31262,y:32866,varname:node_5775,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5365-OUT;n:type:ShaderForge.SFN_Add,id:6894,x:31466,y:32810,varname:node_6894,prsc:2|A-8923-OUT,B-5775-OUT;n:type:ShaderForge.SFN_Multiply,id:2217,x:31695,y:33001,varname:node_2217,prsc:2|A-6894-OUT,B-1812-OUT,C-9840-OUT;n:type:ShaderForge.SFN_Tau,id:9840,x:31543,y:33126,varname:node_9840,prsc:2;n:type:ShaderForge.SFN_Sin,id:5308,x:31873,y:32986,varname:node_5308,prsc:2|IN-2217-OUT;n:type:ShaderForge.SFN_RemapRange,id:8,x:32078,y:32988,varname:node_8,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-5308-OUT;n:type:ShaderForge.SFN_Clamp01,id:1114,x:32251,y:32988,varname:node_1114,prsc:2|IN-8-OUT;n:type:ShaderForge.SFN_Slider,id:1812,x:31349,y:33045,ptovrint:False,ptlb:Line Scale,ptin:_LineScale,varname:_LineScale,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:100;n:type:ShaderForge.SFN_Slider,id:5125,x:30056,y:32840,ptovrint:False,ptlb:TimeMult,ptin:_TimeMult,varname:node_5125,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:5000;n:type:ShaderForge.SFN_Multiply,id:8767,x:30461,y:32802,varname:node_8767,prsc:2|A-266-TSL,B-5125-OUT;n:type:ShaderForge.SFN_Multiply,id:6243,x:30620,y:32831,varname:node_6243,prsc:2|A-8767-OUT,B-6915-OUT;n:type:ShaderForge.SFN_TexCoord,id:3621,x:30648,y:32954,varname:node_3621,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:5365,x:31053,y:32870,varname:node_5365,prsc:2|A-1142-OUT,B-9278-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9278,x:30837,y:32943,varname:node_9278,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3621-V;n:type:ShaderForge.SFN_SwitchProperty,id:8923,x:30843,y:32641,ptovrint:False,ptlb:XZ,ptin:_XZ,varname:node_8923,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-396-X,B-396-Z;n:type:ShaderForge.SFN_ValueProperty,id:6915,x:30420,y:32998,ptovrint:False,ptlb:FR,ptin:_FR,cmnt:Must be 1 or -1,varname:node_6915,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-1;n:type:ShaderForge.SFN_Color,id:6061,x:32153,y:32757,ptovrint:False,ptlb:Emissive,ptin:_Emissive,varname:node_6061,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:8633,x:32153,y:32534,ptovrint:False,ptlb:BLACK,ptin:_BLACK,varname:node_8633,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:9724,x:32468,y:32748,varname:node_9724,prsc:2|A-8633-RGB,B-6061-RGB,T-1114-OUT;proporder:1812-5125-8923-6915-6061-8633;pass:END;sub:END;*/

Shader "Custom/FloorLighting" {
    Properties {
        _LineScale ("Line Scale", Range(0, 100)) = 1
        _TimeMult ("TimeMult", Range(0, 5000)) = 10
        [MaterialToggle] _XZ ("XZ", Float ) = 0
        _FR ("FR", Float ) = -1
        [HDR]_Emissive ("Emissive", Color) = (0.5,0.5,0.5,1)
        _BLACK ("BLACK", Color) = (0,0,0,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "DEFERRED"
            Tags {
                "LightMode"="Deferred"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_DEFERRED
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile ___ UNITY_HDR_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles switch 
            #pragma target 3.0
            uniform float _LineScale;
            uniform float _TimeMult;
            uniform fixed _XZ;
            uniform float _FR;
            uniform float4 _Emissive;
            uniform float4 _BLACK;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            void frag(
                VertexOutput i,
                out half4 outDiffuse : SV_Target0,
                out half4 outSpecSmoothness : SV_Target1,
                out half4 outNormal : SV_Target2,
                out half4 outEmission : SV_Target3 )
            {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float _XZ_var = lerp( i.posWorld.r, i.posWorld.b, _XZ );
                float4 node_266 = _Time;
                float node_1114 = saturate((sin(((_XZ_var+((_XZ_var+((node_266.r*_TimeMult)*_FR))+i.uv0.g.r).r)*_LineScale*6.28318530718))*2.0+-1.0));
                float3 emissive = lerp(_BLACK.rgb,_Emissive.rgb,node_1114);
                float3 finalColor = emissive;
                outDiffuse = half4( 0, 0, 0, 1 );
                outSpecSmoothness = half4(0,0,0,0);
                outNormal = half4( normalDirection * 0.5 + 0.5, 1 );
                outEmission = half4( lerp(_BLACK.rgb,_Emissive.rgb,node_1114), 1 );
                #ifndef UNITY_HDR_ON
                    outEmission.rgb = exp2(-outEmission.rgb);
                #endif
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles switch 
            #pragma target 3.0
            uniform float _LineScale;
            uniform float _TimeMult;
            uniform fixed _XZ;
            uniform float _FR;
            uniform float4 _Emissive;
            uniform float4 _BLACK;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float _XZ_var = lerp( i.posWorld.r, i.posWorld.b, _XZ );
                float4 node_266 = _Time;
                float node_1114 = saturate((sin(((_XZ_var+((_XZ_var+((node_266.r*_TimeMult)*_FR))+i.uv0.g.r).r)*_LineScale*6.28318530718))*2.0+-1.0));
                float3 emissive = lerp(_BLACK.rgb,_Emissive.rgb,node_1114);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
