// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:5706,x:32719,y:32712,varname:node_5706,prsc:2|diff-678-OUT,spec-9152-OUT,normal-7546-OUT,emission-3845-OUT,alpha-9534-OUT,voffset-2672-OUT;n:type:ShaderForge.SFN_Tex2d,id:5549,x:31858,y:32226,ptovrint:False,ptlb:Albedo,ptin:_Albedo,varname:node_5549,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:589,x:31817,y:32479,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_589,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8b8c8a10edf94ddc8cc4cc4fcd5696a9,ntxv:3,isnm:True|UVIN-9194-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8093,x:31405,y:32518,varname:node_8093,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:9194,x:31586,y:32661,varname:node_9194,prsc:2,spu:0.2,spv:0.2|UVIN-8093-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:9534,x:32209,y:33071,ptovrint:False,ptlb:Transparency Value,ptin:_TransparencyValue,varname:node_9534,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Color,id:321,x:31858,y:32052,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_321,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.749012,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:678,x:32060,y:32108,varname:node_678,prsc:2|A-321-RGB,B-5549-RGB;n:type:ShaderForge.SFN_Multiply,id:3345,x:32096,y:32589,varname:node_3345,prsc:2|A-589-RGB,B-2818-OUT;n:type:ShaderForge.SFN_Fresnel,id:2818,x:31906,y:32654,varname:node_2818,prsc:2|NRM-589-RGB;n:type:ShaderForge.SFN_Multiply,id:7546,x:32177,y:32421,varname:node_7546,prsc:2|A-678-OUT,B-3345-OUT;n:type:ShaderForge.SFN_Fresnel,id:7023,x:32273,y:32759,varname:node_7023,prsc:2|NRM-7546-OUT,EXP-377-OUT;n:type:ShaderForge.SFN_ValueProperty,id:377,x:32050,y:32821,ptovrint:False,ptlb:fresne,ptin:_fresne,varname:node_377,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:6;n:type:ShaderForge.SFN_Color,id:2297,x:32234,y:32905,ptovrint:False,ptlb:GlowColor,ptin:_GlowColor,varname:node_2297,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.7303271,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:3845,x:32448,y:32848,varname:node_3845,prsc:2|A-7023-OUT,B-2297-RGB;n:type:ShaderForge.SFN_Multiply,id:2672,x:32374,y:33258,varname:node_2672,prsc:2|A-452-OUT,B-5674-OUT;n:type:ShaderForge.SFN_ValueProperty,id:452,x:32321,y:33148,ptovrint:False,ptlb:node_452,ptin:_node_452,varname:node_452,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Tangent,id:5674,x:32149,y:33359,varname:node_5674,prsc:2;n:type:ShaderForge.SFN_Slider,id:9152,x:32230,y:32659,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:node_9152,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:30;proporder:5549-589-9534-321-377-2297-452-9152;pass:END;sub:END;*/

Shader "Custom/QuirkyMovement" {
    Properties {
        _Albedo ("Albedo", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _TransparencyValue ("Transparency Value", Float ) = 0.1
        _Color ("Color", Color) = (0,0.749012,1,1)
        _fresne ("fresne", Float ) = 6
        [HDR]_GlowColor ("GlowColor", Color) = (0,0.7303271,1,1)
        _node_452 ("node_452", Float ) = 3
        _Specular ("Specular", Range(0, 30)) = 0
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
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _TransparencyValue;
            uniform float4 _Color;
            uniform float _fresne;
            uniform float4 _GlowColor;
            uniform float _node_452;
            uniform float _Specular;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                v.vertex.xyz += (_node_452*o.tangentDir);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float3 node_678 = (_Color.rgb*_Albedo_var.rgb);
                float4 node_9332 = _Time;
                float2 node_9194 = (i.uv0+node_9332.g*float2(0.2,0.2));
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_9194, _Normal)));
                float3 node_7546 = (node_678*(_Normal_var.rgb*(1.0-max(0,dot(_Normal_var.rgb, viewDirection)))));
                float3 normalLocal = node_7546;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Specular,_Specular,_Specular);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = node_678;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (pow(1.0-max(0,dot(node_7546, viewDirection)),_fresne)*_GlowColor.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,_TransparencyValue);
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
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _TransparencyValue;
            uniform float4 _Color;
            uniform float _fresne;
            uniform float4 _GlowColor;
            uniform float _node_452;
            uniform float _Specular;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                v.vertex.xyz += (_node_452*o.tangentDir);
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
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float3 node_678 = (_Color.rgb*_Albedo_var.rgb);
                float4 node_372 = _Time;
                float2 node_9194 = (i.uv0+node_372.g*float2(0.2,0.2));
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_9194, _Normal)));
                float3 node_7546 = (node_678*(_Normal_var.rgb*(1.0-max(0,dot(_Normal_var.rgb, viewDirection)))));
                float3 normalLocal = node_7546;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Specular,_Specular,_Specular);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = node_678;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * _TransparencyValue,0);
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
            uniform float _node_452;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float3 tangentDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                v.vertex.xyz += (_node_452*o.tangentDir);
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
