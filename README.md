
# CRMSystem

CRM system designed to streamline customer relationship management processes, built with .NET and implementing CQRS with MediatR. The project emphasizes observability with integrated logging, metrics, and monitoring using **Prometheus**, **Grafana**, **Elasticsearch**, and **Kibana**.

## 📁 Project Structure

### Core
- **Application Layer:** Implements core business logic, services, and CQRS (Command Query Responsibility Segregation) patterns.
- **Domain Layer:** Contains domain entities and interfaces.
- **Features:** Holds commands and queries, such as:
  - **Import from Excel**
  - **Export to Excel**

### Infrastructure
- **CRM External:** Manages interactions with external systems.
- **CRM Persistence:** Manages database context and repositories for data persistence.

## 🛠️ Key Technologies and Dependencies

- **CQRS with MediatR:** Separates read and write operations, enhancing scalability and maintainability.
- **Exception Handling Middleware:** Custom middleware that logs exceptions for better debugging and analysis.
- **Prometheus and Grafana:** Metrics and visualization tools for monitoring system performance.
- **Elasticsearch and Kibana:** Logs are sent to Elasticsearch and visualized in Kibana for real-time log analysis.

## ⚙️ Setup

1. **Clone the Repository**
   ```bash
   git clone https://github.com/murs000/CRMSystem.git
   cd CRMSystem
   ```

2. **Install Dependencies**
   Make sure you have the necessary .NET dependencies installed.
   ```bash
   dotnet restore
   ```

3. **Run the Application**
   ```bash
   dotnet run
   ```

## 🚀 Features

- **Metrics with Prometheus & Grafana:**  
   The application collects performance metrics and exposes them for Prometheus:
   ```csharp
   app.MapMetrics();
   app.UseHttpMetrics();
   ```
   These metrics can be visualized in Grafana to monitor real-time application health and performance.

- **Logging with Elasticsearch and Kibana:**  
   The custom exception handling middleware sends logs to Elasticsearch, which can be visualized in Kibana. This allows for structured logging and efficient querying of error events, providing a clear view of log data in real-time.

- **Import/Export to Excel:**  
   A feature-rich implementation of import and export functionality for Excel files to simplify data handling.

## 🔧 External Service Integrations

### Prometheus & Grafana
Prometheus is configured to scrape metrics, which are visualized in Grafana. Key metrics exposed include:
- HTTP request counts
- Error rates
- System health indicators

### Elasticsearch & Kibana
Elasticsearch is used to store and analyze logs, which can be visualized in **Kibana**. Logs are sent through the custom exception-handling middleware, providing a centralized logging solution for tracking application errors and events.

## 📂 Folder Structure

```
CRMSystem/
├── Core/
│   ├── Application/
│   ├── Domain/
│   └── Features/
├── Infrastructure/
│   ├── CRMExternal/
│   └── CRMPersistence/
└── Program.cs
```

## 📊 Observability: Metrics and Logging

- **Prometheus** collects system metrics which are visualized in **Grafana** for monitoring.
- **Elasticsearch** stores logs that can be queried and visualized in **Kibana** for in-depth analysis and traceability.

## 🧪 Testing

The project includes a CRC file, making it easy to extend with test cases and collaborate on unit testing.

## 👥 Contributing

We welcome contributions to enhance the CRM system! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.

---

### References

- [Prometheus Documentation](https://prometheus.io/docs/)
- [Grafana Documentation](https://grafana.com/docs/)
- [Elasticsearch Documentation](https://www.elastic.co/guide/en/elasticsearch/reference/current/index.html)
- [Kibana Documentation](https://www.elastic.co/guide/en/kibana/current/index.html)
