public class MenuDataService
{
    private readonly List<MainMenuItems> MenuData =
    [
        #region Feather icon
         new MainMenuItems(
            menuTitle: "VENDOR ADMINISTRATOR "
        ),
         new MainMenuItems (
            path: "#",
            type: "link",
            title: "Dashboard",
            icon: "fi fi-ts-airplay",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false
        ),
         new MainMenuItems(
            menuTitle: "PRODUCT MANAGEMENT "
        ),
            new MainMenuItems (
            type: "sub",
            title: "Classification",
            icon: "fi fi-ts-catalog-magazine",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Category",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Sub-Category",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Product Type",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Product Family",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Brand",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
            new MainMenuItems (
            type: "sub",
            title: "Product",
            icon: "fi fi-ts-chart-tree-map",
            svgicon: "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"20\" height=\"20\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"me-2 feather feather-package\"><line x1=\"16.5\" y1=\"9.4\" x2=\"7.5\" y2=\"4.21\"></line><path d=\"M21 16V8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16z\"></path><polyline points=\"3.27 6.96 12 12.01 20.73 6.96\"></polyline><line x1=\"12\" y1=\"22.08\" x2=\"12\" y2=\"12\"></line></svg>",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    icon: "ft ft-grid",
                    title: "Manage Product",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Link-Supplier",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Service",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Archived Product",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
            new MainMenuItems (
            type: "sub",
            title: "Supplier",
            icon: "fi fi-ts-ball-pile",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Manage Supplier",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Archived Supplier",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
            new MainMenuItems (
            type: "sub",
            title: "Warehouse",
            icon: "fi fi-ts-building",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "WarehouseFacility",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),

         new MainMenuItems(
            menuTitle: "PURCHASE & SALES"
        ),
         new MainMenuItems (
            type: "sub",
            title: "Purchase Requisition",
            icon: "fi fi-ts-bags-shopping",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Manage PR",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),

         new MainMenuItems (
            type: "sub",
            title: "Purchase Order",
            icon: "fi fi-ts-balcony",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "View All POs",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Create Purchase",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Draft Purchase",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Pending Approvel",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Acceped POs",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Rejected POs",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Modification Requested",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "InTransit",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Delivered",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Completed",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Approval",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "PO History",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
         new MainMenuItems (
            type: "sub",
            title: "Sales Order",
            icon: "fi fi-ts-ballot",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "View All Sales Order",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Pending Order",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: " Fulfilled Order",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Cancelled Order",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Order History",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
         new MainMenuItems(
            menuTitle: "INVENTORY REPHLENISMENT "
        ),
         new MainMenuItems (
            type: "sub",
            title: "Inventory & Stock",
            icon: "fi fi-ts-inventory-alt",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "View Stock Levels",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Update Stock",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Core Stock",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Multi Warehouse",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Stock Movements",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Stock Reservation",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "ReStock Request",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: " Low Stock Alerts",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Inventory Adjustment",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
            new MainMenuItems (
            type: "sub",
            title: "Invoices & Payments",
            icon: "fi fi-ts-calculator-bill",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Upcoming Invoices",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "View Invoices",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Create Invoice",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Pending Payments",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Paid Invoices",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Payment Reconciliation",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
         new MainMenuItems (
            type: "sub",
            title: "Qulaity Control",
            icon: "fi fi-ts-concierge-bell",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "List of QC",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "QC Pending Items",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "QC Passed Items",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "QC Failed Items",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Return Processing",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
         new MainMenuItems(
            menuTitle: "LOGISTICS TRANSACTION"
        ),
         new MainMenuItems (
            type: "sub",
            title: " Restocking",
            icon: "fi fi-ts-pencil-ruler",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Quality Control",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Financial Cost Analysis",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Logistics Warehouse Allocation",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Product Inventory",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Restocking Request",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Source Vendor",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Create Restock Order",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Pending Restock Approvals",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Restocked Items History",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
         new MainMenuItems (
            type: "sub",
            title: "Logistics & Delivery",
            icon: "fi fi-ts-checklist-task-budget",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "PO Delivery Details",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "List of Order",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Warehouse",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    type: "sub",
                    title: "Advanced Warehouse",
                    selected: false,
                    active: false,
                    dirChange: false,
                    children: new MainMenuItems[]
                    {
                        new MainMenuItems (
                            path: "#",
                            type: "link",
                            title: "Warehouse Detail",
                            selected: false,
                            active: false,
                            dirChange: false
                        ),
                        new MainMenuItems (
                            path: "#",
                            type: "link",
                            title: "Inventory Handling",
                            selected: false,
                            active: false,
                            dirChange: false
                        ),
                        new MainMenuItems (
                            path: "#",
                            type: "link",
                            title: "Distribution Channels",
                            selected: false,
                            active: false,
                            dirChange: false
                        ),
                    }
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Outgoing Shipments",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: " Track Deliveries",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Delivery Receipts",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Shipping Partners",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
         new MainMenuItems (
            type: "sub",
            title: "Credit/Debit Notes",
            icon: "fi fi-ts-arrow-trend-up",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "View Credit Notes",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Create Credit Note",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "View Debit Note",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Create Debit Note",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Pending Approvals",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Processed Notes History",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
         new MainMenuItems(
            menuTitle: "AI INSIGHT "
        ),
         new MainMenuItems (
            type: "sub",
            title: "Audit & Compliance",
            icon: "fi fi-ts-analyse",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: " Sales Logs",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: " Transaction Logs",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "User Activity Logs",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: " Compliance Reports",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
         new MainMenuItems (
            type: "sub",
            title: "Reports & Insights",
            icon: "fi fi-ts-box-ballot",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Vendor Performance",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Financial Risk ",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Compliance & Risk",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Relations & Agreement",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Order Performance",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Financial Reports",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
         ),
         new MainMenuItems(
            menuTitle: "SETTINGS"
        ),
         new MainMenuItems (
            type: "sub",
            title: "User & Role ",
            icon: "fi fi-ts-admin-alt",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "User & Role ",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Role Permissions",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Access Logs",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),

         new MainMenuItems (
            type: "sub",
            title: " Settings",
            icon: "fi fi-ts-flask-gear",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Currency Info",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Brand",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Unit Info",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Profile Settings",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Country",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "State",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "City",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Address",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Notification Preferences",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "API Integrations",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
         new MainMenuItems(
            menuTitle: "REGISTER "
        ),
            new MainMenuItems (
            type: "sub",
            title: "Vendor Register",
            icon: "ft ft-user-check",
            badgeClass: "bg-danger-transparent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "General Information",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Business Location",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Financial Information",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Nature of Business",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Contact Information",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Work Details",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Products & Services",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Achievements",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Client Info",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Testimonial Info",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
                new MainMenuItems (
                    path: "#",
                    type: "link",
                    title: "Document Info",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),

        #endregion
    ];

    public List<MainMenuItems> GetMenuData()
    {
        return MenuData;
    }
}