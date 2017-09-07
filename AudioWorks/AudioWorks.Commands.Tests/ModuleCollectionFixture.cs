using AudioWorks.Api.Tests;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    [CollectionDefinition("Module")]
    public sealed class ModuleCollectionFixture
        : ICollectionFixture<ModuleFixture>, ICollectionFixture<ExtensionFixture>
    {
    }
}
