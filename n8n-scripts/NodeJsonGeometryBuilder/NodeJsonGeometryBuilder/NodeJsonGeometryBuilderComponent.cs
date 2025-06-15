using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace NodeJsonGeometryBuilder
{
    public class NodeJsonGeometryBuilderComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public NodeJsonGeometryBuilderComponent()
          : base("  ", "Nickname",
            "Description",
            "Category", "Subcategory")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("FilePath", "P", "File path for Json File", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGeometryParameter("Mesh", "M", "Mesh geometry from Json", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string filePath = "";
            List<Mesh> meshList = new List<Mesh>();
            if (DA.GetData(0, ref filePath))
            {
                //var path = System.IO.Path.GetFullPath(filePath);
                meshList = LoadMeshesFromJson(File.ReadAllText(filePath));
            }

            DA.SetDataList(0, meshList);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("1843f629-b7cb-4d6f-895d-0299bd711825");

        // Define C# classes to match the JSON structure
        public class RootObject
        {
            public List<double> origin { get; set; }
            public List<GeometryGroup> geometry { get; set; }
        }

        public class GeometryGroup
        {
            public string type { get; set; }
            public string geometryType { get; set; }
            public List<MeshData> meshes { get; set; }
        }

        public class MeshData
        {
            public List<double> vertices { get; set; }
        }

        public List<Mesh> LoadMeshesFromJson(string json)
        {
            var rootObjects = JsonConvert.DeserializeObject<List<RootObject>>(json);
            var meshes = new List<Mesh>();
            foreach (var root in rootObjects)
            {
                foreach (var group in root.geometry)
                {
                    if (group.geometryType == "meshes")
                    {
                        foreach (var meshData in group.meshes)
                        {
                            var mesh = new Mesh();
                            for (int i = 0; i < meshData.vertices.Count; i += 3)
                            {
                                var vertex = new Point3d(
                                    meshData.vertices[i],
                                    meshData.vertices[i + 2],
                                    meshData.vertices[i + 1]);
                                mesh.Vertices.Add(vertex);
                            }
                            for (int i = 0; i <= mesh.Vertices.Count - 3; i += 3)
                            {
                                mesh.Faces.AddFace(i, i + 1, i + 2);
                            }
                            mesh.Normals.ComputeNormals();
                            mesh.Compact();
                            string log = "";
                            mesh.IsValidWithLog(out log);
                            meshes.Add(mesh);
                        }
                    }
                }
            }
            return meshes;
        }
    }
}