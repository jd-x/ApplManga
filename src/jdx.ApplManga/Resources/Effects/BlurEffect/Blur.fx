sampler2D input : register(s0);

float2 upperLeftCorner : register(c0);
float2 lowerRightCorner : register(c1);

float PI = 3.1415;
float EPSILON = 0.0001;

/// <summary>Applies gaussian blur inside a rectangular area defined by the upper and lower points</summary>

float gaussianblur(float n) {
	float theta = 2.0f + EPSILON;	//float.Epsilon;

	return theta = (float)((1.0 / sqrt(2 * PI * theta)) * exp(-(n * n) / (2 * theta * theta)));
}

float4 main(float2 uv : TEXCOORD) : COLOR {
	if(uv.x < upperLeftCorner.x || uv.y < upperLeftCorner.y || uv.x > lowerRightCorner.y || uv.y > lowerRightCorner.y) {
		return tex2D(input, uv);
	}

	return gaussianblur(uv);
}
