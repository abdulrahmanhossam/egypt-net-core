# Egypt.Net.Core 🇪🇬

Core domain utilities for working with Egyptian national data in .NET.

This library provides clean, immutable, and well-tested domain models
for common Egyptian data concepts, starting with the **Egyptian National ID**.

---

## Why this library exists

Most .NET examples focus on global or Western data models.
Egyptian developers often reimplement the same logic
(such as parsing the national ID) in ad-hoc and error-prone ways.

This library exists to:
- Provide a clean and reusable core domain model
- Encourage correct validation and domain boundaries
- Serve as an educational reference for beginners
- Grow gradually through real Egyptian use cases

---

## Features

### Core Features
- ✅ Parse and validate Egyptian National ID
- ✅ Extract birth date, age, gender, and governorate
- ✅ Calculate age and adulthood status
- ✅ Clean and immutable domain model
- ✅ Domain-specific exception hierarchy
- ✅ Safe creation via `TryCreate`
- ✅ Quick validation via `IsValid`
- ✅ No external dependencies
- ✅ Fully unit tested (70+ tests)

### New Features 🆕
- ✅ **Checksum Validation**: Validates the 14th digit using weighted algorithm
- ✅ **IEquatable & IComparable**: Full equality and comparison support
- ✅ **Formatting Options**: Multiple formatting styles (dashes, spaces, masked, detailed)
- ✅ **Arabic Support**: Governorate and gender names in Arabic
- ✅ **String Extensions**: Fluent API for validation and parsing
- ✅ **Enhanced Properties**: `GovernorateNameAr`, `GovernorateNameEn`, `GenderAr`

---
## ⚠️ Checksum Validation

### Default Behavior

By default, the library **does NOT validate** the 14th checksum digit.
```csharp
// Default - no checksum validation
var id = new EgyptianNationalId("30101010123458");  // ✅ Accepted
```

### Why is checksum disabled by default?

1. **Not publicly documented** - The official algorithm is not available
2. **Prevents false rejections** - Real National IDs won't be rejected
3. **Security** - Prevents misuse for generating fake IDs
4. **Flexibility** - You can enable it if you have the verified algorithm

### How to enable checksum validation?

If you have the official checksum algorithm:
```csharp
// Enable checksum validation
var id = new EgyptianNationalId("30101010123458", validateChecksum: true);

// Or with IsValid
bool isValid = EgyptianNationalId.IsValid("30101010123458", validateChecksum: true);

// Or with string extensions
bool isValid = "30101010123458".IsValidEgyptianNationalId(validateChecksum: true);
```

### What IS validated by default?

Even without checksum validation, the library validates:

✅ **Format** - Exactly 14 digits
✅ **Birth date** - Valid calendar date
✅ **Governorate** - Valid governorate code (01-88)
✅ **Century** - Valid century digit (2 or 3)
✅ **Domain rules** - All structural rules

This is **sufficient for most production use cases**.

### For developers with the official algorithm

If you have access to the verified checksum algorithm:

1. Update the `ValidateChecksum` method in `EgyptianNationalId.cs`
2. Set `validateChecksum: true` when creating instances
3. Consider contributing it back (with proper authorization)
```csharp
// In your fork/custom build
public static bool ValidateChecksum(string value)
{
    // Your verified algorithm here
    // ...
}

// Then use it
var id = new EgyptianNationalId(userInput, validateChecksum: true);
```

## Installation

Available on NuGet:

```bash
dotnet add package Egypt.Net.Core
```
**Note:** v1.0.1+ has checksum validation disabled by default for better compatibility with real National IDs.

## Basic Usage  (No Checksum Validation)

```csharp
using Egypt.Net.Core;

var nationalId = new EgyptianNationalId("30101011234565");

Console.WriteLine(nationalId.BirthDate);         // 2001-01-01
Console.WriteLine(nationalId.Gender);            // Male
Console.WriteLine(nationalId.Governorate);       // Cairo
Console.WriteLine(nationalId.GovernorateNameAr); // القاهرة
Console.WriteLine(nationalId.GenderAr);          // ذكر
Console.WriteLine(nationalId.IsAdult);           // true
Console.WriteLine(nationalId.Age);               // calculated age
```

## Safe Creation (Recommended)

```csharp
using Egypt.Net.Core;

if (EgyptianNationalId.TryCreate("30101011234565", out var nationalId))
{
    Console.WriteLine($"{nationalId!.GovernorateNameAr} - {nationalId.GenderAr}");
}
else
{
    Console.WriteLine("Invalid National ID");
}
```

## Checksum Validation 🆕

```csharp
// Validate with checksum (default)
var isValid = EgyptianNationalId.IsValid("30101011234565"); // true/false

// Create with checksum validation
var id = new EgyptianNationalId("30101011234565"); // throws if checksum invalid

// Skip checksum validation (for testing or legacy data)
var idNoChecksum = new EgyptianNationalId("30101011234565", validateChecksum: false);

// Validate checksum only
bool hasValidChecksum = EgyptianNationalId.ValidateChecksum("30101011234565");
```

## Formatting Options 🆕

```csharp
var id = new EgyptianNationalId("30101011234565");

// Format with dashes: 3-010101-01-23456
Console.WriteLine(id.FormatWithDashes());

// Format with spaces: 3 010101 01 23456
Console.WriteLine(id.FormatWithSpaces());

// Format with brackets: [3][010101][01][23456]
Console.WriteLine(id.FormatWithBrackets());

// Masked format: 301********65
Console.WriteLine(id.FormatMasked());

// Detailed format with all info
Console.WriteLine(id.FormatDetailed());
/*
Century: 3 (2000s)
Birth Date: 01/01/2001
Governorate: 01 (Cairo)
Serial: 2345
Gender: Male
*/
```

## Arabic Support 🆕

```csharp
var id = new EgyptianNationalId("30101011234565");

// Arabic governorate name
Console.WriteLine(id.GovernorateNameAr); // القاهرة

// English governorate name
Console.WriteLine(id.GovernorateNameEn); // Cairo

// Arabic gender
Console.WriteLine(id.GenderAr); // ذكر or أنثى

// Using extension methods on Governorate enum
var gov = Governorate.Cairo;
Console.WriteLine(gov.GetArabicName());    // القاهرة
Console.WriteLine(gov.GetEnglishName());   // Cairo

var (arabic, english) = gov.GetBothNames();
Console.WriteLine($"{arabic} ({english})"); // القاهرة (Cairo)
```

## String Extension Methods 🆕

```csharp
using Egypt.Net.Core;

string idString = "30101011234565";

// Quick validation
bool isValid = idString.IsValidEgyptianNationalId();

// Convert to NationalId
var nationalId = idString.ToEgyptianNationalId();
if (nationalId != null)
{
    Console.WriteLine(nationalId.GovernorateNameAr);
}

// Try parse pattern
if (idString.TryParseAsNationalId(out var id))
{
    Console.WriteLine($"Age: {id!.Age}");
}

// Format validation only
bool hasValidFormat = idString.HasValidNationalIdFormat();

// Checksum validation only
bool hasValidChecksum = idString.HasValidNationalIdChecksum();
```

## Equality and Comparison 🆕

```csharp
var id1 = new EgyptianNationalId("30101011234565");
var id2 = new EgyptianNationalId("30101011234565");
var id3 = new EgyptianNationalId("29001010123452");

// Equality
Console.WriteLine(id1 == id2);  // true
Console.WriteLine(id1 == id3);  // false

// Comparison (by birth date)
Console.WriteLine(id3 < id1);   // true (1990 < 2001)

// Can be used in collections
var hashSet = new HashSet<EgyptianNationalId> { id1, id2, id3 };
Console.WriteLine(hashSet.Count); // 2 (id1 and id2 are equal)

// Can be sorted
var list = new List<EgyptianNationalId> { id1, id3 };
list.Sort(); // Sorted by birth date (oldest first)
```

## Exception Handling

The library provides a clear exception hierarchy:

```csharp
try
{
    var id = new EgyptianNationalId("invalid");
}
catch (InvalidNationalIdFormatException ex)
{
    // Handle format errors (not 14 digits, contains non-digits)
}
catch (InvalidChecksumException ex)
{
    // Handle checksum validation errors
}
catch (InvalidBirthDateException ex)
{
    // Handle invalid dates (e.g., Feb 30)
}
catch (InvalidGovernorateCodeException ex)
{
    // Handle unrecognized governorate codes
}
catch (EgyptianNationalIdException ex)
{
    // Handle any National ID related error
}
```

## All Available Properties

```csharp
var id = new EgyptianNationalId("30101011234565");

// Basic properties
string value = id.Value;                    // "30101011234565"
DateTime birthDate = id.BirthDate;          // 2001-01-01
int birthYear = id.BirthYear;               // 2001
int birthMonth = id.BirthMonth;             // 1
int birthDay = id.BirthDay;                 // 1

// Age properties
int age = id.Age;                           // calculated
bool isAdult = id.IsAdult;                  // true if >= 18

// Governorate properties
int governorateCode = id.GovernorateCode;   // 1
Governorate governorate = id.Governorate;   // Cairo
string governorateAr = id.GovernorateNameAr;// القاهرة
string governorateEn = id.GovernorateNameEn;// Cairo

// Gender properties
Gender gender = id.Gender;                  // Male/Female
string genderAr = id.GenderAr;             // ذكر/أنثى

// Serial number
int serialNumber = id.SerialNumber;         // 2345
```

## Versioning

This library follows semantic versioning:

- `0.x.x` → Public API may change
- `1.0.0` → Stable API

## Project Status

This project is under active development.
New features will be added gradually with a strong focus on correctness and clarity.

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

See [CONTRIBUTING.md](../CONTRIBUTING.md) for detailed guidelines.

## License

This project is licensed under the MIT License.

---

Made with ❤️ for Egyptian developers
