// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:1,grmd:1,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:5551,x:32683,y:32715,varname:node_5551,prsc:2|normal-5359-OUT,emission-8290-OUT,alpha-251-OUT,refract-9205-OUT;n:type:ShaderForge.SFN_Tex2d,id:9570,x:31704,y:32660,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_9570,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:55e2334423fefa34fb4916f6780413d3,ntxv:3,isnm:True|UVIN-1042-UVOUT;n:type:ShaderForge.SFN_Append,id:3913,x:31962,y:32830,varname:node_3913,prsc:2|A-9570-R,B-9570-G;n:type:ShaderForge.SFN_Slider,id:251,x:31492,y:33117,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_251,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:8761,x:31706,y:32188,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8761,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3463866,c2:0.5079582,c3:0.5283019,c4:0;n:type:ShaderForge.SFN_TexCoord,id:1042,x:31048,y:32779,varname:node_1042,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:8660,x:31962,y:32964,varname:node_8660,prsc:2|A-3913-OUT,B-2042-OUT;n:type:ShaderForge.SFN_Slider,id:2042,x:31492,y:33027,ptovrint:False,ptlb:RefractionValue,ptin:_RefractionValue,varname:node_2042,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.5;n:type:ShaderForge.SFN_Lerp,id:8290,x:31961,y:32202,varname:node_8290,prsc:2|A-8761-RGB,B-4855-RGB,T-9775-OUT;n:type:ShaderForge.SFN_Color,id:4855,x:31706,y:32024,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_4855,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2672659,c2:0.735849,c3:0.398342,c4:1;n:type:ShaderForge.SFN_Fresnel,id:9775,x:31704,y:32513,varname:node_9775,prsc:2|EXP-5309-OUT;n:type:ShaderForge.SFN_Slider,id:5309,x:31339,y:32534,ptovrint:False,ptlb:fres,ptin:_fres,varname:node_5309,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1,max:3;n:type:ShaderForge.SFN_Multiply,id:1927,x:31962,y:32686,varname:node_1927,prsc:2|A-9775-OUT,B-1042-UVOUT;n:type:ShaderForge.SFN_Multiply,id:9205,x:32328,y:33018,varname:node_9205,prsc:2|A-1927-OUT,B-8660-OUT;n:type:ShaderForge.SFN_Divide,id:1475,x:32085,y:32554,varname:node_1475,prsc:2|A-9570-RGB,B-778-OUT;n:type:ShaderForge.SFN_Vector1,id:778,x:31905,y:32588,varname:node_778,prsc:2,v1:3.33;n:type:ShaderForge.SFN_Normalize,id:5359,x:32214,y:32619,varname:node_5359,prsc:2|IN-1475-OUT;proporder:8761-4855-9570-251-2042-5309;pass:END;sub:END;*/

Shader "Custom/Florp" {
    Properties {
        _Color ("Color", Color) = (0.3463866,0.5079582,0.5283019,0)
        _Color2 ("Color2", Color) = (0.2672659,0.735849,0.398342,1)
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _Opacity ("Opacity", Range(0, 1)) = 0
        _RefractionValue ("RefractionValue", Range(0, 0.5)) = 0
        _fres ("fres", Range(1, 3)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _Opacity;
            uniform float4 _Color;
            uniform float _RefractionValue;
            uniform float4 _Color2;
            uniform float _fres;
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
                float4 projPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 normalLocal = normalize((_NormalMap_var.rgb/3.33));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float node_9775 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_fres);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + ((node_9775*i.uv0)*(float2(_NormalMap_var.r,_NormalMap_var.g)*_RefractionValue));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_Color.rgb,_Color2.rgb,node_9775);
                float3 finalColor = emissive;
                return fixed4(lerp(sceneColor.rgb, finalColor,_Opacity),1);
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float4 _Color2;
            uniform float _fres;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_9775 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_fres);
                o.Emission = lerp(_Color.rgb,_Color2.rgb,node_9775);
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
