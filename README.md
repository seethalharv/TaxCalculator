# 💷 UK Tax Calculator – Full Stack ASP.NET Core + Angular App

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Angular](https://img.shields.io/badge/Angular-17-red)
![Azure](https://img.shields.io/badge/Deployed-AzureAppService-blue)
![License: MIT](https://img.shields.io/badge/license-MIT-green)

A full-stack UK tax calculator that computes annual and monthly taxes based on configurable tax bands. Built with ASP.NET Core Web API and Angular, deployed as a unified app on Azure App Service.
🔗 Live URL:
👉 UK Tax Calculator Web App

Note: Authentication is not enabled in this deployment to allow open access for reviewers and demo purposes.

---

## 🔧 Features

- Enter gross annual salary and get tax breakdown
- Customizable tax bands (`appsettings.json`)
- Built-in model validation
- Unit and integration tests with MSTest
- Angular UI served directly from ASP.NET Core backend
- Swagger support
- Postman collection with automated test scripts

---

## 🧱 Architecture Overview

```
TaxCalculator.App.Api/         → ASP.NET Core Web API
  ├── Controllers/
  ├── Program.cs
  ├── appsettings.json         → Defines TaxBands

TaxCalculator.App.Core/        → Domain models (TaxInput, TaxResult, TaxBand)

TaxCalculator.App.Services/    → Business logic (UKTaxCalculatorService)

TaxCalculator.App.Tests/       → MSTest unit/integration tests

AngularClient/                 → Angular frontend
  └── dist/                    → Build output (served by API)
```

---

## 🧩 Configurable Tax Bands

Located in `appsettings.json`:

```json
"TaxBands": [
  { "LowerLimit": 0, "UpperLimit": 5000, "TaxRate": 0 },
  { "LowerLimit": 5000, "UpperLimit": 20000, "TaxRate": 20 },
  { "LowerLimit": 20000, "UpperLimit": null, "TaxRate": 40 }
]
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Node.js + npm](https://nodejs.org/)
- Angular CLI (`npm install -g @angular/cli`)

---

### 🔧 Backend Setup

```bash
git clone https://github.com/<your-username>/uk-tax-calculator.git
cd TaxCalculator.App.Api
dotnet restore
```

---

### 🖼️ Frontend Setup (Angular)

```bash
cd AngularClient
npm install
ng build --configuration=production
```

Copy output from `dist/<app-name>/` into:

```bash
../TaxCalculator.App.Api/AngularClient/browser/
```

---

### ▶️ Run Locally

```bash
cd TaxCalculator.App.Api
dotnet run
```

- 🔗 UI: `https://localhost:7072/index.html` (this will take you to the calculator angualr UI single page)
- 🔗 Swagger: `https://localhost:7072/swagger`
- 🔗 API: `POST /api/taxCalculator/calculate`

---

## 🧪 Testing

### ✅ Unit & Integration Tests (MSTest)

```bash
dotnet test TaxCalculator.App.Tests
```

Includes tests for:
- `TaxCalculatorController`
- `UKTaxCalculatorService`

---

### 📬 Postman Collection

Import `UK-Tax-Calculator-API.postman_collection.json` into Postman.

Includes:
- `POST /api/taxCalculator/calculate`
- Test inputs for edge cases
- Test scripts to validate structure and response codes

> ✅ Ready to run with [Newman](https://github.com/postmanlabs/newman) for CI/CD.

---

## ☁️ Deploy to Azure App Service

1. Use Visual Studio **Publish** or CLI:
   ```bash
   dotnet publish -c Release
   ```

2. Make sure `.csproj` includes:

   ```xml
   <ItemGroup>
     <Content Include="AngularClient\browser\**\*">
       <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
     </Content>
   </ItemGroup>
   ```

3. Angular served via:

   ```csharp
   app.UseStaticFiles(new StaticFileOptions {
     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "AngularClient", "browser"))
   });
   app.MapFallbackToFile("index.html");
   ```

---

## 🧠 Design Highlights

- Follows **Clean Architecture** principles
- Uses DI, validation, model binding, and service segregation
- All logic easily testable and reusable
- Suitable for production deployment or Azure DevOps pipelines

---

## 📄 License

MIT License. See [`LICENSE`](./LICENSE) for details.

---

## ✉️ Contact

Built by [sharv](https://github.com/seethalharv)  
Questions? Please email: seethalmd@gmail.com
