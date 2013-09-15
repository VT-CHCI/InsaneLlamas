Shader "Custom/ConstantColor" {
	Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _Detail ("Detail", 2D) = "gray" {}
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert

//      half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten) {
//          half NdotL = dot (s.Normal, lightDir);
//          half diff = NdotL * 0.5 + 0.5;
//          half4 c;
//          c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten * 2);
//          c.a = s.Alpha;
//          return c;
//     }

      struct Input {
          float2 uv_MainTex;
          float4 screenPos;
      };
      sampler2D _MainTex;
      sampler2D _Detail;
      
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
          screenUV *= float2(.625f,.625f);
          o.Albedo *= tex2D (_Detail, screenUV).rgb * 2;
      }
      ENDCG
    } 
    Fallback "Diffuse"
}
