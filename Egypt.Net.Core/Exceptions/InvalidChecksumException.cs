namespace Egypt.Net.Core.Exceptions;

/// <summary>
/// Exception thrown when the National ID checksum validation fails.
/// </summary>
public sealed class InvalidChecksumException : EgyptianNationalIdException
{
    public InvalidChecksumException()
        : base("National ID checksum validation failed. The ID may be invalid or corrupted.")
    {
    }
}
