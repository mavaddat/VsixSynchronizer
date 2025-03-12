using System.Text;

namespace VsixSynchronizer
{
    internal static class StringBuilderExtensions
    {
        static public StringBuilder AppendHeader(this StringBuilder builder)
        {
            builder.AppendLine("// ------------------------------------------------------------------------------");
            builder.AppendLine("// <auto-generated>");
            builder.Append($"//     This file was generated by {Vsix.Name} {Vsix.Version}");
            if (Vsix.IsPreview)
            {
                builder.Append("-pre");
            }
            builder.AppendLine();
            builder.AppendLine($"//     Available from https://marketplace.visualstudio.com/items?itemName=MadsKristensen.VsixSynchronizer64");
            builder.AppendLine("// </auto-generated>");
            builder.AppendLine("// ------------------------------------------------------------------------------");
            return builder;
        }
    }
}
