float4x4 World;
float4x4 View;
float4x4 Projection;

texture tex;
sampler2D sam = sampler_state
{
	texture = < tex > ;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;

};

struct VertexShaderOutput
{
    float4 Position : POSITION0;

};

VertexShaderOutput VS(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    return output;
}

float4 PS(VertexShaderOutput input) : COLOR0
{
    return float4(1, 0, 0, 1);
}

technique MainTechnique
{
    pass MainPass
    {
        VertexShader = compile vs_2_0 VS();
        PixelShader = compile ps_2_0 PS();
    }
}
