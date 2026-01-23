namespace Egypt.Net.Core;

public sealed class EgyptianNationalId
{

    private const int CenturyIndex = 0;
    private const int YearIndex = 1;
    private const int MonthIndex = 3;
    private const int DayIndex = 5;
    private const int GovernorateIndex = 7;
    private const int SerialIndex = 9;
    private const int SerialLength = 3;

    public bool IsAdult => Age >= 18;
    public int Age => CalculateAge();
    public Governorate Governorate { get; }
    public string Value { get; }
    public DateTime BirthDate { get; }
    public int GovernorateCode { get; }
    public int SerialNumber { get; }
    public Gender Gender { get; }

    public EgyptianNationalId(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Invalid Egyptian National ID.", nameof(value));

        Value = value;

        BirthDate = GetBirthDate();
        GovernorateCode = GetGovernorateCode();
        Governorate = GetGovernorate();
        SerialNumber = GetSerialNumber();
        Gender = GetGender();
    }

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

    private int CalculateAge()
    {
        var today = DateTime.Today;
        int age = today.Year - BirthDate.Year;

        if (BirthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }

    private DateTime GetBirthDate()
    {
        int year = GetYear();
        int month = GetMonth();
        int day = GetDay();

        return new DateTime(year, month, day);
    }

    private Gender GetGender()
    {
        int serialLastDigit = int.Parse(Value.Substring(SerialIndex + SerialLength - 1, 1));

        return serialLastDigit % 2 == 0
            ? Gender.Female
            : Gender.Male;
    }

    private int GetGovernorateCode()
    {
        return int.Parse(Value.Substring(GovernorateIndex, 2));
    }

    private Governorate GetGovernorate()
    {
        int code = GetGovernorateCode();

        if (!Enum.IsDefined(typeof(Governorate), code))
            throw new InvalidOperationException("Invalid governorate code in national ID.");

        return (Governorate)code;
    }

    private int GetSerialNumber()
    {
        return int.Parse(Value.Substring(SerialIndex, 3));
    }

    private int GetYear()
    {
        int centuryBase = GetCenturyBase();
        int yearPart = int.Parse(Value.Substring(YearIndex, 2));

        return centuryBase + yearPart;
    }

    private int GetMonth()
    {
        return int.Parse(Value.Substring(MonthIndex, 2));
    }

    private int GetDay()
    {
        return int.Parse(Value.Substring(DayIndex, 2));
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
