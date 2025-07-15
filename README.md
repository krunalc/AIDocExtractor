# 📄 AI Document Extractor - Azure Document Intelligence
 
Document Extractor is a web-based tool that extracts data from uploaded invoices, business cards, identity cards, etc. using Azure Document Intelligence (formerly Form Recognizer). It allows users to upload documents and view structured information including fields, values, and confidence scores. This is demo version code only and not production code so while adapting this code necessary changes will be required
 
---
 
## 🚀 Features
 
- Upload invoice , business cards , identity cards and other files (`.pdf`, `.jpg`, `.png`, etc.)
- Preview uploaded files directly in the UI
- Extract data using:
  - Azure SDK
  - Azure Document Intelligence REST API 
  - Training Custom Model
- Display extracted fields with confidence levels
- Responsive, user-friendly interface
 
---
 
## 🛠 Technologies Used
 
- ASP.NET Core MVC
- Razor Views
- Bootstrap 5
- Azure Document Intelligence (Form Recognizer)
- C#
 
---
 
## 🔐 Credentials & Configuration
 
> **Important**: Credentials like API Key and Endpoint are not stored in the source code (`appsettings.json`) for security reasons.
 
### Where to Get the Required Azure Credentials
 
| Name       | Description                                                                 |
|------------|-----------------------------------------------------------------------------|
| `Endpoint` | Found in the Azure Portal under **Your Azure AI Document Intelligence resource → Keys & Endpoint** |
| `Key`      | Found alongside the Endpoint in the Azure Portal. Copy the **Primary Key**. |
| `BaseURL`  | Typically same as the Endpoint. It looks like `https://<your-region>.api.cognitive.microsoft.com/formrecognizer/documentModels` and used as base url to make api calls|
