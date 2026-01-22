namespace Egypt.Net.Core;

public sealed class EgyptianNationalId
{
    // ===== Public API =====

    public string Value { get; }
    public DateTime BirthDate { get; }
    public int GovernorateCode { get; }
    public int SerialNumber { get; }
    public Gender Gender { get; }

    // ===== Constructor =====

    public EgyptianNationalId(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Invalid Egyptian National ID.", nameof(value));

        Value = value;

        BirthDate = GetBirthDate();
        GovernorateCode = GetGovernorateCode();
        SerialNumber = GetSerialNumber();
        Gender = GetGender();
    }

    // ===== Validation =====

    public static bool IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (value.Length != 14)
            return false;

        foreach (char c in value)
        {
            if (!char.IsDigit(c))
                return false;
        }

        return true;
    }

    // ===== Domain Parsing =====

    private DateTime GetBirthDate()
    {
        int year = GetYear();
        int month = GetMonth();
        int day = GetDay();

        return new DateTime(year, month, day);
    }

    private Gender GetGender()
    {
        int serialLastDigit = int.Parse(Value.Substring(11, 1));

        return serialLastDigit % 2 == 0
            ? Gender.Female
            : Gender.Male;
    }

    private int GetGovernorateCode()
    {
        return int.Parse(Value.Substring(7, 2));
    }

    private int GetSerialNumber()
    {
        return int.Parse(Value.Substring(9, 3));
    }

    // ===== Low-level Helpers =====

    private int GetYear()
    {
        int centuryBase = GetCenturyBase();
        int yearPart = int.Parse(Value.Substring(1, 2));

        return centuryBase + yearPart;
    }

    private int GetMonth()
    {
        return int.Parse(Value.Substring(3, 2));
    }

    private int GetDay()
    {
        return int.Parse(Value.Substring(5, 2));
    }

    private int GetCenturyBase()
    {
        char centuryDigit = Value[0];

        if (centuryDigit == '2')
            return 1900;

        if (centuryDigit == '3')
            return 2000;

        throw new InvalidOperationException("Unsupported century digit in national ID.");
    }
}
