﻿using System.Collections.Generic;
using BufferTarget = OpenTK.Graphics.OpenGL4.BufferTarget;
using BufferUsageHint = OpenTK.Graphics.OpenGL4.BufferUsageHint;
using GL = OpenTK.Graphics.OpenGL4.GL;
using VertexAttribPointerType = OpenTK.Graphics.OpenGL4.VertexAttribPointerType;

namespace IntegralEngine
{
    public class RawMesh
    {
        private static List<int> vaos = new List<int>();
        private static List<int> vbos = new List<int>();

        private int vaoID;
        private int vertexCount;
        public RawMesh(int _vaoID, int _vertexCount)
        {
            vaoID = _vaoID;
            vertexCount = _vertexCount;
        }

        public int GetVaoID()
        {
            return vaoID;
        }

        public int GetVertexCount()
        {
            return vertexCount;
        }

        public static RawMesh LoadToVao(float[] positions, float[] textureCoordinates, int[] indices)
        {
            int vaoID = CreateVAO();
            BindIndicesBuffer(indices);
            StoreDataInAttributeList(0,3,positions);
            StoreDataInAttributeList(1, 2, textureCoordinates);
            UnbindVAO();
            return new RawMesh(vaoID, indices.Length);
        }

        private static int CreateVAO()
        {
            int vaoID;
            GL.GenVertexArrays(1, out vaoID);
            GL.BindVertexArray(vaoID);
            vaos.Add(vaoID);
            return vaoID;
        }

        private static void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }

        private static void StoreDataInAttributeList(int attributeIndex, int size, float[] data)
        {
            int vboID = 0;
            GL.GenBuffers(1,out vboID);
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float)*data.Length, data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeIndex,size,VertexAttribPointerType.Float,false,0,0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private static void BindIndicesBuffer(int[] indices)
        {
            int vboID = 0;
            GL.GenBuffers(1, out vboID);
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * indices.Length, indices, BufferUsageHint.StaticDraw);
        }
    

        public static void Cleanup()
        {
            foreach (int i in vaos)
            {
                GL.DeleteVertexArray(i);
            }
            foreach (int i in vbos)
            {
                GL.DeleteBuffer(i);
            }
        }
    }
}