Shader "Custom/NegativeGrayscale" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
		//_Detail ("Detail", 2D) = "gray" {}
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        //#pragma surface frag Lambert
        #pragma surface frag NoLight

		half4 LightingNoLight (SurfaceOutput s, half3 lightDir, half atten) {
		  half4 c;
		  c.rgb = s.Albedo;
		  c.a = s.Alpha;
		  return c;
		}

		struct Input {
		  //float2 uv_MainTex;
		  float4 screenPos;
		};
		sampler2D _MainTex;
		//sampler2D _Detail;        

        void frag(Input IN, inout SurfaceOutput o) {
        	float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
          	screenUV = float2(1.0f-screenUV.x,screenUV.y);
          	screenUV *= float2(.625f,.9375f);
          	o.Albedo = tex2D (_MainTex, screenUV).rgb;
        }
        
        ENDCG
    } 
    FallBack "Diffuse"
}