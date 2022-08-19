// Sehr einfacher Shader, der einem Objekt einfach eine Farbe zuordnet
Shader "_Shaders/MBU/JustColor" 
{
	// Properties für das Material im Inspektor
	Properties 
	{
		 _MyColor("Color", Color) = (0.0, 1.0, 0.0, 1.0)
	}
	
	SubShader 
	{
		
		Pass
		{
			// shader pass für ambient light und erste Lichtquelle	  
            Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM

		    // Vertex und Fragment Shader
			#pragma vertex vert
			#pragma fragment frag

		    // Build-in Variablen aus Unity verwenden
			#include "UnityCG.cginc"

		    // Uniform-Variable, muss den gleichen Namen haben wie die Property
			uniform fixed4 _MyColor;
			
			// Datenstruktur für die Eingabe des Vertex Shaders
			struct vertexInput
			{
				float4 vertex : POSITION;
			};

			// Datenstruktur für die Weitergabe von Daten vom Vertex zum Fragment Shader
			struct fragmentInput
			{
				float4 pos : SV_POSITION;
				nointerpolation fixed4 color : COLOR0;
			};

			// Vertex Shader
			fragmentInput vert( vertexInput i )
			{
				fragmentInput o;
				o.pos = UnityObjectToClipPos( i.vertex);
				o.color = _MyColor;
				return o;
			}

			// Fragment Shader
			half4 frag( fragmentInput i ) : COLOR
			{
				return i.color;
			}
			ENDCG
		}	//END Pass
		
	} 	// END SubShader

	FallBack "Diffuse"
}
