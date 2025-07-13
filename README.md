# 💷 UK Tax Calculator – Full Stack ASP.NET Core + Angular App

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Angular](https://img.shields.io/badge/Angular-17-red)
![Azure](https://img.shields.io/badge/Deployed-AzureAppService-blue)
![License: MIT](https://img.shields.io/badge/license-MIT-green)

A full-stack UK tax calculator that computes annual and monthly taxes based on configurable tax bands. Built with ASP.NET Core Web API and Angular, deployed as a unified app on Azure App Service.

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

TaxCalculator.App.Api/ → ASP.NET Core Web API
├── Controllers/
├── Program.cs
├── appsettings.json → Defines TaxBands

TaxCalculator.App.Core/ → Domain models (TaxInput, TaxResult, TaxBand)

TaxCalculator.App.Services/ → Business logic (UKTaxCalculatorService)

TaxCalculator.App.Tests/ → MSTest unit/integration tests

AngularClient/ → Angular frontend
└── dist/ → Build output (served by API)


---

## 🧩 Configurable Tax Bands

Located in `appsettings.json`:

```json
"TaxBands": [
  { "LowerLimit": 0, "UpperLimit": 5000, "TaxRate": 0 },
  { "LowerLimit": 5000, "UpperLimit": 20000, "TaxRate": 20 },
  { "LowerLimit": 20000, "UpperLimit": null, "TaxRate": 40 }
]


🚀 Getting Started
Prerequisites
.NET 8 SDK

Node.js + npm

Angular CLI (npm install -g @angular/cli)

git clone https://github.com/<your-username>/uk-tax-calculator.git
cd TaxCalculator.App.Api
dotnet restore

