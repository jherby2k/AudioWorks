using System;
using System.Composition;
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute must implement <see cref="IPrerequisiteValidator"/>.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [PublicAPI, MeansImplicitUse, BaseTypeRequired(typeof(IPrerequisiteValidator))]
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class PrerequisiteValidatorExportAttribute : ExportAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrerequisiteValidatorExportAttribute"/> class.
        /// </summary>
        public PrerequisiteValidatorExportAttribute()
            : base(typeof(IPrerequisiteValidator))
        {
        }
    }
}
