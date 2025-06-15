using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace NodeJsonGeometryBuilder
{
    public class NodeJsonGeometryBuilderInfo : GH_AssemblyInfo
    {
        public override string Name => "NodeJsonGeometryBuilder";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("0e5e7f7b-153d-4758-b552-fe20c35f674d");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}