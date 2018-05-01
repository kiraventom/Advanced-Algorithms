﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A weighted graph vertex
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TW"></typeparam>
    public class WeightedGraphVertex<T, TW> where TW : IComparable
    {
        public T Value { get; private set; }

        public System.Collections.Generic.Dictionary<WeightedGraphVertex<T, TW>, TW> Edges { get; set; }

        public WeightedGraphVertex(T value)
        {
            Value = value;
            Edges = new System.Collections.Generic.Dictionary<WeightedGraphVertex<T, TW>, TW>();

        }

    }

    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    /// <summary>
    /// A weighted graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TW"></typeparam>
    public class WeightedGraph<T, TW> where TW : IComparable
    {
        public int VerticesCount => Vertices.Count;
        internal System.Collections.Generic.Dictionary<T, WeightedGraphVertex<T, TW>> Vertices { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WeightedGraph()
        {
            Vertices = new System.Collections.Generic.Dictionary<T, WeightedGraphVertex<T, TW>>();
        }


        /// <summary>
        /// return a reference vertex  to start traversing Vertices
        /// O(1) complexity
        /// </summary>
        public WeightedGraphVertex<T, TW> ReferenceVertex
        {
            get
            {
                using (var enumerator = Vertices.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        return enumerator.Current.Value;
                    }
                }

                return null;
            }
        }

      
        /// <summary>
        /// Add a new vertex to this graph
        /// O(1) complexity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WeightedGraphVertex<T, TW> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new WeightedGraphVertex<T, TW>(value);

            Vertices.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// remove given vertex from this graph
        /// O(V) complexity
        /// </summary>
        /// <param name="value"></param>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!Vertices.ContainsKey(value))
            {
                throw new Exception("Vertex not in this graph.");
            }


            foreach (var vertex in Vertices[value].Edges)
            {
                vertex.Key.Edges.Remove(Vertices[value]);
            }

            Vertices.Remove(value);
        }

        /// <summary>
        /// Add a new edge to this graph with given weight 
        /// and between given source and destination vertex
        /// O(1) complexity
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="weight"></param>
        public void AddEdge(T source, T dest, TW weight)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }


            Vertices[source].Edges.Add(Vertices[dest], weight);
            Vertices[dest].Edges.Add(Vertices[source], weight);
        }

        /// <summary>
        /// Remove given edge
        /// O(1) complexity
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public void RemoveEdge(T source, T dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!Vertices[source].Edges.ContainsKey(Vertices[dest]) 
                || !Vertices[dest].Edges.ContainsKey(Vertices[source]))
            {
                throw new Exception("Edge do not exists.");
            }

            Vertices[source].Edges.Remove(Vertices[dest]);
            Vertices[dest].Edges.Remove(Vertices[source]);
        }

        /// <summary>
        /// Do we have an edge between given source and destination
        /// O(1) complexity
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public bool HasEdge(T source, T dest)
        {
            if(!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new ArgumentException("source or destination is not in this graph.");
            }

            return Vertices[source].Edges.ContainsKey(Vertices[dest])
                   && Vertices[dest].Edges.ContainsKey(Vertices[source]);

        }


        public List<Tuple<T, TW>> GetAllEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return Vertices[vertex].Edges.Select(x => new Tuple<T, TW>(x.Key.Value, x.Value)).ToList();
        }

        /// <summary>
        /// Find the Vertex with given value
        ///  O(1) complexity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WeightedGraphVertex<T, TW> FindVertex(T value)
        {
            if (Vertices.ContainsKey(value))
            {
                return Vertices[value];
            }

            return null;
        }
    }
}
