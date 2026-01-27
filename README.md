# Egypt.NET 🇪🇬

An open-source .NET project focused on building clean, well-designed
domain libraries for **Egyptian-specific data and real-world use cases**.

The project aims to provide production-aware, beginner-friendly
domain models instead of ad-hoc or copy-paste implementations.

---

## 🎯 Project Goals

Egypt.NET exists to:

- Provide **Egypt-focused .NET libraries**
- Encourage **clean domain modeling**
- Help **beginners learn real open-source practices**
- Avoid fragile, duplicated implementations
- Grow gradually through real, well-defined use cases

---

## 📦 Current Modules

### Egypt.Net.Core

Core domain utilities for working with Egyptian national data.

Current features include:
- ✅ Egyptian National ID parsing and validation
- ✅ Checksum validation for data integrity
- ✅ Birth date extraction and age calculation
- ✅ Gender detection (with Arabic support)
- ✅ Governorate resolution (bilingual: Arabic & English)
- ✅ Multiple formatting options (dashes, spaces, masked, detailed)
- ✅ IEquatable & IComparable implementation
- ✅ String extension methods for fluent API
- ✅ Safe creation without exceptions
- ✅ Domain-specific exception hierarchy
- ✅ Fully unit tested (70+ tests)
- ✅ No external dependencies

📖 Module documentation:
👉 [`Egypt.Net.Core/README.md`](./Egypt.Net.Core/README.md)

📦 NuGet:
```bash
dotnet add package Egypt.Net.Core
```

---

## 🚀 Quick Example

```csharp
using Egypt.Net.Core;

var id = new EgyptianNationalId("30101011234565");

Console.WriteLine(id.BirthDate);         // 2001-01-01
Console.WriteLine(id.GovernorateNameAr); // القاهرة
Console.WriteLine(id.GenderAr);          // ذكر
Console.WriteLine(id.Age);               // 24
Console.WriteLine(id.FormatWithDashes()); // 3-010101-01-23456

// String extensions
if ("30101011234565".IsValidEgyptianNationalId())
{
    var nationalId = "30101011234565".ToEgyptianNationalId();
    Console.WriteLine($"{nationalId?.GovernorateNameAr} - {nationalId?.Age} سنة");
}
```

---

## 🧠 Philosophy

- Domain first
- Explicit validation
- Fail fast or fail safely
- No magic
- Beginner-friendly but production-aware
- Bilingual support (Arabic & English)
- Clean, immutable objects

---

## 🧪 Testing

Each module includes:
- Dedicated test project
- Clear and readable unit tests
- Realistic test cases that reflect real usage
- 70+ comprehensive tests

---

## 🤝 Contributing

Contributions are welcome, especially from beginners.

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for detailed guidelines.

Recommended flow:
- Fork the repository
- Create a feature branch
- Write or update tests
- Submit a pull request with a clear description

---

## 🗺 Roadmap

### Completed ✅
- Egyptian National ID validation and parsing
- Checksum validation
- Arabic language support
- Multiple formatting options
- Equality and comparison support
- String extension methods

### Upcoming 🔜
- JSON serialization support
- ASP.NET Core model binding
- FluentValidation integration
- More Egyptian domain models (phone numbers, postal codes, etc.)
- Performance optimizations with Span<T>

---

## 📄 License

This project is licensed under the MIT License.

---

Made with ❤️ for the Egyptian developer community
