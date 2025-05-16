# Ege University Revolving Fund Management System

## Overview

This project is a comprehensive Revolving Fund Management System developed for Ege University. It is designed to efficiently manage and track all revolving fund operations within the university, providing a streamlined workflow for financial transactions, reporting, and administrative tasks.

## Features

### User Management

- Multi-level user authentication system with different permission levels
- Role-based access control (administrators, department heads, staff members)
- Secure login with personal identification verification

### Financial Operations

- Invoice management and tracking
- Payment processing and distribution
- Expense tracking and categorization
- Department-specific financial reporting
- Automated deduction calculations

### Reporting System

- Comprehensive financial reports
- Department performance analytics
- Custom report generation
- Statistical analysis of revolving fund activities
- Export capabilities for financial data

### Department Management

- Department-specific fund tracking
- Inter-departmental fund transfers
- Department budget management
- Request approval workflows

### Vendor Management

- Vendor registration and tracking
- Invoice management for external vendors
- Payment history and transaction logs

### Bakery Module

- Special module for bakery product orders
- Order tracking and management
- Income reporting for bakery operations

### R&D Project Management

- Research and Development project tracking
- R&D fund allocation and monitoring
- Personnel assignment to R&D projects

## Technical Details

### Architecture

- ASP.NET Core MVC application
- Entity Framework Core for data access
- SQL Server database backend
- Session-based authentication

### Project Structure

- **DonerSermaye/**: Main project folder containing all application code
  - **Controllers/**: MVC controllers handling user requests
  - **Models/**: Data models and business logic
    - **Data/**: Entity models and database context
    - **Helper/**: Helper classes and utilities
    - **Service/**: Business service layer
  - **Views/**: User interface templates
  - **Migrations/**: Database migration files
- **DonerSermaye.sln**: Visual Studio solution file

## Installation

### Prerequisites

- Visual Studio 2019 or newer
- .NET Core 3.1 or newer
- SQL Server 2016 or newer

### Setup Steps

1. **Clone the Repository**

   ```bash
   git clone https://github.com/tugcantopaloglu/tez-dss.git
   cd tez-dss
   ```

2. **Open in Visual Studio**

   - Open the `DonerSermaye.sln` solution file in Visual Studio

3. **Restore NuGet Packages**

   - Right-click on the solution in Solution Explorer
   - Select "Restore NuGet Packages"

4. **Configure Database Connection**

   - Open `appsettings.json` and update the database connection string:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
     }
   }
   ```

5. **Apply Database Migrations**

   - Open Package Manager Console
   - Run: `Update-Database`

6. **Run the Application**
   - Press F5 or click the "Start" button in Visual Studio

## Usage

After installation, the system provides different interfaces based on user roles:

1. **Administrator Interface**

   - Complete system management
   - User administration
   - Global financial reporting

2. **Department Head Interface**

   - Department-specific fund management
   - Approval workflows
   - Department reporting

3. **Staff Interface**

   - Personal transaction history
   - Request submissions
   - Individual reporting

4. **Finance Office Interface**
   - Payment processing
   - University-wide financial reporting
   - Fund distribution management

## Security

This system implements several security measures:

- Password-protected user accounts
- Role-based access control
- Input validation and sanitization
- Protection against SQL injection
- Session security mechanisms

## Contributing

To contribute to this project:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is proprietary software developed for Ege University's revolving fund operations. All rights reserved.

## Contact

- **Developer**: Tuğcan Topaloğlu
- **Email**: [contact@tugcan.dev](mailto:contact@tugcan.dev)
- **GitHub**: [tugcantopaloglu](https://github.com/tugcantopaloglu)
