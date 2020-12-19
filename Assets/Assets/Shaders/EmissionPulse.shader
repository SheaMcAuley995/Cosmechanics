// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9591,x:32719,y:32712,varname:node_9591,prsc:2|normal-1075-RGB,emission-25-OUT;n:type:ShaderForge.SFN_Tex2d,id:1075,x:32273,y:32752,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_1075,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Color,id:3605,x:32010,y:32958,ptovrint:False,ptlb:_Color,ptin:__Color,varname:node_3605,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.5458817,c4:1;n:type:ShaderForge.SFN_Multiply,id:6423,x:32201,y:32958,varname:node_6423,prsc:2|A-3605-RGB,B-2847-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2847,x:32010,y:33132,ptovrint:False,ptlb:EmissionValue,ptin:_EmissionValue,varname:node_2847,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Sin,id:4558,x:31992,y:33226,varname:node_4558,prsc:2|IN-8868-OUT;n:type:ShaderForge.SFN_Time,id:1579,x:31629,y:33132,varname:node_1579,prsc:2;n:type:ShaderForge.SFN_RemapRange,id:7242,x:32158,y:33226,varname:node_7242,prsc:2,frmn:-1,frmx:1,tomn:0.01,tomx:1|IN-4558-OUT;n:type:ShaderForge.SFN_Multiply,id:25,x:32415,y:33102,varname:node_25,prsc:2|A-6423-OUT,B-7242-OUT;n:type:ShaderForge.SFN_Multiply,id:8868,x:31827,y:33226,varname:node_8868,prsc:2|A-1579-TTR,B-540-OUT;n:type:ShaderForge.SFN_Slider,id:540,x:31472,y:33347,ptovrint:False,ptlb:EmissionFrequency,ptin:_EmissionFrequency,varname:node_540,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:1,max:50;proporder:1075-3605-2847-540;pass:END;sub:END;*/

Shader "Custom/EmissionPulse" {
    Properties {
        _Normal ("Normal", 2D) = "bump" {}
        [HDR]__Color ("_Color", Color) = (0,1,0.5458817,1)
        _EmissionValue ("EmissionValue", Float ) = 1
        _EmissionFrequency ("EmissionFrequency", Range(0.1, 50)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles xboxone ps4 switch 
            #pragma target 3.0
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float4 __Color;
            uniform float _EmissionValue;
            uniform float _EmissionFrequency;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
////// Lighting:
////// Emissive:
                float4 node_1579 = _Time;
                float3 emissive = ((__Color.rgb*_EmissionValue)*(sin((node_1579.a*_EmissionFrequency))*0.495+0.505));
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
