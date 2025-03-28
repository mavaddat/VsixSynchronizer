using Microsoft.VisualStudio.TextTemplating.VSHost;

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VsixSynchronizer
{
    [Guid("62949701-e45c-41fe-8304-eaf34569010d")]
    public sealed class VsixManifestGenerator : BaseCodeGeneratorWithSite
    {
        public const string Name = nameof(VsixManifestGenerator);
        public const string Description = "Generates .NET source code for .vsixmanifest files.";

        public override string GetDefaultExtension()
        {
            return ".cs";
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            VsixManifest manifest = VsixManifestParser.FromManifest(inputFileContent);
            string code = GenerateClass(manifest);
            return Encoding.UTF8.GetBytes(code);
        }

        private string GenerateClass(VsixManifest manifest)
        {
            var sb = new StringBuilder();
            sb.AppendHeader();

            sb.AppendLine($"namespace {FileNamespace}");
            sb.AppendLine("{");
            sb.AppendLine($"    internal sealed partial class Vsix");
            sb.AppendLine($"    {{");
            sb.AppendLine($"        public const string Id = \"{EscapeString(manifest.ID)}\";");
            sb.AppendLine($"        public const string Name = \"{EscapeString(manifest.Name)}\";");
            sb.AppendLine($"        public const string Description = @\"{EscapeVerbatimString(manifest.Description)}\";");
            sb.AppendLine($"        public const string Language = \"{EscapeString(manifest.Language)}\";");
            sb.AppendLine($"        public const string Version = \"{EscapeString(manifest.Version)}\";");
            sb.AppendLine($"        public const string Author = \"{EscapeString(manifest.Author)}\";");
            sb.AppendLine($"        public const string Tags = \"{EscapeString(manifest.Tags)}\";");
            sb.AppendLine($"        public const bool IsPreview = {manifest.IsPreview.ToString().ToLowerInvariant()};");
            sb.AppendLine($"    }}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string EscapeString(string value) => value?.Replace("\\", "\\\\").Replace("\"", "\\\"");

        private static string EscapeVerbatimString(string value) => value?.Replace("\"", "\"\"");
    }
}
