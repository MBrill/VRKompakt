// Phong Shading in Unity

Shader "_Shaders/MBU/Phong" {

   Properties {
      _Color ("Diffuse Material Color", Color) = (1,1,1,1) 
      _SpecColor ("Specular Color", Color) = (1,1,1,1) 
      _Shininess ("Shininess", Float) = 16.0
   }

   SubShader {
      Pass {
         // shader pass für ambient light und erste Lichtquelle	  
         Tags { "LightMode" = "ForwardBase" } 
 
         CGPROGRAM
 
         // Vertex und Fragment Shader
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
		 // Farbe der Lichtquelle (enthalten in Lighting.cginc)
         uniform fixed4 _LightColor0;
 
         // Variablen für die Properties
         uniform fixed4 _Color;
         uniform fixed4 _SpecColor;
         uniform float _Shininess;
 
        // Input für den Vertex Shader 
         struct vertexInput {
             float4 vertex : POSITION;
             float3 normal : NORMAL;
         };
		 
		// Datenstruktur für den Austausch zwischen Vertex und Fragment Shader
        struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posWorld : TEXCOORD0;
            float3 normalDir : TEXCOORD1;
        };
 
        // Vertex Shader
		// Transformation in Weltkoordianten, 
		// Bearbeitung der Normale
		vertexOutput vert(vertexInput input) 
        {
            vertexOutput output;
 
            output.posWorld = mul(unity_ObjectToWorld, input.vertex);
            output.normalDir = normalize(
               mul(float4(input.normal, 0.0), unity_WorldToObject).xyz);
            output.pos = UnityObjectToClipPos(input.vertex);
			
            return output;
        }
 
        // Fragment Shader
        float4 frag(vertexOutput input) : COLOR
        {
		    // Vektor n im Beleuchtungsgesetz
			// Sicherstellen, dass die interpolierten Normalen auch Länge 1 haben!
            float3 normalDirection = normalize(input.normalDir);
            float3 viewDirection = normalize(
               _WorldSpaceCameraPos - input.posWorld.xyz);
            float3 lightDirection;
            float attenuation;
 
            // In ForwardBase werden nur Richtungslichtquellen verarbeitet!
            attenuation = 1.0; 
            lightDirection = normalize(_WorldSpaceLightPos0.xyz);


            // UNITY_LIGHTMODEL_AMBIENT.rgb: Ambiente Lichtfarbe (legacy Variable)
            float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;
            // Diffuser Anteil berechnen mit der ersten Lichtquelle
            float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb
                                       * max(0.0, dot(normalDirection, lightDirection));
 
            float3 specularReflection;
            if (dot(normalDirection, lightDirection) < 0.0) 
            {
               // Rückseite!
			   specularReflection = float3(0.0, 0.0, 0.0); 
            }
            else
            {
               // Wir sind auf der Vorderseite
			   // Den Phong-Exponenten anwenden
			   specularReflection = attenuation * _LightColor0.rgb 
                                    * _SpecColor.rgb * pow(max(0.0, dot(
                                      reflect(-lightDirection, normalDirection), 
                                      viewDirection)), _Shininess);
            }
 
            return float4(ambientLighting + diffuseReflection + specularReflection, 1.0);  
        }
 
         ENDCG
      }
 
      Pass {
        // shader pass für weitere Lichtquellen
        // Dieser Pass wird für jede weitere Lichtquelle ausgeführt!
        Tags { "LightMode" = "ForwardAdd" }
		// Additives Blending
        Blend One One
 
        CGPROGRAM
 
        // Vertex und Fragment Shader
        #pragma vertex vert  
        #pragma fragment frag 
 
        #include "UnityCG.cginc"
        uniform float4 _LightColor0; 
 
        // Properties aus dem Material
        uniform float4 _Color; 
        uniform float4 _SpecColor; 
        uniform float _Shininess;
 
        // Datenstruktur für die Eingabe in den Vertex Shader
        struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
        };
        
		// Datenstruktur für den Austausch zwischen Vertex und Fragment Shader
    	struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posWorld : TEXCOORD0;
            float3 normalDir : TEXCOORD1;
        };
 
        // Vertex Shader
        vertexOutput vert(vertexInput input) 
        {
            vertexOutput output;
 
            output.posWorld = mul(unity_ObjectToWorld, input.vertex);
            output.normalDir = normalize(mul(float4(input.normal, 0.0), unity_WorldToObject).xyz);              
            output.pos = UnityObjectToClipPos(input.vertex);
            return output;
        }
 
        // Fragment Shader
        float4 frag(vertexOutput input) : COLOR
        {
            // Der Vektor n: sicher stellen, dass der interpolierte Vektor normiert ist!
			float3 normalDirection = normalize(input.normalDir);
 
            float3 viewDirection = normalize(_WorldSpaceCameraPos - input.posWorld.xyz);
               
            float3 lightDirection;
            float attenuation;
 
            if (_WorldSpaceLightPos0.w == 0.0)
            {
               // Richtungslichtquelle
			   attenuation = 1.0;
               lightDirection = normalize(_WorldSpaceLightPos0.xyz);
            } 
            else 
            {
               // Punktlicht oder Spotlight
			   float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - input.posWorld.xyz;
               float distance = length(vertexToLightSource);
               attenuation = 1.0 / distance; 
               lightDirection = normalize(vertexToLightSource);
            }
 
            float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb
                                        * max(0.0, dot(normalDirection, lightDirection));
 
            float3 specularReflection;
            if (dot(normalDirection, lightDirection) < 0.0) 
            {
               specularReflection = float3(0.0, 0.0, 0.0); 
            }
            else
            {
               specularReflection = attenuation * _LightColor0.rgb 
                                    * _SpecColor.rgb * pow(max(0.0, dot(
                                   reflect(-lightDirection, normalDirection), 
                                   viewDirection)), _Shininess);
            }
 
            return float4(diffuseReflection + specularReflection, 1.0);

         }
 
         ENDCG
      }
   }
   Fallback "Specular"
}