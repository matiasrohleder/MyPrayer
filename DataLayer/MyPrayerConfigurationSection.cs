using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace DataLayer;

public class MyPrayerConfigurationSection : IConfigurationSection
{

    private readonly ConfigurationRoot _root;
    private readonly string _path;
    private string? _key;

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="root">The configuration root.</param>
    /// <param name="path">The path to this section.</param>
    /// <param name="tenantId">Tenant id of the section.</param>
    public MyPrayerConfigurationSection(ConfigurationRoot? root, string path)
    {
        _root = root ?? throw new ArgumentNullException(nameof(root));
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }

    /// <summary>
    /// Gets the full path to this section from the <see cref="IConfigurationRoot"/>.
    /// </summary>
    public string Path => _path;

    /// <summary>
    /// Gets the key this section occupies in its parent.
    /// </summary>
    public string Key
    {
        get
        {
            _key ??= ConfigurationPath.GetSectionKey(_path);
            return _key;
        }
    }

    /// <summary>
    /// Gets or sets the section value.
    /// </summary>
    public string? Value
    {
        get
        {
            return _root[Path];
        }
        set
        {
            _root[Path] = value;
        }
    }

    /// <summary>
    /// Gets or sets the value corresponding to a configuration key.
    /// </summary>
    /// <param name="key">The configuration key.</param>
    /// <returns>The configuration value.</returns>
    public string? this[string key]
    {
        get
        {
            string newKey = $"{key}";
            if (string.IsNullOrWhiteSpace(_root[ConfigurationPath.Combine(Path, newKey)]))
                newKey = key;

            return _root[ConfigurationPath.Combine(Path, newKey)];
        }

        set
        {
            _root[ConfigurationPath.Combine(Path, key)] = value;
        }
    }

    /// <summary>
    /// Gets a configuration sub-section with the specified key.
    /// </summary>
    /// <param name="key">The key of the configuration section.</param>
    /// <returns>The <see cref="IConfigurationSection"/>.</returns>
    /// <remarks>
    ///     This method will never return <c>null</c>. If no matching sub-section is found with the specified key,
    ///     an empty <see cref="IConfigurationSection"/> will be returned.
    /// </remarks>
    public IConfigurationSection GetSection(string key)
    {
        string newKey = $"{key}";
        if (string.IsNullOrWhiteSpace(_root[ConfigurationPath.Combine(Path, newKey)]))
            newKey = key;

        return _root.GetSection(ConfigurationPath.Combine(Path, newKey));
    }

    /// <summary>
    /// Gets the immediate descendant configuration sub-sections.
    /// </summary>
    /// <returns>The configuration sub-sections.</returns>
    public IEnumerable<IConfigurationSection> GetChildren() => _root.GetChildren();

    /// <summary>
    /// Returns a <see cref="IChangeToken"/> that can be used to observe when this configuration is reloaded.
    /// </summary>
    /// <returns></returns>
    public IChangeToken GetReloadToken() => _root.GetReloadToken();
}