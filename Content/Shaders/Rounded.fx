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
    
    float smoothness = 0.7;

    float min = Radius - smoothness;
    float max = Radius + smoothness;
    
    float smoothBottomLeft = length(pos - float2(Radius, Radius));
    float smoothTopLeft = length(pos - float2(Radius, yMax));
    float smoothTopRight = length(pos - float2(xMax, yMax));
    float smoothBottomRight = length(pos - float2(xMax, Radius));

    if (pos.x < Radius && pos.y < Radius) {
    color.a -= smoothstep(min, max, smoothBottomLeft);
    }
    else if (pos.x < Radius && pos.y > yMax){
        color.a -= smoothstep(min, max, smoothTopLeft);
    }
    else if (pos.x > xMax && pos.y > yMax){
        color.a -= smoothstep(min, max, smoothTopRight);
    }
    else if (pos.x > xMax && pos.y < Radius){
        color.a -= smoothstep(min, max, smoothBottomRight);
    }
    return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};