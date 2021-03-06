﻿#version 430 core 

//Attribute list variables
layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec2 vTextCoords;

//Out variables
out vec2 passTextCoords;

//Uniform variables
uniform mat4 inTranformationMatrix;
uniform mat4 inProjectionMatrix;
uniform mat4 inViewMatrix;

void main(void)
{
	gl_Position = inProjectionMatrix * inViewMatrix * inTranformationMatrix * vec4(vPosition, 1.0);
	passTextCoords = vTextCoords;
}