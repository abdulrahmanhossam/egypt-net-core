using Egypt.Net.Core.Exceptions;

namespace Egypt.Net.Core;

/// <summary>
/// Represents an Egyptian National ID and provides
/// functionality to extract personal information
/// such as birth date, age, gender, and governorate.
/// </summary>
public sealed class EgyptianNationalId
{

    private const int CenturyIndex = 0;
    private const int YearIndex = 1;
    private const int MonthIndex = 3;
    private const int DayIndex = 5;
    private const int GovernorateIndex = 7;
    private const int SerialIndex = 9;
    private const int SerialLength = 4;
    private const int GenderDigitIndex = 12;


    /// <summary>
    /// Gets the original 14-digit National ID value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the birth date extracted from the National ID.
    /// </summary>
    public DateTime BirthDate { get; }

    /// <summary>
    /// Gets the calculated age based on the birth date.
    /// </summary>
    public int Age => CalculateAge();

    /// <summary>
    /// Indicates whether the person is 18 years old or older.
    /// </summary>
    public bool IsAdult => Age >= 18;

    /// <summary>
    /// Gets the governorate code extracted from the National ID.
    /// </summary>
    public int GovernorateCode { get; }

    /// <summary>
    /// Gets the governorate extracted from the National ID.
    /// </summary>
    public Governorate Governorate { get; }

    /// <summary>
    /// Gets the serial number part of the National ID.
    /// </summary>
    public int SerialNumber { get; }

    /// <summary>
    /// Gets the gender extracted from the National ID.
    /// </summary>
    public Gender Gender { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="EgyptianNationalId"/>.
    /// </summary>
    /// <param name="value">The 14-digit Egyptian National ID.</param>
    /// <exception cref="InvalidNationalIdFormatException">
    /// Thrown when the National ID format is invalid.
    /// </exception>
    /// <exception cref="InvalidBirthDateException">
    /// Thrown when the extracted birth date is invalid.
    /// </exception>
    /// <exception cref="InvalidGovernorateCodeException">
    /// Thrown when the governorate code is not recognized.
    /// </exception>
    public EgyptianNationalId(string value)
    {
        if (!IsValid(value))
            throw new InvalidNationalIdFormatException(
                "National ID must be exactly 14 digits long and contain digits only."
            );

        Value = value;

        BirthDate = GetBirthDate();
        GovernorateCode = GetGovernorateCode();
        Governorate = GetGovernorate();
        SerialNumber = GetSerialNumber();
        Gender = GetGender();
    }

    /// <summary>
    /// Validates the format of an Egyptian National ID.
    /// </summary>
    /// <param name="value">The National ID to validate.</param>
    /// <returns>True if the format is valid.</returns>
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

        try
        {
            return new DateTime(year, month, day);
        }
        catch
        {
            throw new InvalidBirthDateException(
                "Invalid birth date extracted from National ID."
            );
        }
    }

    private Gender GetGender()
    {
        int genderDigit = Value[GenderDigitIndex] - '0';

        return genderDigit % 2 == 0
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
            throw new InvalidGovernorateCodeException(code.ToString());

        return (Governorate)code;
    }

    private int GetSerialNumber()
    {
        return int.Parse(Value.Substring(SerialIndex, SerialLength));
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
        char centuryDigit = Value[CenturyIndex];

        return centuryDigit switch
        {
            '2' => 1900,
            '3' => 2000,
            _ => throw new InvalidBirthDateException(
                    "Unsupported century digit in National ID."
                )
        };
    }
}
