# Sundew.Xaml.Optimizations

## Supported optimizations:
### ResourceDictionaryCachingOptimizer
The ResourceDictionaryCachingOptimizer enables caching for merged ResourceDictionaries and has the following advantages:
1. Merged ResourcesDictionaries are only loaded once.
2. Tooling and designers will not break, because they see the original WPF ResourceDictionary.
3. Less overhead maintaining DesignTimerResources.
### FreezeResourceOptimizer
The optimizer changes all WPF Freezable classes such as brushes to frozen unless po:Freeze="False" is set.  
This improves performance by avoiding that WPF has to clone brushes during render.  
Note that brushes that get modified at runtime must set po:Freeze="False, otherwise exceptions will be thrown at runtime.  
For more information about presentation options see: https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/presentationoptions-freeze-attribute

FreezeResourceOptimizer supports the following settings:
- IncludeFrameworkTypes (true/false), default true => Includes all WPF freezables.
- IncludedTypes (List of xaml type names), default null => Additional freezables to be included.
- ExcludedTypes (List of xaml type names), default null => Freezables to be excluded.  
Types in IncludedTypes and ExcludedTypes that does not include a XML namespace will use the WPF presentation namespace.
