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

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float width;
	float height;
	SpriteTexture.GetDimensions(width, height);
	float2 pos = input.TextureCoordinates.xy * float2(width, height);
	
	float halfTex = width/2;
	bool isLeft = pos.x < halfTex;
    float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    if (input.TextureCoordinates.x < 0.5) {
        color.gb = color.r;
    }
    else {
    color.gb = color.gb;
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