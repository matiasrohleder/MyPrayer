namespace Tools.Interfaces.Configuration;

/// <summary>
/// Class that contains the system's general configurations.
/// </summary>
public interface IGeneralConfiguration
{
    string SystemName { get; set; }
    string SystemUrl { get; set; }
}