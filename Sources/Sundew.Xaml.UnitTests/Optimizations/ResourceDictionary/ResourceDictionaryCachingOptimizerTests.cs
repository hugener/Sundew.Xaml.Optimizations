// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceDictionaryCachingOptimizerTests.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Xaml.UnitTests.Optimizations.ResourceDictionary
{
    using System;
    using System.IO;
    using System.Xml.Linq;
    using FluentAssertions;
    using Sundew.Xaml.Optimization;
    using Sundew.Xaml.Optimization.Xml;
    using Sundew.Xaml.Optimizations.ResourceDictionary;
    using Xunit;

    public class ResourceDictionaryCachingOptimizerTests
    {
        public const string SundewXamlOptimizationWpfNamespace = "clr-namespace:Sundew.Xaml.Optimizations;assembly=Sundew.Xaml.Wpf";
        public static readonly XNamespace WpfPresentationNamespace = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        private readonly XamlPlatformInfo xamlPlatformInfo;

        public ResourceDictionaryCachingOptimizerTests()
        {
            this.xamlPlatformInfo = new XamlPlatformInfo(XamlPlatform.WPF, WpfPresentationNamespace, SundewXamlOptimizationWpfNamespace);
        }

        [Fact]
        public void ResourceDictionary_When_ThereIsNothingToOptimize_Then_ResultShouldBeSameAsInput()
        {
            var input = $@"<ResourceDictionary 
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" 
    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
    <ResourceDictionary.MergedDictionaries>
    </ResourceDictionary.MergedDictionaries>
    
    <Style TargetType=""{{x:Type ComboBox}}"">
        <Setter Property=""BorderThickness"" Value=""6""/>
    </Style>
</ResourceDictionary>";

            var xDocument = XDocument.Parse(input);
            var testee = new ResourceDictionaryCachingOptimizer(this.xamlPlatformInfo);

            var result = testee.Optimize(new FileInfo(@"c:\temp\sample.cs"), xDocument, new DirectoryInfo(Environment.CurrentDirectory));

            result.Error.XDocument.ToString().Should().Be(XDocument.Parse(input).ToString());
        }

        [Fact]
        public void Application_When_ThereIsOneMergedResourceDictionary_Then_ResultShouldBeExpectedResult()
        {
            var input = $@"<Application x:Class=""Sundew.Xaml.Sample.App""
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" 
    StartupUri=""MainWindow.xaml"">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source=""/Sundew.Xaml.Sample.Wpf;component/Controls.xaml"" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>";

            var expectedResult = $@"<Application x:Class=""Sundew.Xaml.Sample.App""
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" 
    xmlns:sxo=""clr-namespace:Sundew.Xaml.Optimizations;assembly=Sundew.Xaml.Wpf""
    StartupUri=""MainWindow.xaml"">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <sxo:ResourceDictionary Source=""/Sundew.Xaml.Sample.Wpf;component/Controls.xaml"" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>";

            var xDocument = XDocument.Parse(input);
            var testee = new ResourceDictionaryCachingOptimizer(this.xamlPlatformInfo);

            var result = testee.Optimize(new FileInfo(@"c:\temp\sample.cs"), xDocument, new DirectoryInfo(Environment.CurrentDirectory));

            result.Value.XDocument.ToString().Should().Be(XDocument.Parse(expectedResult).ToString());
        }
    }
}