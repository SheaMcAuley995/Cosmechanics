// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:6542,x:32719,y:32712,varname:node_6542,prsc:2|diff-2520-OUT,normal-9587-RGB,emission-8540-OUT,voffset-3394-OUT,tess-7652-VOUT;n:type:ShaderForge.SFN_TexCoord,id:7426,x:31504,y:33084,varname:node_7426,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Divide,id:3394,x:31892,y:33128,varname:node_3394,prsc:2|A-9587-RGB,B-6409-OUT;n:type:ShaderForge.SFN_Vector1,id:1770,x:31669,y:33207,varname:node_1770,prsc:2,v1:8;n:type:ShaderForge.SFN_Color,id:4481,x:31285,y:32353,ptovrint:False,ptlb:node_4481,ptin:_node_4481,varname:node_4481,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9587,x:31700,y:32996,ptovrint:False,ptlb:node_9587,ptin:_node_9587,varname:node_9587,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:True|UVIN-8196-UVOUT;n:type:ShaderForge.SFN_Panner,id:8196,x:31494,y:32940,varname:node_8196,prsc:2,spu:0.1,spv:0.1|UVIN-7426-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2520,x:31663,y:32413,varname:node_2520,prsc:2|A-4481-RGB,B-9587-RGB;n:type:ShaderForge.SFN_Slider,id:6409,x:31543,y:33306,ptovrint:False,ptlb:offset,ptin:_offset,varname:node_6409,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:8.888889,max:20;n:type:ShaderForge.SFN_RgbToHsv,id:7652,x:31579,y:32567,varname:node_7652,prsc:2|IN-9587-RGB;n:type:ShaderForge.SFN_Multiply,id:8540,x:31814,y:32567,varname:node_8540,prsc:2|A-4481-RGB,B-7652-HOUT;proporder:4481-9587-6409;pass:END;sub:END;*/

Shader "Custom/Tewmp" {
    Properties {
        [HDR]_node_4481 ("node_4481", Color) = (0.5,0.5,0.5,1)
        _node_9587 ("node_9587", 2D) = "white" {}
        _offset ("offset", Range(0, 20)) = 8.888889
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
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _LightColor0;
            uniform float4 _node_4481;
            uniform sampler2D _node_9587; uniform float4 _node_9587_ST;
            uniform float _offset;
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
                float4 node_3357 = _Time;
                float2 node_8196 = (o.uv0+node_3357.g*float2(0.1,0.1));
                float3 _node_9587_var = UnpackNormal(tex2Dlod(_node_9587,float4(TRANSFORM_TEX(node_8196, _node_9587),0.0,0)));
                v.vertex.xyz += (_node_9587_var.rgb/_offset);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    float4 node_3357 = _Time;
                    float2 node_8196 = (v.texcoord0+node_3357.g*float2(0.1,0.1));
                    float3 _node_9587_var = UnpackNormal(tex2Dlod(_node_9587,float4(TRANSFORM_TEX(node_8196, _node_9587),0.0,0)));
                    float4 node_7652_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                    float4 node_7652_p = lerp(float4(float4(_node_9587_var.rgb,0.0).zy, node_7652_k.wz), float4(float4(_node_9587_var.rgb,0.0).yz, node_7652_k.xy), step(float4(_node_9587_var.rgb,0.0).z, float4(_node_9587_var.rgb,0.0).y));
                    float4 node_7652_q = lerp(float4(node_7652_p.xyw, float4(_node_9587_var.rgb,0.0).x), float4(float4(_node_9587_var.rgb,0.0).x, node_7652_p.yzx), step(node_7652_p.x, float4(_node_9587_var.rgb,0.0).x));
                    float node_7652_d = node_7652_q.x - min(node_7652_q.w, node_7652_q.y);
                    float node_7652_e = 1.0e-10;
                    float3 node_7652 = float3(abs(node_7652_q.z + (node_7652_q.w - node_7652_q.y) / (6.0 * node_7652_d + node_7652_e)), node_7652_d / (node_7652_q.x + node_7652_e), node_7652_q.x);;
                    return node_7652.b;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_3357 = _Time;
                float2 node_8196 = (i.uv0+node_3357.g*float2(0.1,0.1));
                float3 _node_9587_var = UnpackNormal(tex2D(_node_9587,TRANSFORM_TEX(node_8196, _node_9587)));
                float3 normalLocal = _node_9587_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = (_node_4481.rgb*_node_9587_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_7652_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_7652_p = lerp(float4(float4(_node_9587_var.rgb,0.0).zy, node_7652_k.wz), float4(float4(_node_9587_var.rgb,0.0).yz, node_7652_k.xy), step(float4(_node_9587_var.rgb,0.0).z, float4(_node_9587_var.rgb,0.0).y));
                float4 node_7652_q = lerp(float4(node_7652_p.xyw, float4(_node_9587_var.rgb,0.0).x), float4(float4(_node_9587_var.rgb,0.0).x, node_7652_p.yzx), step(node_7652_p.x, float4(_node_9587_var.rgb,0.0).x));
                float node_7652_d = node_7652_q.x - min(node_7652_q.w, node_7652_q.y);
                float node_7652_e = 1.0e-10;
                float3 node_7652 = float3(abs(node_7652_q.z + (node_7652_q.w - node_7652_q.y) / (6.0 * node_7652_d + node_7652_e)), node_7652_d / (node_7652_q.x + node_7652_e), node_7652_q.x);;
                float3 emissive = (_node_4481.rgb*node_7652.r);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _LightColor0;
            uniform float4 _node_4481;
            uniform sampler2D _node_9587; uniform float4 _node_9587_ST;
            uniform float _offset;
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
                float4 node_5 = _Time;
                float2 node_8196 = (o.uv0+node_5.g*float2(0.1,0.1));
                float3 _node_9587_var = UnpackNormal(tex2Dlod(_node_9587,float4(TRANSFORM_TEX(node_8196, _node_9587),0.0,0)));
                v.vertex.xyz += (_node_9587_var.rgb/_offset);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    float4 node_5 = _Time;
                    float2 node_8196 = (v.texcoord0+node_5.g*float2(0.1,0.1));
                    float3 _node_9587_var = UnpackNormal(tex2Dlod(_node_9587,float4(TRANSFORM_TEX(node_8196, _node_9587),0.0,0)));
                    float4 node_7652_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                    float4 node_7652_p = lerp(float4(float4(_node_9587_var.rgb,0.0).zy, node_7652_k.wz), float4(float4(_node_9587_var.rgb,0.0).yz, node_7652_k.xy), step(float4(_node_9587_var.rgb,0.0).z, float4(_node_9587_var.rgb,0.0).y));
                    float4 node_7652_q = lerp(float4(node_7652_p.xyw, float4(_node_9587_var.rgb,0.0).x), float4(float4(_node_9587_var.rgb,0.0).x, node_7652_p.yzx), step(node_7652_p.x, float4(_node_9587_var.rgb,0.0).x));
                    float node_7652_d = node_7652_q.x - min(node_7652_q.w, node_7652_q.y);
                    float node_7652_e = 1.0e-10;
                    float3 node_7652 = float3(abs(node_7652_q.z + (node_7652_q.w - node_7652_q.y) / (6.0 * node_7652_d + node_7652_e)), node_7652_d / (node_7652_q.x + node_7652_e), node_7652_q.x);;
                    return node_7652.b;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_5 = _Time;
                float2 node_8196 = (i.uv0+node_5.g*float2(0.1,0.1));
                float3 _node_9587_var = UnpackNormal(tex2D(_node_9587,TRANSFORM_TEX(node_8196, _node_9587)));
                float3 normalLocal = _node_9587_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = (_node_4481.rgb*_node_9587_var.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
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
            Cull Back
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform sampler2D _node_9587; uniform float4 _node_9587_ST;
            uniform float _offset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 node_7250 = _Time;
                float2 node_8196 = (o.uv0+node_7250.g*float2(0.1,0.1));
                float3 _node_9587_var = UnpackNormal(tex2Dlod(_node_9587,float4(TRANSFORM_TEX(node_8196, _node_9587),0.0,0)));
                v.vertex.xyz += (_node_9587_var.rgb/_offset);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    float4 node_7250 = _Time;
                    float2 node_8196 = (v.texcoord0+node_7250.g*float2(0.1,0.1));
                    float3 _node_9587_var = UnpackNormal(tex2Dlod(_node_9587,float4(TRANSFORM_TEX(node_8196, _node_9587),0.0,0)));
                    float4 node_7652_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                    float4 node_7652_p = lerp(float4(float4(_node_9587_var.rgb,0.0).zy, node_7652_k.wz), float4(float4(_node_9587_var.rgb,0.0).yz, node_7652_k.xy), step(float4(_node_9587_var.rgb,0.0).z, float4(_node_9587_var.rgb,0.0).y));
                    float4 node_7652_q = lerp(float4(node_7652_p.xyw, float4(_node_9587_var.rgb,0.0).x), float4(float4(_node_9587_var.rgb,0.0).x, node_7652_p.yzx), step(node_7652_p.x, float4(_node_9587_var.rgb,0.0).x));
                    float node_7652_d = node_7652_q.x - min(node_7652_q.w, node_7652_q.y);
                    float node_7652_e = 1.0e-10;
                    float3 node_7652 = float3(abs(node_7652_q.z + (node_7652_q.w - node_7652_q.y) / (6.0 * node_7652_d + node_7652_e)), node_7652_d / (node_7652_q.x + node_7652_e), node_7652_q.x);;
                    return node_7652.b;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
