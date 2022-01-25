// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//It belongs to the Custom directory and is named GouraudShader
Shader "Shaders/GouraudShader"
{
    Properties
    {
        _MainColor ("Color", Color) = (1, 1, 1, 1)
        //I defined the position of a light source myself
        _LightPos ("LightPosition", Vector) = (-0.5, 5.5, -0.5, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : TEXCOORD0;
            };

            float4 _MainColor;
            float4 _LightPos;
            
            //The point of Gouraud Shading is that the light is calculated in the vert function
            v2f vert (float4 vertex : POSITION, float3 normal : NORMAL)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(vertex);

                //Convert the coordinate information and normal information of the point we handwritten before to the world coordinate system for calculation.
                float3 worldPos = mul(UNITY_MATRIX_M, vertex);
                float3 worldNor = mul(UNITY_MATRIX_M, normal);

                //Direction of light
                float3 lightDir = normalize(_LightPos - worldPos);
                //Distance, used to calculate the attenuation of light
                float3 dist = distance(_LightPos, worldPos);

                //Calculation of Diffuse Light.
                float lightPor = max(0, dot(worldNor, lightDir));
                //Attenuation coefficient, divided by 2 is a mysterious reason
                float atten = 2 / pow(dist, 2);

                //The final color displayed on the screen is interpolated and passed to the fragment function to display the color.
                o.color = _MainColor * lightPor * atten;

                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                //Color after interpolation
                return i.color;
            }
            ENDCG
        }
    }
}