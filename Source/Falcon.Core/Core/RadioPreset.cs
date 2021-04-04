using System.Diagnostics.CodeAnalysis;

namespace Falcon.Core
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum RadioBands
    {
        UHF,
        VHF
    }

    public sealed class RadioPreset
    {
        public string Name { get; set; }

        public float Frequency { get; set; } // TODO: Clamp and enforce frequency ranges, allowed values.

        public RadioBands Band { get; set; } = RadioBands.UHF;

        public override string ToString()
        {
            return $"{Name}: {Frequency:G3}";
        }
    }
}