// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:6944,x:32719,y:32712,varname:node_6944,prsc:2|normal-9642-OUT,emission-2909-OUT,alpha-5902-OUT,refract-3651-OUT;n:type:ShaderForge.SFN_Tex2d,id:5369,x:31689,y:32656,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_9570,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:55e2334423fefa34fb4916f6780413d3,ntxv:3,isnm:True|UVIN-9115-UVOUT;n:type:ShaderForge.SFN_Append,id:8245,x:32026,y:32894,varname:node_8245,prsc:2|A-5369-R,B-5369-G;n:type:ShaderForge.SFN_Slider,id:5902,x:31556,y:33181,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_251,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2949067,max:1;n:type:ShaderForge.SFN_Color,id:8114,x:31770,y:32252,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8761,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3463866,c2:0.5079582,c3:0.5283019,c4:0;n:type:ShaderForge.SFN_TexCoord,id:131,x:31338,y:32773,varname:node_131,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:7507,x:32026,y:33028,varname:node_7507,prsc:2|A-8245-OUT,B-30-OUT;n:type:ShaderForge.SFN_Slider,id:30,x:31556,y:33091,ptovrint:False,ptlb:RefractionValue,ptin:_RefractionValue,varname:node_2042,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.5;n:type:ShaderForge.SFN_Lerp,id:2909,x:32025,y:32266,varname:node_2909,prsc:2|A-8114-RGB,B-3952-RGB,T-2959-OUT;n:type:ShaderForge.SFN_Color,id:3952,x:31770,y:32088,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_4855,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2672659,c2:0.735849,c3:0.398342,c4:1;n:type:ShaderForge.SFN_Fresnel,id:2959,x:31705,y:32474,varname:node_2959,prsc:2|EXP-1710-OUT;n:type:ShaderForge.SFN_Slider,id:3909,x:31019,y:32393,ptovrint:False,ptlb:fres,ptin:_fres,varname:node_5309,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.5,cur:1.393163,max:3;n:type:ShaderForge.SFN_Multiply,id:8192,x:32026,y:32750,varname:node_8192,prsc:2|A-2959-OUT,B-131-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3651,x:32392,y:33082,varname:node_3651,prsc:2|A-8192-OUT,B-7507-OUT;n:type:ShaderForge.SFN_Divide,id:5273,x:32149,y:32618,varname:node_5273,prsc:2|A-5369-RGB,B-7866-OUT;n:type:ShaderForge.SFN_Vector1,id:7866,x:32007,y:32516,varname:node_7866,prsc:2,v1:3.33;n:type:ShaderForge.SFN_Panner,id:9115,x:31558,y:32749,varname:node_9115,prsc:2,spu:-0.01,spv:0|UVIN-131-UVOUT;n:type:ShaderForge.SFN_Vector1,id:7055,x:31283,y:32636,varname:node_7055,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:1710,x:31471,y:32514,varname:node_1710,prsc:2|A-3909-OUT,B-7055-OUT;n:type:ShaderForge.SFN_Normalize,id:9642,x:32305,y:32708,varname:node_9642,prsc:2|IN-5273-OUT;proporder:5369-30-3909-8114-3952-5902;pass:END;sub:END;*/

Shader "Custom/FlorpReverse" {
    Properties {
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _RefractionValue ("RefractionValue", Range(0, 0.5)) = 0
        _fres ("fres", Range(0.5, 3)) = 1.393163
        _Color ("Color", Color) = (0.3463866,0.5079582,0.5283019,0)
        _Color2 ("Color2", Color) = (0.2672659,0.735849,0.398342,1)
        _Opacity ("Opacity", Range(0, 1)) = 0.2949067
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
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
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
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_692 = _Time;
                float2 node_9115 = (i.uv0+node_692.g*float2(-0.01,0));
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_9115, _NormalMap)));
                float3 normalLocal = normalize((_NormalMap_var.rgb/3.33));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float node_2959 = pow(1.0-max(0,dot(normalDirection, viewDirection)),(_fres*(-1.0)));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + ((node_2959*i.uv0)*(float2(_NormalMap_var.r,_NormalMap_var.g)*_RefractionValue));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_Color.rgb,_Color2.rgb,node_2959);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,_Opacity),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
