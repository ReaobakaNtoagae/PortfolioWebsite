# **Municipal Service Management App**

## **Project Overview**

The Municipal Service Management App is a desktop application designed to help citizens interact with local government services efficiently. It provides functionalities for:

* **Service Request Status Tracking** – allowing users to submit, view, and track service requests.
* **Local Announcements & Events** – keeping the community informed about events, activities, and important announcements.
* **Report Issues** – enabling users to report problems with location, category, description, and attachments.

The application is designed for both **end-users** (citizens) and **municipality staff**, ensuring effective communication, faster resolution of issues, and better service prioritization.

---

## **Key Features**

1. **Service Request Status**

   * Users can submit service requests and track their status.
   * Displays all requests in lists, sorted by priority or progress.
   * Interactive visualization of service departments and their connections using a **graph layout**.

2. **Local Announcements & Events**

   * Displays upcoming local events and announcements.
   * Users can filter events by category or date, and view recommended or recently viewed events.

3. **Report Issues**

   * Allows citizens to submit issues with details including location, category, description, and file attachments.
   * Dynamic **progress bar** shows completeness of the report in real-time.
   * Validation ensures all necessary information is provided before submission.

4. **Multilingual Support**

   * The menu, labels, and prompts for the **Report Issue** and other pages support multiple languages.
   * Resource files dynamically update UI text according to the selected language.

---

## **Data Structures Used**

The project leverages advanced data structures to enhance performance and provide efficient functionalities:

1. **Binary Search Tree (BST)** – Stores service requests keyed by Request ID.

   * Enables fast search, insertion, and retrieval by unique request IDs.
   * Example: Quickly locating `SR101` for updating or displaying details.

2. **AVL Tree** – Stores service requests keyed by Description.

   * Self-balancing ensures efficient search for requests by text content.
   * Example: Searching for all requests containing "water" in the description.

3. **Red-Black Tree (RBT)** – Stores service requests keyed by Status.

   * Balanced structure guarantees fast retrieval of requests based on status (Pending, In Progress, Completed).
   * Example: Listing all "High" priority requests in the system.

4. **Heap** – Maintains high-priority service requests.

   * Supports efficient extraction of top-priority requests.
   * Example: Displaying urgent requests in the dashboard first.

5. **Graph** – Represents department connections for visualization.

   * Departments are nodes; edges represent workflow or dependencies.
   * Example: Drawing service department network to highlight affected departments for a request.

6. **Queue & List** – Used for events and recently viewed items.

   * Maintains chronological order and allows retrieval of most recent actions efficiently.
   * Example: Showing recently viewed events and pending upcoming events.

7. **BasicTree** – Represents hierarchical categories for service requests.

   * Example: “City Services” → “Water” → “Pothole Repair.”

**Benefits for Users and Municipality:**

* Users experience **fast search, real-time progress feedback, and intuitive visualization** of departments and services.
* Municipality staff can **prioritize requests, manage workloads, and visualize department interactions** efficiently.

---

## **Compilation and Running Instructions**

### **Requirements**

* .NET 7.0 SDK or newer
* Windows 10/11 for WPF compatibility
* Visual Studio 2022 (recommended)

### **Steps**

1. Clone the repository:

   ```bash
   git clone https://github.com/VCPTA/bca3-prog7312-poe-submission-Reaobaka.git
   ```
2. Open the project in **Visual Studio**.
3. Restore NuGet packages if prompted.
4. Build the solution (Ctrl + Shift + B).
5. Run the app (F5) or launch `ServiceRequestApp.exe` from `bin/Debug/net7.0-windows`.

---

## **Folder Structure**

* `PROG7312PRACTICE/Models` – Data models for requests, events, and issues.
* `PROG7312PRACTICE/DataStructures` – Custom data structure implementations (BST, AVL, RBT, Heap, Graph).
* `PROG7312PRACTICE` – WPF XAML UI pages.
* `PROG7312PRACTICE/Resources` – Multilingual resource files for dynamic localization.

---

## **Multilingual Support**

* Resource files allow the menu, form labels, and prompts to be displayed in multiple languages.
* Example: `Report Issue` prompts and progress bar labels change according to the selected language dynamically.

---

## **References**

* Cormen, T.H., Leiserson, C.E., Rivest, R.L., & Stein, C. (2009) *Introduction to Algorithms*. 3rd ed. MIT Press.
* Goodrich, M.T., Tamassia, R., & Goldwasser, M.H. (2014) *Data Structures and Algorithms in C#*. 1st ed. Wiley.
* Microsoft (2025) *WPF Applications Overview*. [https://learn.microsoft.com/en-us/dotnet/desktop/wpf/](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
* Microsoft (2025) *Localization in WPF*. [https://learn.microsoft.com/en-us/dotnet/desktop/wpf/globalization-localization/](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/globalization-localization/)
