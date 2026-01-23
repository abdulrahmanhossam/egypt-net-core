# Egypt.Net.Core

Core domain utilities for working with Egyptian national data in .NET.

This library provides clean, immutable, and well-tested domain models
for common Egyptian data concepts, starting with the Egyptian National ID.

---

## Why this project exists

Most examples and libraries in the .NET ecosystem focus on global or Western
data models. Egyptian developers often reimplement the same logic
(such as parsing the national ID) in ad-hoc, error-prone ways.

This project exists to:

- Provide a clean and reusable core library
- Encourage correct domain modeling
- Serve as an educational reference for beginners
- Grow gradually through real Egyptian use cases

---

## Features

- Parse and validate Egyptian National ID
- Extract birth date
- Determine gender
- Resolve governorate
- Calculate age and adulthood
- Clean and immutable domain model
- Domain-specific exceptions
- No external dependencies
- Fully unit tested

---

## Non-Goals

This project does **not** aim to:

- Be a full identity or authentication system
- Handle UI, databases, or APIs
- Replace official government validation
- Solve all Egyptian data problems at once

---

## Installation

> Package not published yet.

Once published, it will be available via NuGet:

```bash
dotnet add package Egypt.Net.Core
```

## Usage

```csharp
using Egypt.Net.Core;

var nationalId = new EgyptianNationalId("30101010123456");

Console.WriteLine(nationalId.BirthDate); // 2001-01-01
Console.WriteLine(nationalId.Gender);    // Male
Console.WriteLine(nationalId.Governorate); // Cairo
Console.WriteLine(nationalId.IsAdult);   // true
```

## Versioning

This project follows semantic versioning:

- `0.x.x` → Public API may change
- `1.0.0` → Stable API


## Project Status

This project is under active development.
New features will be added gradually with a strong focus on correctness and clarity.


## License

This project is licensed under the MIT License.

