// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1932,x:32719,y:32712,varname:node_1932,prsc:2|diff-81-OUT,alpha-5635-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:2234,x:30824,y:32686,varname:node_2234,prsc:2;n:type:ShaderForge.SFN_Subtract,id:7357,x:31255,y:32737,varname:node_7357,prsc:2|A-2234-XYZ,B-2175-XYZ;n:type:ShaderForge.SFN_ObjectPosition,id:2175,x:30824,y:32814,varname:node_2175,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:6533,x:31422,y:32767,varname:node_6533,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-7357-OUT;n:type:ShaderForge.SFN_Step,id:5788,x:32273,y:32807,varname:node_5788,prsc:2|A-7820-OUT,B-9995-OUT;n:type:ShaderForge.SFN_Slider,id:6435,x:31770,y:32901,ptovrint:False,ptlb:FillAmount,ptin:_FillAmount,varname:_FillAmount,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.5790306,max:1;n:type:ShaderForge.SFN_FaceSign,id:2048,x:31875,y:33086,varname:node_2048,prsc:2,fstp:0;n:type:ShaderForge.SFN_Color,id:9980,x:31875,y:33308,ptovrint:False,ptlb:SideColor,ptin:_SideColor,varname:_SideColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.3,c4:1;n:type:ShaderForge.SFN_Color,id:9627,x:31875,y:33480,ptovrint:False,ptlb:TopColor,ptin:_TopColor,varname:_TopColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.4,c3:0.5,c4:1;n:type:ShaderForge.SFN_If,id:3043,x:32379,y:33153,varname:node_3043,prsc:2|A-2048-VFACE,B-781-OUT,GT-9980-RGB,EQ-877-OUT,LT-9627-RGB;n:type:ShaderForge.SFN_Vector1,id:781,x:31875,y:33228,varname:node_781,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:81,x:32415,y:32938,varname:node_81,prsc:2|A-5788-OUT,B-3043-OUT;n:type:ShaderForge.SFN_Color,id:8951,x:31875,y:33636,ptovrint:False,ptlb:BottomColor,ptin:_BottomColor,varname:_BottomColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:877,x:32309,y:33482,varname:node_877,prsc:2|A-9627-RGB,B-8951-RGB,T-3946-OUT;n:type:ShaderForge.SFN_Clamp01,id:3946,x:31875,y:33804,cmnt:Saturate,varname:node_3946,prsc:2|IN-5718-OUT;n:type:ShaderForge.SFN_Add,id:5718,x:31360,y:33665,varname:node_5718,prsc:2|A-6533-G,B-1943-OUT;n:type:ShaderForge.SFN_Slider,id:1943,x:31165,y:33847,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:_Offset,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6947939,max:1;n:type:ShaderForge.SFN_TexCoord,id:437,x:31422,y:32941,varname:node_437,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:5635,x:32590,y:33268,ptovrint:False,ptlb:Opactiy,ptin:_Opactiy,varname:node_5635,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:7820,x:31837,y:32741,varname:node_7820,prsc:2|A-6533-OUT,B-6435-OUT;n:type:ShaderForge.SFN_Vector1,id:9995,x:32094,y:32982,varname:node_9995,prsc:2,v1:0.5;proporder:6435-9980-9627-8951-1943-5635;pass:END;sub:END;*/

Shader "Custom/Liquid" {
    Properties {
        _FillAmount ("FillAmount", Range(-1, 1)) = 0.5790306
        _SideColor ("SideColor", Color) = (0.5,0.5,0.3,1)
        _TopColor ("TopColor", Color) = (0.5,0.4,0.5,1)
        _BottomColor ("BottomColor", Color) = (0.5,0.5,0.5,1)
        _Offset ("Offset", Range(0, 1)) = 0.6947939
        _Opactiy ("Opactiy", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
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
            uniform float4 _LightColor0;
            uniform float _FillAmount;
            uniform float4 _SideColor;
            uniform float4 _TopColor;
            uniform float4 _BottomColor;
            uniform float _Offset;
            uniform float _Opactiy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 node_6533 = (i.posWorld.rgb-objPos.rgb).rgb;
                float node_3043_if_leA = step(isFrontFace,1.0);
                float node_3043_if_leB = step(1.0,isFrontFace);
                float3 diffuseColor = (step((node_6533+_FillAmount),0.5)*lerp((node_3043_if_leA*_TopColor.rgb)+(node_3043_if_leB*_SideColor.rgb),lerp(_TopColor.rgb,_BottomColor.rgb,saturate((node_6533.g+_Offset))),node_3043_if_leA*node_3043_if_leB));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,_Opactiy);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _FillAmount;
            uniform float4 _SideColor;
            uniform float4 _TopColor;
            uniform float4 _BottomColor;
            uniform float _Offset;
            uniform float _Opactiy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_6533 = (i.posWorld.rgb-objPos.rgb).rgb;
                float node_3043_if_leA = step(isFrontFace,1.0);
                float node_3043_if_leB = step(1.0,isFrontFace);
                float3 diffuseColor = (step((node_6533+_FillAmount),0.5)*lerp((node_3043_if_leA*_TopColor.rgb)+(node_3043_if_leB*_SideColor.rgb),lerp(_TopColor.rgb,_BottomColor.rgb,saturate((node_6533.g+_Offset))),node_3043_if_leA*node_3043_if_leB));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * _Opactiy,0);
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
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
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
