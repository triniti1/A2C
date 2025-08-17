# 📦 A2C – CRM Microservices Platform

A2C is a CRM project based on Microservices architecture. 
It includes .NET-based backend services, a React frontend interface, and distributed infrastructure components such as PostgreSQL, Kafka, and Object Storage.
---

## 🧱 Project Structure

A2C/ 
├── Client/ # React frontend 
├── Documentation/ # SRS, SDD, diagrams, and more 
├── Shared/ # Common code (DTOs, Utils, etc.) 
├── Tests/ # Unit and integration tests per service 
├── Server/ # Microservices backend 
│ ├── AuthService/ 
│ ├── CustomersService/ 
│ ├── OrdersService/ 
│ ├── LoggingService/ 
│ ├── NotificationsWorker/ 
│ ├── PublisherService/ 
│ └── Gateway/ 
├── A2C.sln # Main solution file 
└── docker-compose.yml # Docker setup for all services

