// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4260,x:32719,y:32712,varname:node_4260,prsc:2|alpha-5153-OUT;n:type:ShaderForge.SFN_Slider,id:9851,x:32905,y:32990,ptovrint:False,ptlb:opacity,ptin:_opacity,varname:node_9851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ObjectPosition,id:7152,x:30918,y:32792,varname:node_7152,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:5215,x:31206,y:32623,ptovrint:False,ptlb:WobbleX,ptin:_WobbleX,varname:node_5215,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4649,x:31206,y:32711,ptovrint:False,ptlb:WobbleY,ptin:_WobbleY,varname:node_4649,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:2778,x:31424,y:32734,varname:node_2778,prsc:2|A-5215-OUT,B-7152-XYZ;n:type:ShaderForge.SFN_Multiply,id:546,x:31424,y:32869,varname:node_546,prsc:2|A-4649-OUT,B-8387-OUT;n:type:ShaderForge.SFN_Set,id:3072,x:30897,y:32703,varname:ObjPos,prsc:2|IN-7152-XYZ;n:type:ShaderForge.SFN_Code,id:8387,x:31022,y:33118,varname:node_8387,prsc:2,code:ZgBsAG8AYQB0ACAAcwAgAD0AIABzAGkAbgAoAFIAbwB0AGEAdABpAG8AbgApADsADQAKACAAIAAgACAAZgBsAG8AYQB0ACAAYwAgAD0AIABjAG8AcwAoAFIAbwB0AGEAdABpAG8AbgApADsADQAKACAAIAAgACAAZgBsAG8AYQB0ACAAbwBuAGUAXwBtAGkAbgB1AHMAXwBjACAAPQAgADEALgAwACAALQAgAGMAOwANAAoADQAKACAAIAAgACAAQQB4AGkAcwAgAD0AIABuAG8AcgBtAGEAbABpAHoAZQAoAEEAeABpAHMAKQA7AA0ACgAgACAAIAAgAGYAbABvAGEAdAAzAHgAMwAgAHIAbwB0AF8AbQBhAHQAIAA9ACAADQAKACAAIAAgACAAewAgACAAIABvAG4AZQBfAG0AaQBuAHUAcwBfAGMAIAAqACAAQQB4AGkAcwAuAHgAIAAqACAAQQB4AGkAcwAuAHgAIAArACAAYwAsACAAbwBuAGUAXwBtAGkAbgB1AHMAXwBjACAAKgAgAEEAeABpAHMALgB4ACAAKgAgAEEAeABpAHMALgB5ACAALQAgAEEAeABpAHMALgB6ACAAKgAgAHMALAAgAG8AbgBlAF8AbQBpAG4AdQBzAF8AYwAgACoAIABBAHgAaQBzAC4AegAgACoAIABBAHgAaQBzAC4AeAAgACsAIABBAHgAaQBzAC4AeQAgACoAIABzACwADQAKACAAIAAgACAAIAAgACAAIABvAG4AZQBfAG0AaQBuAHUAcwBfAGMAIAAqACAAQQB4AGkAcwAuAHgAIAAqACAAQQB4AGkAcwAuAHkAIAArACAAQQB4AGkAcwAuAHoAIAAqACAAcwAsACAAbwBuAGUAXwBtAGkAbgB1AHMAXwBjACAAKgAgAEEAeABpAHMALgB5ACAAKgAgAEEAeABpAHMALgB5ACAAKwAgAGMALAAgAG8AbgBlAF8AbQBpAG4AdQBzAF8AYwAgACoAIABBAHgAaQBzAC4AeQAgACoAIABBAHgAaQBzAC4AegAgAC0AIABBAHgAaQBzAC4AeAAgACoAIABzACwADQAKACAAIAAgACAAIAAgACAAIABvAG4AZQBfAG0AaQBuAHUAcwBfAGMAIAAqACAAQQB4AGkAcwAuAHoAIAAqACAAQQB4AGkAcwAuAHgAIAAtACAAQQB4AGkAcwAuAHkAIAAqACAAcwAsACAAbwBuAGUAXwBtAGkAbgB1AHMAXwBjACAAKgAgAEEAeABpAHMALgB5ACAAKgAgAEEAeABpAHMALgB6ACAAKwAgAEEAeABpAHMALgB4ACAAKgAgAHMALAAgAG8AbgBlAF8AbQBpAG4AdQBzAF8AYwAgACoAIABBAHgAaQBzAC4AegAgACoAIABBAHgAaQBzAC4AegAgACsAIABjAA0ACgAgACAAIAAgAH0AOwANAAoAIAAgACAAIABPAHUAdAAgAD0AIABtAHUAbAAoAHIAbwB0AF8AbQBhAHQALAAgACAASQBuACkAOwA=,output:2,fname:RotationAboutAxis,width:344,height:396,input:2,input:2,input:0,input_1_label:In,input_2_label:Axis,input_3_label:Rotation|A-7152-XYZ,B-8783-OUT,C-272-OUT;n:type:ShaderForge.SFN_Vector3,id:8783,x:30758,y:33051,varname:node_8783,prsc:2,v1:-1,v2:0,v3:0;n:type:ShaderForge.SFN_Vector1,id:272,x:30758,y:32984,varname:node_272,prsc:2,v1:90;n:type:ShaderForge.SFN_Add,id:8732,x:31633,y:32749,varname:node_8732,prsc:2|A-2778-OUT,B-546-OUT;n:type:ShaderForge.SFN_Add,id:9884,x:31843,y:32749,varname:node_9884,prsc:2|A-8732-OUT,B-1922-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:8532,x:30758,y:32856,varname:node_8532,prsc:2;n:type:ShaderForge.SFN_Set,id:5432,x:30747,y:32765,varname:worldPos,prsc:2|IN-8532-XYZ;n:type:ShaderForge.SFN_Get,id:8711,x:31882,y:33259,varname:node_8711,prsc:2|IN-3072-OUT;n:type:ShaderForge.SFN_Get,id:6516,x:31403,y:33006,varname:node_6516,prsc:2|IN-5432-OUT;n:type:ShaderForge.SFN_Subtract,id:1922,x:31633,y:32881,varname:node_1922,prsc:2|A-6516-OUT,B-8711-OUT;n:type:ShaderForge.SFN_ComponentMask,id:8915,x:32035,y:32749,varname:node_8915,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9884-OUT;n:type:ShaderForge.SFN_Add,id:4210,x:32297,y:32624,varname:node_4210,prsc:2|A-7383-OUT,B-8915-G;n:type:ShaderForge.SFN_Slider,id:7383,x:31792,y:32514,ptovrint:False,ptlb:FillAmount,ptin:_FillAmount,varname:node_7383,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5128205,max:1;n:type:ShaderForge.SFN_Step,id:5153,x:32463,y:32669,varname:node_5153,prsc:2|A-4210-OUT,B-1293-OUT;n:type:ShaderForge.SFN_Vector1,id:1293,x:32297,y:32744,varname:node_1293,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Slider,id:2109,x:31882,y:33090,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:node_2109,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4857689,max:1;n:type:ShaderForge.SFN_Add,id:7886,x:32198,y:32942,varname:node_7886,prsc:2|A-8915-G,B-2109-OUT;n:type:ShaderForge.SFN_Clamp01,id:1225,x:32361,y:32942,varname:node_1225,prsc:2|IN-7886-OUT;n:type:ShaderForge.SFN_Color,id:7013,x:31461,y:33230,ptovrint:False,ptlb:TopColor,ptin:_TopColor,varname:node_7013,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:2389,x:31461,y:33398,ptovrint:False,ptlb:BottomColor,ptin:_BottomColor,varname:node_2389,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:1,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:8007,x:31753,y:33246,varname:node_8007,prsc:2|A-7013-RGB,B-2389-RGB,T-1225-OUT;n:type:ShaderForge.SFN_FaceSign,id:9939,x:31728,y:33468,varname:node_9939,prsc:2,fstp:0;n:type:ShaderForge.SFN_If,id:741,x:31899,y:33496,varname:node_741,prsc:2|A-9939-VFACE,B-2113-OUT,GT-2113-OUT,EQ-2113-OUT,LT-2113-OUT;n:type:ShaderForge.SFN_Vector1,id:2113,x:31728,y:33605,varname:node_2113,prsc:2,v1:1;n:type:ShaderForge.SFN_ObjectPosition,id:2967,x:30423,y:32451,varname:node_2967,prsc:2;n:type:ShaderForge.SFN_FragmentPosition,id:3238,x:30422,y:32616,varname:node_3238,prsc:2;n:type:ShaderForge.SFN_Subtract,id:1747,x:30597,y:32522,varname:node_1747,prsc:2|A-2967-XYZ,B-3238-XYZ;proporder:9851-5215-4649-7383;pass:END;sub:END;*/

Shader "Unlit/UnlitLiquid" {
    Properties {
        _opacity ("opacity", Range(0, 1)) = 1
        _WobbleX ("WobbleX", Float ) = 0
        _WobbleY ("WobbleY", Float ) = 0
        _FillAmount ("FillAmount", Range(0, 1)) = 0.5128205
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
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
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _WobbleX;
            uniform float _WobbleY;
            float3 RotationAboutAxis( float3 In , float3 Axis , float Rotation ){
            float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;
            
                Axis = normalize(Axis);
                float3x3 rot_mat = 
                {   one_minus_c * Axis.x * Axis.x + c, one_minus_c * Axis.x * Axis.y - Axis.z * s, one_minus_c * Axis.z * Axis.x + Axis.y * s,
                    one_minus_c * Axis.x * Axis.y + Axis.z * s, one_minus_c * Axis.y * Axis.y + c, one_minus_c * Axis.y * Axis.z - Axis.x * s,
                    one_minus_c * Axis.z * Axis.x - Axis.y * s, one_minus_c * Axis.y * Axis.z + Axis.x * s, one_minus_c * Axis.z * Axis.z + c
                };
                Out = mul(rot_mat,  In);
            }
            
            uniform float _FillAmount;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
////// Lighting:
                float3 finalColor = 0;
                float3 worldPos = i.posWorld.rgb;
                float3 ObjPos = objPos.rgb;
                float2 node_8915 = (((_WobbleX*objPos.rgb)+(_WobbleY*RotationAboutAxis( objPos.rgb , float3(-1,0,0) , 90.0 )))+(worldPos-ObjPos)).rg;
                fixed4 finalRGBA = fixed4(finalColor,step((_FillAmount+node_8915.g),0.6));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
