A backend API for managing dormitory-related tasks such as student data, events, permissions, and payments. Built with a RESTful design, it handles authentication and CRUD operations for students, admins, and other dorm entities.

Features:
Student/Admin Management: CRUD operations for students and admins.\n
Event & Room Management: Register students for events, and manage room assignments.
Payments & Permissions: Track payments and student leave requests.
Authentication: Login, logout, and password management.
Key Endpoints:
Authentication: /api/Authentication (POST, GET)
Students: /api/student (GET, POST, PUT, DELETE)
Events: /api/event/registerstudenttoevent (POST)
Payments: /api/payment (POST)
