// <copyright file="LocalizableResourceStringTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

using System.Globalization;
using System.Resources;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

public class LocalizableResourceStringTests
{
    [Fact(DisplayName = "The generic ToString() method must be overriden.")]
    public void ToStringMethod_MustBeOverriden()
    {
        // Arrange
        const string NameOfResource = "Resource1";
        ResourceManager resourceManager = GetTestResourceManagerInstance();
        var defaultCultureResourceSet = resourceManager.GetResourceSet(CustomResourceManager.DefaultCulture, false, false);
        var expectedString = defaultCultureResourceSet?.GetString(NameOfResource) ?? string.Empty;

        // Act
        LocalizableResourceString localizableResourceString = new(NameOfResource, resourceManager, typeof(CustomResourceManager));
        var actualString = localizableResourceString.ToString(null);

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The ToString() method with non-existing resource must return string.Empty.")]
    public void ToStringMethod_WithNonExistingResource_MustReturnStringEmpty()
    {
        // Arrange
        const string NameOfResource = "ResourceZZZ";
        ResourceManager resourceManager = GetTestResourceManagerInstance();
        var expectedString = string.Empty;

        // Act
        LocalizableResourceString localizableResourceString = new(NameOfResource, resourceManager, typeof(CustomResourceManager));
        var actualString = localizableResourceString.ToString(null);

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The ToString() method with arguments must format the resource string.")]
    public void ToStringMethod_WithArguments_MustFormatTheResourceString()
    {
        // Arrange
        const string NameOfResource = "ResourceWithArguments";
        const string ArgumentValue = "formatted";
        ResourceManager resourceManager = GetTestResourceManagerInstance();
        var defaultCultureResourceSet = resourceManager.GetResourceSet(CustomResourceManager.DefaultCulture, false, false);
        var resourceStringValue = defaultCultureResourceSet?.GetString(NameOfResource) ?? string.Empty;
        var expectedString = string.Format(CultureInfo.InvariantCulture, resourceStringValue, ArgumentValue);

        // Act
        LocalizableResourceString localizableResourceString = new(NameOfResource, resourceManager, typeof(CustomResourceManager), ArgumentValue);
        var actualString = localizableResourceString.ToString(null);

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The Equals() method must return true")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        const string NameOfResource = "Resource1";
        ResourceManager resourceManager = GetTestResourceManagerInstance();

        // Act
        LocalizableResourceString localizableResourceString1 = new(NameOfResource, resourceManager, typeof(CustomResourceManager));
        LocalizableResourceString localizableResourceString2 = new(NameOfResource, resourceManager, typeof(CustomResourceManager));
        var actualEquals1 = localizableResourceString1.Equals(localizableResourceString2);
        var actualEquals2 = localizableResourceString1.Equals((object?)localizableResourceString2);
        var actualEquals3 = localizableResourceString1 == localizableResourceString2;
        var actualEquals4 = localizableResourceString1 != localizableResourceString2;

        // Assert
        Assert.True(actualEquals1);
        Assert.True(actualEquals2);
        Assert.True(actualEquals3);
        Assert.False(actualEquals4);
    }

    [Fact(DisplayName = "The Equals() method with different resource name must return false")]
    public void EqualsMethod_DifferentResourceName_MustReturnFalse()
    {
        // Arrange
        const string NameOfResource1 = "Resource1";
        const string NameOfResource2 = "Resource2";
        ResourceManager resourceManager = GetTestResourceManagerInstance();

        // Act
        LocalizableResourceString localizableResourceString1 = new(NameOfResource1, resourceManager, typeof(CustomResourceManager));
        LocalizableResourceString localizableResourceString2 = new(NameOfResource2, resourceManager, typeof(CustomResourceManager));
        var actualEquals1 = localizableResourceString1.Equals(localizableResourceString2);
        var actualEquals2 = localizableResourceString1.Equals((object?)localizableResourceString2);
        var actualEquals3 = localizableResourceString1 == localizableResourceString2;
        var actualEquals4 = localizableResourceString1 != localizableResourceString2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = "The Equals() method with different resource manager must return false")]
    public void EqualsMethod_DifferentResourceManager_MustReturnFalse()
    {
        // Arrange
        const string NameOfResource = "Resource1";
        ResourceManager resourceManager1 = GetTestResourceManagerInstance();
        ResourceManager resourceManager2 = GetTestResourceManagerInstance();

        // Act
        LocalizableResourceString localizableResourceString1 = new(NameOfResource, resourceManager1, typeof(CustomResourceManager));
        LocalizableResourceString localizableResourceString2 = new(NameOfResource, resourceManager2, typeof(CustomResourceManager));
        var actualEquals1 = localizableResourceString1.Equals(localizableResourceString2);
        var actualEquals2 = localizableResourceString1.Equals((object?)localizableResourceString2);
        var actualEquals3 = localizableResourceString1 == localizableResourceString2;
        var actualEquals4 = localizableResourceString1 != localizableResourceString2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = "The Equals() method with different resource source must return false")]
    public void EqualsMethod_DifferentResourceSource_MustReturnFalse()
    {
        // Arrange
        const string NameOfResource = "Resource1";
        ResourceManager resourceManager = GetTestResourceManagerInstance();
        var resourceSource1 = typeof(CustomResourceManager);
        var resourceSource2 = typeof(LocalizableResourceStringTests);

        // Act
        LocalizableResourceString localizableResourceString1 = new(NameOfResource, resourceManager, resourceSource1);
        LocalizableResourceString localizableResourceString2 = new(NameOfResource, resourceManager, resourceSource2);
        var actualEquals1 = localizableResourceString1.Equals(localizableResourceString2);
        var actualEquals2 = localizableResourceString1.Equals((object?)localizableResourceString2);
        var actualEquals3 = localizableResourceString1 == localizableResourceString2;
        var actualEquals4 = localizableResourceString1 != localizableResourceString2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = "The Equals() method with different argument must return false")]
    public void EqualsMethod_DifferentArgument_MustReturnFalse()
    {
        // Arrange
        const string NameOfResource = "Resource1";
        ResourceManager resourceManager = GetTestResourceManagerInstance();
        const string ArgumentValue1 = "argument1";
        const string ArgumentValue2 = "argument2";

        // Act
        LocalizableResourceString localizableResourceString1 = new(NameOfResource, resourceManager, typeof(CustomResourceManager), ArgumentValue1);
        LocalizableResourceString localizableResourceString2 = new(NameOfResource, resourceManager, typeof(CustomResourceManager), ArgumentValue2);
        var actualEquals1 = localizableResourceString1.Equals(localizableResourceString2);
        var actualEquals2 = localizableResourceString1.Equals((object?)localizableResourceString2);
        var actualEquals3 = localizableResourceString1 == localizableResourceString2;
        var actualEquals4 = localizableResourceString1 != localizableResourceString2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = "The generic GetHashCode() method must return non-zero value.")]
    public void GetHashCodeMethod_MustNotBeZero()
    {
        // Arrange
        const string NameOfResource = "Resource1";
        ResourceManager resourceManager = GetTestResourceManagerInstance();

        // Act
        LocalizableResourceString localizableResourceString = new(NameOfResource, resourceManager, typeof(CustomResourceManager));
        var actualHashCode = localizableResourceString.GetHashCode();

        // Assert
        Assert.NotEqual(0, actualHashCode);
    }

    private static CustomResourceManager GetTestResourceManagerInstance()
    {
        Dictionary<string, string> defaultCultureResources = new()
        {
            { "Resource1", "My Resource 1 DefaultCulture string" },
            { "Resource2", "My Resource 2 DefaultCulture string" },
            { "Resource3", "My Resource 3 DefaultCulture string" },
            { "ResourceWithArguments", "My Resource DefaultCulture string {0}" },
        };

        Dictionary<string, string> roResources = new()
        {
            { "Resource1", "Romanian string for My Resource 1" },
            { "Resource2", "Romanian string for My Resource 2" },
            { "Resource3", "Romanian string for My Resource 3" },
            { "ResourceWithArguments", "{0} Romanian string for My Resource" },
        };

        var resourceSetMap = new Dictionary<string, Dictionary<string, string>>
        {
            { CustomResourceManager.DefaultCulture.Name, defaultCultureResources },
            { "ro-MD", roResources },
        };

        return new CustomResourceManager(resourceSetMap);
    }

    private class CustomResourceManager : ResourceManager
    {
        private readonly Dictionary<string, CustomResourceSet> _resourceSetMap;

        public CustomResourceManager(Dictionary<string, Dictionary<string, string>> resourceSetMap)
        {
            _resourceSetMap = new Dictionary<string, CustomResourceSet>();

            foreach (var kvp in resourceSetMap)
            {
                var resourceSet = new CustomResourceSet(kvp.Value);
                _resourceSetMap.Add(kvp.Key, resourceSet);
            }
        }

        public static CultureInfo DefaultCulture => CultureInfo.CurrentUICulture;

        public override ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            return _resourceSetMap[culture.Name];
        }

        public override string GetString(string name, CultureInfo culture)
        {
            return this.GetResourceSet(culture, false, false).GetString(name) ?? string.Empty;
        }

        public override string GetString(string name)
        {
            return this.GetString(name, CultureInfo.InvariantCulture);
        }

        public override object GetObject(string name)
        {
            return this.GetString(name);
        }

        public override object GetObject(string name, CultureInfo culture)
        {
            return this.GetString(name, culture);
        }

        private class CustomResourceSet : ResourceSet
        {
            private readonly Dictionary<string, string> _resourcesMap;

            public CustomResourceSet(Dictionary<string, string> resourcesMap)
            {
                _resourcesMap = resourcesMap;
            }

            public override string GetString(string name)
            {
                return _resourcesMap[name];
            }

            public override string GetString(string name, bool ignoreCase)
            {
                throw new NotImplementedException();
            }

            public override object GetObject(string name)
            {
                return this.GetString(name);
            }

            public override object GetObject(string name, bool ignoreCase)
            {
                throw new NotImplementedException();
            }
        }
    }
}
