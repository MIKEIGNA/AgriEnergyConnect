# AgriEnergyConnect

---

#### **Introduction**

AgriEnergyConnect is a web-based platform designed to streamline agricultural management and foster collaboration between **Farmers** and **Employees**. Farmers can add and manage their products, while Employees can manage farmer information and monitor products.

This README provides all the necessary steps to set up, run, and use the system effectively.

---

### **Table of Contents**
1. [System Requirements](#system-requirements)
2. [Installation Guide](#installation-guide)
3. [User Roles and Permissions](#user-roles-and-permissions)
4. [Features and Navigation](#features-and-navigation)
5. [Using the System](#using-the-system)
6. [Troubleshooting](#troubleshooting)
7. [Contributing](#contributing)

---

### **System Requirements**

- **Operating System**: Windows
- **Development Environment**: Visual Studio 2022 or later
- **Framework**: ASP.NET Core 6.0 or higher
- **Database**: SQLite Server
- **Browser**: Modern browsers like Chrome, Edge, or Firefox

---

### **Installation Guide**

1. **Clone the Repository**
   ```bash
   git clone https://github.com/MIKEIGNA/AgriEnergyConnect.git
   cd AgriEnergyConnect
   ```

2. **Open the Project**
   - Open the solution file (`AgriEnergyConnect.sln`) in Visual Studio.

3. **Run the Application**
   - Press `F5` or use the **Run** button in Visual Studio.
   - This will open a link on your default browser like so: https://localhost:7095/

4. **Seed Data (Optional)**
   - If no users exist, manually add Farmers and Employees via the Admin section or through database scripts.

---

### **User Roles and Permissions**

1. **Farmers**
   - Can log in to add, view, and manage their products.
   - Limited to actions related to their own data.

2. **Employees**
   - Can manage farmer details (add, view, search farmers).
   - Can view and search for all products across the platform.

Each role has tailored navigation to ensure ease of use and data privacy.

---

### **Features and Navigation**

#### **Farmers**
- **Home**: View dashboard and basic instructions.
- **Add Product**: Add new products with details like name, category, and production date.
- **View Products**: List of all their products with search and filtering options.

#### **Employees**
- **Home**: Overview of tasks and platform statistics.
- **Add Farmer**: Add new farmers with contact and address details.
- **View Farmers**: List of all farmers with search functionality.
- **View Products**: View all products across the system, with filtering options (by name, farmer, category, and date range).

#### **Navigation**
The navigation bar is dynamic and changes based on the logged-in user's role.

---

### **Using the System**

#### **Farmer Workflow**
1. Log in with your farmer credentials.
2. Default credentials Farmer User:
         - Email: farmer@example.com
         - Password: Password123!
3. Navigate to **Add Product** to add your products.
4. Use the **View Products** page to review and search for your added products.

#### **Employee Workflow**
1. Log in with your employee credentials.
2. Default credentials Employee User:
         - Email: employee@example.com
         - Password: Password123!
3. Use **Add Farmer** to register new farmers.
4. Visit **View Farmers** to search or manage existing farmer data.
5. Access **View Products** to browse or search for products from all farmers.

---

### **Troubleshooting**

1. **Database Connection Issues**
   - Ensure the connection string in `appsettings.json` matches your SQL Server instance.
     
2. **Unable to Log In**
   - Ensure user roles (`Farmer` or `Employee`) are correctly assigned.
   - Check the user table in the database for valid credentials.

3. **404 or Navigation Issues**
   - Verify URLs in the navigation bar and ensure controllers and actions are correctly defined.

---

### **Contributing**

1. Fork the repository.
2. Create a new branch (`feature/my-feature`).
3. Commit changes and push to your branch.
4. Open a pull request to the main branch.

---
