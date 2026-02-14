# Egypt.Net.Core 🇪🇬

Core domain utilities for working with Egyptian national data in .NET.

This library provides clean, immutable, and well-tested domain models for common Egyptian data concepts, starting with the **Egyptian National ID**.

---

## 🎯 Why This Library Exists

Egyptian developers often reimplement National ID validation logic in ad-hoc and error-prone ways. This library provides:
- ✅ Clean, reusable domain model
- ✅ Correct validation and domain boundaries
- ✅ Educational reference for beginners
- ✅ Production-ready code

---

## 📦 Installation

```bash
dotnet add package Egypt.Net.Core
```

**Latest Version:** v1.0.1+ (checksum validation disabled by default)

---

## ⚡ Quick Start

```csharp
using Egypt.Net.Core;

var id = new EgyptianNationalId("30101010123458");

Console.WriteLine(id.BirthDate);           // 2001-01-01
Console.WriteLine(id.Age);                 // 24
Console.WriteLine(id.Gender);              // Male
Console.WriteLine(id.GovernorateNameAr);   // القاهرة
Console.WriteLine(id.IsAdult);             // true
```

---

## ✨ Features

### 🔍 Validation & Parsing
- Parse and validate Egyptian National IDs
- Safe creation with `TryCreate()` (no exceptions)
- Quick validation with `IsValid()`
- Optional checksum validation

### 📊 Data Extraction
- Birth date, age, gender
- Governorate (27 governorates)
- Geographic region classification
- Serial number

### 🌍 Arabic Support
- Full bilingual support (Arabic & English)
- All 27 governorates in Arabic
- Gender in Arabic (ذكر/أنثى)

### 🎨 Formatting
- 5 formatting styles: dashes, spaces, brackets, masked, detailed
- Privacy protection with masked format

### 🔧 Developer Experience
- String extension methods for fluent API
- IEquatable & IComparable support
- LINQ-friendly
- Zero dependencies
- 100+ unit tests

---

## 📚 Documentation

### Basic Usage

```csharp
// Simple creation
var id = new EgyptianNationalId("30101010123458");

// Safe creation (recommended)
if (EgyptianNationalId.TryCreate("30101010123458", out var nationalId))
{
    Console.WriteLine($"{nationalId!.GovernorateNameAr} - {nationalId.Age} سنة");
}

// String extensions
if ("30101010123458".IsValidEgyptianNationalId())
{
    var id = "30101010123458".ToEgyptianNationalId();
}
```

---

### All Available Properties

```csharp
var id = new EgyptianNationalId("30101010123458");

// 📅 Date & Age
id.BirthDate              // 2001-01-01
id.BirthYear              // 2001
id.BirthMonth             // 1
id.BirthDay               // 1
id.Age                    // 24
id.IsAdult                // true (>= 18)

// 📍 Location
id.Governorate            // Cairo (enum)
id.GovernorateCode        // 01
id.GovernorateNameAr      // القاهرة
id.GovernorateNameEn      // Cairo
id.BirthRegion            // GreaterCairo (enum)
id.BirthRegionNameAr      // القاهرة الكبرى
id.IsFromUpperEgypt       // false
id.IsFromLowerEgypt       // true

// 👤 Personal
id.Gender                 // Male (enum)
id.GenderAr               // ذكر
id.SerialNumber           // 2345

// 📄 Raw
id.Value                  // "30101010123458"
```

---

### Formatting Options

```csharp
var id = new EgyptianNationalId("30101010123458");

id.FormatWithDashes()     // 3-010101-01-23458
id.FormatWithSpaces()     // 3 010101 01 23458
id.FormatWithBrackets()   // [3][010101][01][23458]
id.FormatMasked()         // 301********58 (privacy!)
id.FormatDetailed()       // Multi-line format with all info
```

---

### Geographic Regions

Egypt is divided into 7 regions:

```csharp
id.BirthRegion            // GreaterCairo, Delta, UpperEgypt, etc.
id.BirthRegionNameAr      // القاهرة الكبرى
id.IsFromUpperEgypt       // false
id.IsFromLowerEgypt       // true
id.IsFromGreaterCairo     // true
id.IsFromDelta            // false
id.IsFromCoastalRegion    // false
id.IsBornAbroad           // false (Governorate.Foreign)
```

**Regions:**
1. **Greater Cairo** (القاهرة الكبرى) - Cairo, Giza, Qalyubia
2. **Delta** (الدلتا) - Alexandria, Dakahlia, Sharqia, etc.
3. **Canal** (قناة السويس) - Port Said, Suez, Ismailia
4. **Upper Egypt** (الصعيد) - Beni Suef, Asyut, Luxor, etc.
5. **Sinai & Red Sea** (سيناء والبحر الأحمر)
6. **Western Desert** (الصحراء الغربية) - Matrouh, New Valley
7. **Foreign** (خارج الجمهورية)

---

### Checksum Validation

⚠️ **Disabled by default** - The official algorithm is not publicly documented.

```csharp
// Default - no checksum
var id = new EgyptianNationalId("30101010123458");  // ✅

// Enable checksum (if you have the verified algorithm)
var id = new EgyptianNationalId("30101010123458", validateChecksum: true);

// Check only
bool hasValidChecksum = EgyptianNationalId.ValidateChecksum("30101010123458");
```

**What IS validated by default:**
✅ Format (14 digits)  
✅ Birth date validity  
✅ Governorate code (01-88)  
✅ Century digit (2 or 3)  
✅ All structural rules

---

### String Extensions

```csharp
string input = "30101010123458";

// Validation
input.IsValidEgyptianNationalId()     // true/false
input.HasValidNationalIdFormat()      // format only
input.HasValidNationalIdChecksum()    // checksum only

// Parsing
var id = input.ToEgyptianNationalId();
if (input.TryParseAsNationalId(out var nationalId))
{
    // Use nationalId
}
```

---

### Collections & LINQ

```csharp
var id1 = new EgyptianNationalId("30101010123458");
var id2 = new EgyptianNationalId("30101010123458");
var id3 = new EgyptianNationalId("29001010123452");

// Equality
id1 == id2                // true
id1.Equals(id2)           // true

// Comparison (by birth date)
id3 < id1                 // true (1990 < 2001)

// Collections
var unique = new HashSet<EgyptianNationalId> { id1, id2, id3 };
// Count = 2 (id1 and id2 are equal)

// LINQ
var adults = ids.Where(id => id.IsAdult);
var fromCairo = ids.Where(id => id.Governorate == Governorate.Cairo);
var expiredIds = ids.Where(id => id.IsLikelyExpired);
```

---

### Exception Handling

```csharp
try
{
    var id = new EgyptianNationalId("invalid");
}
catch (InvalidNationalIdFormatException)
{
    // Format error: not 14 digits, contains letters, etc.
}
catch (InvalidBirthDateException)
{
    // Invalid date: Feb 30, etc.
}
catch (InvalidGovernorateCodeException)
{
    // Unknown governorate code
}
catch (InvalidChecksumException)
{
    // Checksum validation failed (if enabled)
}
catch (EgyptianNationalIdException)
{
    // Base exception - catches all
}
```

---

## 🎯 Use Cases

### Age Verification System
```csharp
var id = userInput.ToEgyptianNationalId();
if (id == null)
    return "Invalid National ID";

if (!id.IsAdult)
    return $"Must be 18+. Your age: {id.Age}";

return "Access granted";
```

### Regional Analytics
```csharp
var employees = GetEmployees();
var byRegion = employees
    .GroupBy(e => e.NationalId.BirthRegion)
    .OrderByDescending(g => g.Count());

foreach (var group in byRegion)
{
    Console.WriteLine($"{group.Key.GetArabicName()}: {group.Count()} employees");
}
```

### Privacy-Protected Logging
```csharp
// ❌ Bad - privacy violation
logger.Log($"User {id.Value} logged in");

// ✅ Good - masked format
logger.Log($"User {id.FormatMasked()} logged in");
// Output: User 301********58 logged in
```

---

## 📊 Testing

- **100+ Unit Tests** with comprehensive coverage
- All edge cases tested (leap years, boundaries, etc.)
- 100% pass rate
- Production-ready quality

```bash
dotnet test
```

---

## 🗺️ Roadmap

### ✅ Completed
- National ID validation & parsing
- Checksum validation (optional)
- Geographic region classification
- Arabic language support
- Formatting options
- Equality & comparison

### 🔜 Coming Soon
- Generation classification (Gen Z, Millennials, etc.)
- Age group classification
- Zodiac signs
- Advanced validation rules
- ASP.NET Core integration
- JSON serialization

---

## 🤝 Contributing

Contributions are welcome! See [CONTRIBUTING.md](../CONTRIBUTING.md) for guidelines.

---

## 📄 License

MIT License - Made with ❤️ for Egyptian developers

---

## 🔗 Links

- 📦 [NuGet Package](https://www.nuget.org/packages/Egypt.Net.Core/)
- 💻 [GitHub Repository](https://github.com/abdulrahmanhossam/Egypt-Net-Core)
