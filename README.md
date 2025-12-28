# WorkService

WorkService is a production-ready **ASP.NET Core Web API** deployed to **Azure Container Apps**, backed by **Azure SQL (serverless)**, and fully automated with **Docker + GitHub Actions CI/CD**.

This project demonstrates end-to-end ownership: local development, containerization, cloud deployment, database migrations, secrets management, and automated deployments.

---

## 🚀 Live Deployment

- **Platform:** Azure Container Apps (Consumption)
- **Ingress:** External HTTPS
- **Database:** Azure SQL Database (Serverless)
- **CI/CD:** GitHub Actions (auto deploy on `main` + manual dispatch)

---

## 🧱 Tech Stack

- **Backend:** ASP.NET Core (.NET)
- **ORM:** Entity Framework Core
- **Database:** Azure SQL Database (Serverless)
- **Containerization:** Docker
- **Registry:** Azure Container Registry (ACR)
- **Hosting:** Azure Container Apps
- **CI/CD:** GitHub Actions
- **Secrets:** Azure Container Apps secret references

---

## 📁 Project Structure

WorkService/
├── WorkService.Api/
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── Models/
│   │   └── WorkItem.cs
│   ├── Program.cs
│   └── Dockerfile
├── .github/
│   └── workflows/
│       └── deploy-containerapp.yml
├── WorkService.sln
└── README.md


---

## ⚙️ API Endpoints

### Health Check

GET /health

### Work Items

GET /api/work-items

POST /api/work-items

PATCH /api/work-items/{id}/done


Example POST body:
```json
{
  "title": "My first work item"
}
```
