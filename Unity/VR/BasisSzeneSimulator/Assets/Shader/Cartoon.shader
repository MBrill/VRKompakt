// Cartoon Shader in Unity
// Portiert aus der Cg-Version aus
// Bender, Brill: Computergrafik, 
// Hanser Verlag, 2005, 2. Auflage, pp. 339 ff.

Shader "_BasisSzene/Cartoon" {

    Properties{
       _Color("Diffuse Material Color", Color) = (1,1,1,1)
       _Step_x_1("Step function x-value 1", float) = 0.1
       _Step_x_2("Step function x-value 2", float) = 0.6
       _Step_x_3("Step function x-value 3", float) = 0.85
       _Step_y_1("Step function y-value 1", float) = 0.2
       _Step_y_2("Step function y-value 2", float) = 0.5
       _Step_y_3("Step function y-value 3", float) = 0.85
    }

    SubShader{
      Pass {
         // shader pass für ambient light und erste Richtungslichtquelle	  
         Tags { "LightMode" = "ForwardBase" }

       CGPROGRAM

       // Vertex und Fragment Shader
       #pragma vertex vert  
       #pragma fragment frag 

       #include "UnityCG.cginc"

       // Variablen für die Properties
       uniform fixed4 _Color;
       uniform float _Step_x_1;
       uniform float _Step_x_2;
       uniform float _Step_x_3;
       uniform float _Step_y_1;
       uniform float _Step_y_2;
       uniform float _Step_y_3;
       // Step Function
       #include "stepFunction.cginc"

       // Input für den Vertex Shader 
       struct vertexInput {
         float4 vertex : POSITION;
         float3 normal : NORMAL;
       };

       // Datenstruktur für den Austausch zwischen Vertex und Fragment Shader
       // Wir realisieren alles im Vertex Shader, aber Shaderlab verlangt einen Fragment Shader.
       // Überprüfen ob das Keyword nointerpolation in CG funktioniert, das entspricht FLAT in GLSL.
       // Dann könnten wir das wieder mit Hilfe eines Vertex Shaders implementieren!
       // nointerpolation fixed3 diff : COLOR0;
       struct vertexOutput {
         float4 pos : SV_POSITION;
         float3 normalDir : TEXCOORD1;
       };

       // Vertex Shader
       // Transformation in Weltkoordianten, 
       // Bearbeitung der Normale
       vertexOutput vert(vertexInput input)
       {
           vertexOutput output;

           output.normalDir = normalize(mul(float4(input.normal, 0.0), unity_WorldToObject).xyz);
           output.pos = UnityObjectToClipPos(input.vertex);

           return output;
        }

       // Fragment Shader
       half4 frag(vertexOutput input) : COLOR
       {
           // Der Vektor n: sicher stellen, dass der interpolierte Vektor normiert ist!
           float3 normalDirection = normalize(input.normalDir);

           float3 lightDirection;

           // In ForwardBase wird nur eine Richtungslichtquelle verarbeitet!
           // Vektor l im Beleuchtungsgesetz
           lightDirection = normalize(_WorldSpaceLightPos0.xyz);

          // Faktor für den Cartoon Effekt berechnen
          float diffuse = max(0.0, dot(normalDirection, lightDirection));
          return step(diffuse) * _Color;
       }

      ENDCG
    }	//END Pass

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
           uniform float _Step_x_1;
           uniform float _Step_x_2;
           uniform float _Step_x_3;
           uniform float _Step_y_1;
           uniform float _Step_y_2;
           uniform float _Step_y_3;
           // Step Function
           #include "stepFunction.cginc"
           // Variablen für die Properties
           uniform fixed4 _Color;

           // Input für den Vertex Shader 
           struct vertexInput {
              float4 vertex : POSITION;
              float3 normal : NORMAL;
           };

           // Datenstruktur für den Austausch zwischen Vertex und Fragment Shader
           // Wir realisieren alles im Vertex Shader, aber Shaderlab verlangt einen Fragment Shader.
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
                output.normalDir = normalize(mul(float4(input.normal, 0.0), unity_WorldToObject).xyz);
                output.pos = UnityObjectToClipPos(input.vertex);

               return output;
            }

           // Fragment Shader
           half4 frag(vertexOutput input) : COLOR
           {
                // Der Vektor n: sicher stellen, dass der interpolierte Vektor normiert ist!
                float3 normalDirection = normalize(input.normalDir);
                float3 lightDirection;

                // Vektor l im Beleuchtungsgesetz
                if (_WorldSpaceLightPos0.w == 0.0)
                {
                     // Richtungslichtquelle
                     lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                }
                else
                {
                    // Punktlicht oder Spotlight
                    float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - input.posWorld.xyz;
                    lightDirection = normalize(vertexToLightSource);
                 }

                // Faktor für den Cartoon Effekt berechnen
                float diffuse = max(0.0, dot(normalDirection, lightDirection));
                return step(diffuse) * _Color;
            }

       ENDCG

     } // END Pass

  } 	// END SubShader

FallBack "Diffuse"
}