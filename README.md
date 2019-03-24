# Sundew.Xaml.Optimizations

## Supported optimizations:
### ResourceDictionaryCachingOptimizer
The ResourceDictionaryCachingOptimizer enables caching for merged ResourceDictionaries and has the following advantages:
1. Merged ResourcesDictionaries are only loaded once.
2. Tooling and designers will not break, because they see the original WPF ResourceDictionary.
3. Less overhead maintaining DesignTimerResources.
