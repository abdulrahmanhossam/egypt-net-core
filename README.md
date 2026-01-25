# Egypt.NET 🇪🇬

A .NET open-source project focused on building clean, well-designed
domain libraries for Egyptian-specific data and real-world use cases.

The first released module is **Egypt.Net.Core**, which provides a
strong domain model for the Egyptian National ID.

---

## 🎯 Project Goals

This project aims to:

- Provide **Egypt-focused .NET libraries**
- Encourage **clean domain modeling**
- Help **beginners learn real open-source practices**
- Avoid ad-hoc, copy-paste implementations
- Grow gradually with real use cases

---

## 📦 Current Modules

### Egypt.Net.Core

Core domain utilities for working with Egyptian national data.

Features include:
- Egyptian National ID parsing and validation
- Birth date extraction
- Gender detection
- Governorate resolution
- Age and adulthood calculation
- Domain-specific exceptions
- Fully unit tested
- No external dependencies

📖 Module documentation:
👉 [`Egypt.Net.Core/README.md`](./Egypt.Net.Core/README.md)

📦 NuGet:
```bash
dotnet add package Egypt.Net.Core
```
---

## 🧠 Philosophy

- Domain first
- Explicit validation
- Fail fast or fail safely
- No magic
- Beginner-friendly but production-aware

---

## 🧪 Testing

Each module includes:
- Dedicated test project
- Clear and readable unit tests
- Realistic test cases

---

## 🤝 Contributing

Contributions are welcome, especially from beginners.

Recommended flow:
- Fork the repository
- Create a feature branch
- Write or update tests
- Submit a pull request with a clear description

---

## 🗺 Roadmap (High-Level)

- Improve National ID validation rules
- Add safe factory methods and result types
- Introduce more Egyptian domain models
- Improve documentation and examples

---

## 📄 License

This project is licensed under the MIT License.
