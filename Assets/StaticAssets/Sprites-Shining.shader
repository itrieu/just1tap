// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprite/Sprite Shininess"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
		_Color	("Color", Color) = (1,1,1,1)
		_Shininess ("Shininess", Float) = 0
	}
	
	SubShader
	{
		LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"RenderEffect"="Colored"
		}
		
//		Cull Off
		Lighting Off
//		ZWrite Off
		Fog { Mode Off }
		Offset -1, -1
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			 CGPROGRAM

        #pragma vertex vert
        #pragma fragment frag


        #include "UnityCG.cginc"

        float4 _Color;
        sampler2D _MainTex;
        float _Shininess;
        
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        float4 _MainTex_ST;

        v2f vert (appdata_base v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos (v.vertex);
            o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
            return o;
        }

        half4 frag (v2f i) : COLOR
        {
//            half4 texcol = tex2D (_MainTex, i.uv);
//
//			texcol = lerp(texcol, _Color, _Shininess) * texcol.a;
//
//            return texcol;
			half4 o = tex2D (_MainTex, i.uv);
			half al = o.a;
            half g = (o.r + o.g + o.b) * 0.3; 
        	o =  (_Color * g);
        	o = lerp(o, _Color, _Shininess) * al;
            return o;

        }
        ENDCG
		}
	}
}
