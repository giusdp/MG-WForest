#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

int Width;
int Height;
int Radius;

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 pos = input.TextureCoordinates.xy * float2(Width, Height); 
    
    float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    
    float xMax = Width - Radius;
    
    float yMax = Height - Radius;
    
    float alphaValue = 1.0;
    float smoothness = 0.7;
    
    if (pos.x < Radius && pos.y < Radius) {
       alphaValue -= smoothstep(Radius - smoothness, Radius + smoothness, length(pos - float2(Radius, Radius) ) ); 
    }
    else if (pos.x < Radius && pos.y > yMax){
        alphaValue -= smoothstep(Radius - smoothness, Radius + smoothness, length(pos - float2(Radius, yMax)));
    }
    else if (pos.x > xMax && pos.y > yMax){
        alphaValue -= smoothstep(Radius - smoothness, Radius + smoothness, length(pos - float2(xMax, yMax)));
    }
    else if (pos.x > xMax && pos.y < Radius){
        alphaValue -= smoothstep(Radius - smoothness, Radius + smoothness, length(pos - float2(xMax, Radius)));
    }
    color.a *= alphaValue;
    return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};