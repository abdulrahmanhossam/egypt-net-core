using Egypt.Net.Core.Exceptions;
using System;

namespace Egypt.Net.Core;

/// <summary>
/// Represents an Egyptian National ID and provides
/// functionality to extract personal information
/// such as birth date, age, gender, and governorate.
/// </summary>
public sealed class EgyptianNationalId : IEquatable<EgyptianNationalId>, IComparable<EgyptianNationalId>
{

    private const int CenturyDigitIndex = 0;
    private const int BirthYearStartIndex = 1;
    private const int BirthMonthStartIndex = 3;
    private const int BirthDayStartIndex = 5;
    private const int GovernorateCodeStartIndex = 7;
    private const int SerialStartIndex = 9;
    private const int SerialLength = 4;
    // 13th digit (0-based index 12) determines gender
    private const int GenderDigitIndex = 12;
    private const int FirstIssueAge = 16;
    private const int ValidityPeriodYears = 7;
    private const int ValidityPeriodChangeYear = 2021;
    // Checksum weights for validation (first 13 digits)
    private static readonly int[] ChecksumWeights = { 2, 7, 6, 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };


    /// <summary>
    /// Gets the original 14-digit National ID value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the birth date extracted from the National ID.
    /// </summary>
    public DateTime BirthDate { get; }

    /// <summary>
    /// Gets the birth year extracted from the National ID.
    /// </summary>
    public int BirthYear => BirthDate.Year;

    /// <summary>
    /// Gets the birth month extracted from the National ID.
    /// </summary>
    public int BirthMonth => BirthDate.Month;

    /// <summary>
    /// Gets the birth day extracted from the National ID.
    /// </summary>
    public int BirthDay => BirthDate.Day;

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
    /// Gets the governorate name in Arabic.
    /// </summary>
    public string GovernorateNameAr => Governorate.GetArabicName();

    /// <summary>
    /// Gets the governorate name in English.
    /// </summary>
    public string GovernorateNameEn => Governorate.GetEnglishName();

    /// <summary>
    /// Gets the serial number part of the National ID.
    /// </summary>
    public int SerialNumber { get; }

    /// <summary>
    /// Gets the gender extracted from the National ID.
    /// </summary>
    public Gender Gender { get; }

    /// <summary>
    /// Gets the gender in Arabic.
    /// </summary>
    public string GenderAr => Gender == Gender.Male ? "ذكر" : "أنثى";

    /// <summary>
    /// Gets the estimated date when the National ID was first issued.
    /// Egyptian National IDs are typically issued when a person turns 16.
    /// This is an estimation based on the birth date.
    /// </summary>
    /// <remarks>
    /// The actual issue date may vary slightly depending on when the person
    /// applied for their ID after turning 16.
    /// </remarks>
    public DateTime EstimatedIssueDate => CalculateEstimatedIssueDate();

    /// <summary>
    /// Gets the number of years since the estimated issue date.
    /// This represents approximately how long ago the ID was first issued.
    /// </summary>
    public int YearsSinceIssue => CalculateYearsSinceIssue();

    /// <summary>
    /// Gets the age of the National ID card in years.
    /// This is the time elapsed since the estimated issue date.
    /// </summary>
    public int CardAge => YearsSinceIssue;

    /// <summary>
    /// Gets the estimated expiry date of the National ID.
    /// Current regulation: IDs are valid for 7 years from issue date.
    /// Note: IDs issued before 2021 had a 5-year validity period.
    /// </summary>
    /// <remarks>
    /// This is an estimation. The actual expiry date may differ if:
    /// - The ID was issued before the person turned 16
    /// - The ID was renewed at a different time
    /// - Validity period regulations changed
    /// </remarks>
    public DateTime EstimatedExpiryDate => CalculateEstimatedExpiryDate();

    /// <summary>
    /// Indicates whether the National ID is likely expired based on
    /// the estimated issue date and current validity period (7 years).
    /// </summary>
    /// <remarks>
    /// This is an estimation. A person may have renewed their ID before expiry.
    /// Always verify with official sources for critical applications.
    /// </remarks>
    public bool IsLikelyExpired => DateTime.Today > EstimatedExpiryDate;

    /// <summary>
    /// Gets the number of years until the estimated expiry date.
    /// Returns a negative number if the ID is already expired.
    /// </summary>
    public int YearsUntilExpiry => CalculateYearsUntilExpiry();

    /// <summary>
    /// Indicates whether the ID is estimated to expire within the next year.
    /// Useful for sending renewal reminders.
    /// </summary>
    public bool IsExpiringSoon => YearsUntilExpiry >= 0 && YearsUntilExpiry <= 1;

    /// <summary>
    /// Indicates whether the person is old enough to have received
    /// their first National ID (age 16 or older).
    /// </summary>
    public bool IsEligibleForNationalId => Age >= FirstIssueAge;

    /// <summary>
    /// Initializes a new instance of <see cref="EgyptianNationalId"/>.
    /// </summary>
    /// <param name="value">The 14-digit Egyptian National ID.</param>
    /// <param name="validateChecksum">
    /// Whether to validate the 14th checksum digit.
    /// Default is FALSE because the official algorithm is not publicly documented.
    /// Set to TRUE only if you have the verified algorithm.
    /// </param>
    /// <exception cref="InvalidNationalIdFormatException">
    /// Thrown when the National ID format is invalid.
    /// </exception>
    /// <exception cref="InvalidChecksumException">
    /// Thrown when validateChecksum is true and the checksum is invalid.
    /// </exception>
    public EgyptianNationalId(string value, bool validateChecksum = false)
    {
        if (!IsValidFormat(value))
            throw new InvalidNationalIdFormatException(
                "National ID must be exactly 14 digits long and contain digits only."
            );

        if (validateChecksum && !ValidateChecksum(value))
            throw new InvalidChecksumException();

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
    public static bool IsValidFormat(string value)
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

    /// <summary>
    /// Validates the checksum digit of an Egyptian National ID.
    /// The checksum is calculated using a weighted algorithm on the first 13 digits,
    /// and the 14th digit should match the calculated checksum.
    /// </summary>
    /// <param name="value">The National ID to validate.</param>
    /// <returns>True if the checksum is valid; otherwise, false.</returns>
    public static bool ValidateChecksum(string value)
    {
        if (!IsValidFormat(value))
            return false;

        int sum = 0;
        for (int i = 0; i < 13; i++)
        {
            int digit = value[i] - '0';
            sum += digit * ChecksumWeights[i];
        }

        int calculatedChecksum = sum % 10;
        int providedChecksum = value[13] - '0';

        return calculatedChecksum == providedChecksum;
    }

    /// <summary>
    /// Determines whether the provided value represents
    /// a valid Egyptian National ID (format and domain rules).
    /// </summary>
    /// <param name="value">The National ID value to validate.</param>
    /// <param name="validateChecksum">Whether to validate the checksum digit (default: true).</param>
    /// <returns>
    /// True if the value is a valid Egyptian National ID; otherwise, false.
    /// </returns>
    public static bool IsValid(string value, bool validateChecksum = false)
    {
        return TryCreate(value, out _, validateChecksum);
    }

    /// <summary>
    /// Tries to create a valid <see cref="EgyptianNationalId"/> instance
    /// without throwing exceptions.
    /// </summary>
    /// <param name="value">
    /// The Egyptian National ID value to validate and parse.
    /// </param>
    /// <param name="nationalId">
    /// When this method returns <c>true</c>, contains the created
    /// <see cref="EgyptianNationalId"/> instance; otherwise, <c>null</c>.
    /// </param>
    /// <param name="validateChecksum">Whether to validate the checksum digit (default: true).</param>
    /// <returns>
    /// <c>true</c> if the value represents a valid Egyptian National ID;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method is recommended for safe validation scenarios where
    /// exception handling is not desired.
    /// </remarks>
    public static bool TryCreate(string value, out EgyptianNationalId? nationalId, bool validateChecksum = false)
    {
        nationalId = null;

        if (!IsValidFormat(value))
            return false;

        try
        {
            nationalId = new EgyptianNationalId(value, validateChecksum);
            return true;
        }
        catch (EgyptianNationalIdException)
        {
            return false;
        }
    }

    #region IEquatable Implementation

    /// <summary>
    /// Determines whether the specified <see cref="EgyptianNationalId"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The National ID to compare with the current instance.</param>
    /// <returns>True if the specified National ID is equal to the current instance; otherwise, false.</returns>
    public bool Equals(EgyptianNationalId? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Value == other.Value;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>True if the specified object is equal to the current instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as EgyptianNationalId);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="EgyptianNationalId"/> are equal.
    /// </summary>
    public static bool operator ==(EgyptianNationalId? left, EgyptianNationalId? right)
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="EgyptianNationalId"/> are not equal.
    /// </summary>
    public static bool operator !=(EgyptianNationalId? left, EgyptianNationalId? right)
    {
        return !(left == right);
    }

    #endregion

    #region IComparable Implementation

    /// <summary>
    /// Compares the current instance with another <see cref="EgyptianNationalId"/> based on birth date.
    /// </summary>
    /// <param name="other">The National ID to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared.
    /// Less than zero: This instance precedes other in the sort order (older).
    /// Zero: This instance occurs in the same position in the sort order as other.
    /// Greater than zero: This instance follows other in the sort order (younger).
    /// </returns>
    public int CompareTo(EgyptianNationalId? other)
    {
        if (other is null)
            return 1;

        // Compare by birth date first (older people come first)
        int birthDateComparison = BirthDate.CompareTo(other.BirthDate);
        if (birthDateComparison != 0)
            return birthDateComparison;

        // If birth dates are equal, compare by serial number
        return SerialNumber.CompareTo(other.SerialNumber);
    }

    /// <summary>
    /// Determines whether one National ID is less than another based on birth date.
    /// </summary>
    public static bool operator <(EgyptianNationalId? left, EgyptianNationalId? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Determines whether one National ID is less than or equal to another based on birth date.
    /// </summary>
    public static bool operator <=(EgyptianNationalId? left, EgyptianNationalId? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Determines whether one National ID is greater than another based on birth date.
    /// </summary>
    public static bool operator >(EgyptianNationalId? left, EgyptianNationalId? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Determines whether one National ID is greater than or equal to another based on birth date.
    /// </summary>
    public static bool operator >=(EgyptianNationalId? left, EgyptianNationalId? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }

    #endregion

    /// <summary>
    /// Returns a string representation of the National ID.
    /// </summary>
    /// <returns>The 14-digit National ID value.</returns>
    public override string ToString()
    {
        return Value;
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
        return int.Parse(Value.Substring(GovernorateCodeStartIndex, 2));
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
        return int.Parse(Value.Substring(SerialStartIndex, SerialLength));
    }

    private int GetYear()
    {
        int centuryBase = GetCenturyBase();
        int yearPart = int.Parse(Value.Substring(BirthYearStartIndex, 2));

        return centuryBase + yearPart;
    }

    private int GetMonth()
    {
        return int.Parse(Value.Substring(BirthMonthStartIndex, 2));
    }

    private int GetDay()
    {
        return int.Parse(Value.Substring(BirthDayStartIndex, 2));
    }

    private int GetCenturyBase()
    {
        char centuryDigit = Value[CenturyDigitIndex];

        return centuryDigit switch
        {
            '2' => 1900,
            '3' => 2000,
            _ => throw new InvalidBirthDateException(
                    "Unsupported century digit in National ID."
                )
        };
    }

    /// <summary>
    /// Calculates the estimated issue date of the National ID.
    /// IDs are typically issued when a person turns 16 years old.
    /// </summary>
    private DateTime CalculateEstimatedIssueDate()
    {
        return BirthDate.AddYears(FirstIssueAge);
    }

    /// <summary>
    /// Calculates the number of years since the estimated issue date.
    /// </summary>
    private int CalculateYearsSinceIssue()
    {
        var issueDate = EstimatedIssueDate;
        var today = DateTime.Today;

        int years = today.Year - issueDate.Year;

        if (issueDate.Date > today.AddYears(-years))
            years--;

        if (years < 0)
            return 0;

        return years;
    }

    /// <summary>
    /// Calculates the estimated expiry date of the National ID.
    /// Current regulation: 7 years validity.
    /// Old regulation (before 2021): 5 years validity.
    /// </summary>
    private DateTime CalculateEstimatedExpiryDate()
    {
        var issueDate = EstimatedIssueDate;

        int validityPeriod;

        if (issueDate.Year < ValidityPeriodChangeYear)
        {
            validityPeriod = 5;
        }
        else
        {
            validityPeriod = ValidityPeriodYears;
        }

        return issueDate.AddYears(validityPeriod);
    }

    /// <summary>
    /// Calculates the number of years until the estimated expiry date.
    /// Returns negative if already expired.
    /// </summary>
    private int CalculateYearsUntilExpiry()
    {
        var expiryDate = EstimatedExpiryDate;
        var today = DateTime.Today;

        if (today > expiryDate)
        {
            int yearsSinceExpiry = today.Year - expiryDate.Year;
            if (expiryDate.Date > today.AddYears(-yearsSinceExpiry))
                yearsSinceExpiry--;

            return -yearsSinceExpiry;
        }

        int yearsUntil = expiryDate.Year - today.Year;
        if (today.Date > expiryDate.AddYears(-yearsUntil))
            yearsUntil--;

        return yearsUntil;
    }
}
