﻿using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Linq;
using ProBuilder2.Common;
using UnityEngine;

namespace ProBuilder2.Common
{}
	
/**
 *
 */
namespace ProBuilder2.UpgradeKit
{
	[Serializable()]		
	public class pb_SerializableObject : ISerializable
	{
		// pb_Object
		[SerializeField] private Vector3[] 	vertices;
		[SerializeField] private Vector2[] 	uv;
		[SerializeField] private Color[]	color;
		[SerializeField] private pb_Face[] 	faces;
		[SerializeField] private int[][] 	sharedIndices;
		[SerializeField] private int[][] 	sharedIndicesUV;
		[SerializeField] private bool 		userCollisions;

		public Vector3[] 	GetVertices() { return vertices; }
		public Vector2[] 	GetUVs() { return uv; }
		public Color[]		GetColors() { return color; }
		public pb_Face[] 	GetFaces() { return faces; }
		public int[][]		GetSharedIndices() { return sharedIndices; }
		public int[][]		GetSharedIndicesUV() { return sharedIndicesUV; }
		public bool 		GetUserCollisions() { return userCollisions; }

		public pb_SerializableObject(pb_Object pb)
		{
			this.vertices = pb.vertices;

			// Make sure the mesh is valid, and in sync with current pb_Object
			if(pb.msh == null || pb.msh.vertexCount != pb.vertexCount)
			{
				pb_UpgradeKitUtils.RebuildMesh(pb);
			}

			this.uv = pb.msh != null ? pb.msh.uv : null;

			if(pb.msh != null && pb.msh.colors != null && pb.msh.colors.Length == pb.vertexCount)
			{
				this.color = pb.msh.colors;
			}
			else
			{
				this.color = new Color[pb.vertexCount];
				for(int i = 0; i < this.color.Length; i++)
					this.color[i] = Color.white;
			}
			this.faces = pb.faces;
			this.sharedIndices = (int[][])pb.GetSharedIndices().ToArray();

			PropertyInfo prop_uv = pb.GetType().GetProperty("sharedIndicesUV", BindingFlags.Instance | BindingFlags.Public);

			if(prop_uv != null)
			{
				var val = prop_uv.GetValue(pb, null);

				if(val != null)
				{
					pb_IntArray[] sharedUvs = (pb_IntArray[])val;
					this.sharedIndicesUV =  (int[][])sharedUvs.ToArray();
				}
				else
				{
					this.sharedIndicesUV = new int[0][];
				}
			}
			else
			{
				this.sharedIndicesUV = new int[0][];
			}

			PropertyInfo prop_userCollisions = pb.GetType().GetProperty("userCollisions", BindingFlags.Instance | BindingFlags.Public);
			userCollisions = prop_userCollisions == null ? false : (bool) prop_userCollisions.GetValue(pb, null);
		}

		public void Print()
		{
			Debug.Log(	"vertices: " + vertices.ToFormattedString(", ") +
						"\nuv: " + uv.ToFormattedString(", ") +
						// "\nsharedIndices: " + ((pb_IntArray[])sharedIndices.ToPbIntArray()).ToFormattedString(", ") +
						"\nfaces: " + faces.ToFormattedString(", ")
						);
		}

		public bool Equals(pb_SerializableObject other)
		{
			return vertices.SequenceEqual(other.vertices) &&
					uv.SequenceEqual(other.uv) && 
					color.SequenceEqual(other.color) &&
					pb_UpgradeKitUtils.FacesAreEqual(faces, other.faces);
		}

		// OnSerialize
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// pb_object
			info.AddValue("vertices", 			System.Array.ConvertAll(vertices, x => (pb_Vector3)x),				typeof(pb_Vector3[]));
			info.AddValue("uv", 				System.Array.ConvertAll(uv, x => (pb_Vector2)x), 					typeof(pb_Vector2[]));
			info.AddValue("color", 				System.Array.ConvertAll(color, x => (pb_Color)x), 					typeof(pb_Color[]));
			info.AddValue("faces", 				System.Array.ConvertAll(faces, x => new pb_SerializableFace(x)),	typeof(pb_SerializableFace[]));
			info.AddValue("sharedIndices", 		sharedIndices, 														typeof(int[][]));
			info.AddValue("sharedIndicesUV",	sharedIndicesUV, 													typeof(int[][]));
			info.AddValue("userCollisions",		userCollisions, 													typeof(bool));
		}

		// The pb_SerializableObject constructor is used to deserialize values. 
		public pb_SerializableObject(SerializationInfo info, StreamingContext context)
		{
			/// Vertices
			pb_Vector3[] pb_vertices = (pb_Vector3[]) info.GetValue("vertices", typeof(pb_Vector3[]));
			this.vertices = System.Array.ConvertAll(pb_vertices, x => (Vector3)x);
			
			/// UVs
			pb_Vector2[] pb_uv = (pb_Vector2[]) info.GetValue("uv", typeof(pb_Vector2[]));
			this.uv = System.Array.ConvertAll(pb_uv, x => (Vector2)x);
			
			/// Colors
			pb_Color[] pb_color = (pb_Color[]) info.GetValue("color", typeof(pb_Color[]));
			this.color = System.Array.ConvertAll(pb_color, x => (Color)x);

			/// Faces
			pb_SerializableFace[] pb_faces = (pb_SerializableFace[]) info.GetValue("faces", typeof(pb_SerializableFace[]));
			this.faces = (pb_Face[]) System.Array.ConvertAll(pb_faces, x => (pb_Face)x);

			// Shared Indices
			this.sharedIndices = (int[][]) info.GetValue("sharedIndices", typeof(int[][]));

			// Shared Indices UV
			this.sharedIndicesUV = (int[][]) info.GetValue("sharedIndicesUV", typeof(int[][]));

			// User collisions
			this.userCollisions = (bool) info.GetValue("userCollisions", typeof(bool));
		}
	}
}