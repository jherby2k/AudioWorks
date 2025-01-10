/* Copyright © 2020 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit.Sdk;
using Xunit.v3;

namespace AudioWorks.Api.Tests
{
    public sealed class TestOrderer : ITestCollectionOrderer
    {
        public IReadOnlyCollection<TTestCollection> OrderTestCollections<TTestCollection>(
            IReadOnlyCollection<TTestCollection> testCollections) where TTestCollection : ITestCollection
        {
            var collections = testCollections.ToArray();
            return new ReadOnlyCollection<TTestCollection>(collections
                .Where(c => c.TestCollectionDisplayName.Equals("Setup Tests", StringComparison.OrdinalIgnoreCase))
                .Concat(
                    collections.Where(c =>
                        !c.TestCollectionDisplayName.Equals("Setup Tests", StringComparison.OrdinalIgnoreCase)))
                .ToList());
        }
    }
}
