
# HR Leave Management Web Application

## Description

This is a simple web application that enables companies and their employees to manage, track, and submit leave requests. At its core, the application consists of an API interacting with a straightforward Blazor UI, accessible in any modern browser. This application was created to further enhance my skills in leveraging SOLID & Clean architecture and CQRS design patterns.

## Features

- **Leave Request Management**:
  - Users can easily submit leave requests for various leave types.
  - Track leave allocations and manage requests conveniently.
  - Administrators have the ability to accept or deny leave requests.

- **User Roles**:
  - Two distinct roles: Administrators and Employees.
  - Employees can request, review, and cancel leaves.
  - Administrators have the authority to accept or deny leave requests.

- **Email Notifications**:
  - Users receive timely email notifications for leave request updates.
  - Stay informed about the status of your leave requests directly in your inbox.

## Benefits

### ASP.NET Core API

- **Fast and Scalable**:
  - Outperforms other popular web frameworks in TechEmpower benchmarks.
  - Processes more than 7 million requests per second, catering to the needs of large-scale organizations.

- **Secure**:
  - Utilizes ASP.NET Core's Identity libraries for robust authentication.
  - Ensures security with JWT token authentication in headers.
  - Efficiently encodes sensitive information, preventing unauthorized access.

- **Independent and Modular**:
  - The API can be independently consumed by other software and UIs.
  - Supports functions like CRUD tasks in the Leave Management database, error handling, and user authentication.

### Blazor UI

- **Fast and Intuitive**:
  - Renders a clean and efficient UI using Blazor's robust ecosystem.
  - Offers users a fast and intuitive environment accessible in any modern browser.

- **Secure**:
  - Restricts access for non-authenticated and non-authorized users.
  - Implements role-based access control for a secure user experience.

### Easy Database Migration

- **Efficient Data Migration**:
  - Leverages Entity Framework Core for simplified migration of data models.
  - Streamlines the process of migrating to any SQL database without unnecessary setup time.

## Project Structure

The project structure of this application follows the best practices of SOLID & Clean architecture, commonly known as the Onion architecture. The application is organized into various projects, each with its distinct responsibility.

### API

- **API:**
  - The application's API, used for communication with other software.
  - Execution logs are stored in a `.txt` folder for auditing and debugging.

### Core

- **Application:**
  - Interfaces used for interaction with other projects.
  - Data mapping logic, custom exceptions for error handling, and database communication features (e.g., Get Leave Types, Create Leave Request) following a CQRS design pattern.

- **Domain:**
  - Declaration of data objects used within the application, to be migrated to the database.

### Infrastructure

- **Identity:**
  - Configuration for authentication, authorization, role/user management, and JWT token generation.

- **Infrastructure:**
  - Configuration of the in-app logger and email service.

- **Persistence:**
  - Configuration of the initial database migration and repositories for database communication.

### UI

- **BlazorUI:**
  - Rendering of the application's features into a simple and intuitive UI.
  - Acts as an HTTP client, communicating with the API.
  - Front-end logic and page component rendering.

### Test

- **UnitTest:**
  - Project for unit tests using mock repositories to validate application features.

- **IntegrationTests:**
  - Project for integration tests, checking functionalities like database context interactions.

## Tech Stack

This web application is built using a modern tech stack to ensure efficiency, security, and maintainability.

### Backend

- **ASP.NET Core:** The server-side framework used to build the robust API, providing fast and scalable performance.
- **Entity Framework Core:** For efficient data access and database management.
- **Mediatr:** A .NET library that implements the mediator pattern, providing a simple way to decouple the sending of requests (commands or queries) from their handlers.
- **AutoMapper:** A simple library that helps us to transform one object type into another. It is a convention-based object-to-object mapper that requires very little configuration. 
- **C#:** The primary programming language for backend development.

### Frontend

- **Blazor:** The framework used for building the interactive and intuitive user interface.
- **NSwagStudio:** A tool used to automatically generate a service client used to communicate with the backend's API. The tool parses the API's Swagger and generates the client's methods.
- **HTML, CSS, JavaScript:** Standard web technologies utilized for frontend development.

### Authentication and Authorization

- **JWT (JSON Web Tokens):** Used for secure authentication and authorization.
- **ASP.NET Core Identity:** Handles user authentication and authorization processes.

### Database

- **SQL Server:** The relational database management system chosen for its reliability and performance.

### Logging 

- **Serilog:** The logging library used for recording and monitoring application events.

## Testing

- **xUnit:** The testing framework used for unit tests.
- **Shouldly:** A assertion framework which focuses on giving great error messages when the assertion fails while being simple and terse. .

## Other Tools

- **Git:** Version control system for tracking changes and collaboration.
- **Visual Studio 2022:** IDE for code editing and development.
- **Postman:** Used for API testing and development.
- **Swagger:** Used to automatically generate API documentation.

Feel free to explore the detailed documentation for each technology and tool used in this project for a deeper understanding.

