using AudioWorks.Api.Tests;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    [CollectionDefinition("Module")]
    public class ModuleCollectionFixture
        : ICollectionFixture<ModuleFixture>, ICollectionFixture<ExtensionFixture>
    {
    }
}
