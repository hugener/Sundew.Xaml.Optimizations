// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FreezeResourceSettingsTests.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.UnitTests.Optimizations.Freezing
{
    using FluentAssertions;
    using Sundew.Xaml.Optimizations.Freezing;
    using Xunit;

    public class FreezeResourceSettingsTests
    {
        [Fact]
        public void DeserializeObject_When_IncludeFrameworkTypesIsNotSpecified_Then_ResultShouldBeTrue()
        {
            var text = "{}";

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<FreezeResourceSettings>(text);

            result.IncludeFrameworkTypes.Should().BeTrue();
        }
    }
}