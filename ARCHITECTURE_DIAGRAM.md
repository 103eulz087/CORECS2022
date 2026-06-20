# Code Architecture - Before vs After

## BEFORE (Problems)

```
InventoryOut.cs (Form)
??? object pcode
??? object desc
??? object barcode          ? Data scattered everywhere
??? object isvat
??? object cost
??? object available
??? object objbrcode
?
??? OutInventory()
    ??? String concatenation SQL
        ??? TotalCost = '0'  ? HARDCODED TO ZERO! ??
            ??? Database.ExecuteQuery()
                ??? SQL Injection Risk ??
                ??? No parameterization
```

**Issues:**
- ?? TotalCost always = 0
- ?? SQL Injection vulnerability
- ?? No type safety
- ?? Data scattered
- ?? Business logic mixed with UI

---

## AFTER (Solution)

```
User Interface
    ?
InventoryOut.cs (Form)
    ??? Branch Selected ? PopulateData
    ??? Product Selected ? AutoCalculate TotalCost
    ??? Add Button ? ValidateAndInsert
        ?
    _currentItem (StockOutItem)
        ??? BatchID: string
        ??? BranchCode: string
        ??? ProductCode: string
        ??? Quantity: decimal
        ??? Cost: decimal
        ??? TotalCost: decimal ? Calculated (Qty × Cost) ?
        ??? IsVat: bool
        ??? IsValid(): bool ? Validation logic
            ?
        StockOutService (Business Logic)
            ??? InsertStockOutItem() ? Parameterized
            ??? DeleteStockOutItem() ? Parameterized
            ??? Error handling & validation
                ?
            Database (SQL Server)
                ??? Secure insertion with parameters
                    ??? TotalCost = correctly calculated ?
```

---

## Data Flow Example

### Scenario: Adding a Stock Out Item

```
USER INTERACTION              DATA STATE                  DATABASE OPERATION
?????????????????            ??????????????              ??????????????????

1. Select Branch
   ?                         _currentItem = {}
   ?? BranchCode change      ?? BranchCode = "BR001"

2. Select Product
   ?                         ?? Cost = 100.00
   ?? ProductCode change        ProductCode = "PROD123"
   ?                           Quantity = 50

3. Enter Quantity: 50
   ?                         _currentItem.CalculateTotalCost()
   ?? Qty change                ?
   ?                           TotalCost = 50 × 100 = 5000 ?

4. Click "Add"
   ?                         _currentItem.IsValid()
   ?? Validation                ?
   ?                           ? All fields OK
   ?
   ?? Service Call             StockOutService.InsertStockOutItem()
   ?                           ?? @batchID = "BATCH001"
   ?                           ?? @branchCode = "BR001"
   ?                           ?? @productCode = "PROD123"
   ?                           ?? @quantity = 50
   ?                           ?? @cost = 100.00
   ?                           ?? @totalCost = 5000.00 ?
   ?                                   ?
   ?                                   INSERT executed
   ?                                   ?
   ?? Success Message      [Database updated correctly]
                           TotalCost = 5000 (NOT 0!) ?
```

---

## Type Safety Comparison

### BEFORE - Unsafe
```csharp
object pcode;           // Could be null, string, int, anything
object cost;            // Could be null, decimal, double, etc.

// Dangerous operations
Convert.ToDouble(cost)  // Will throw if not convertible
double total = Convert.ToDouble(qty) * Convert.ToDouble(cost);  // Could fail
```

### AFTER - Safe
```csharp
private StockOutItem _currentItem;  // Always a StockOutItem

// Safe operations with validation
decimal quantity = _currentItem.Quantity;  // Always decimal, never null
decimal cost = _currentItem.Cost;         // Always decimal, never null

// Safe calculation
_currentItem.CalculateTotalCost();  // Built-in method, no casting needed
```

---

## SQL Security Comparison

### BEFORE - Vulnerable (SQL Injection Risk)
```csharp
// If user enters: " OR '1'='1 " as batch ID
string sql = "INSERT INTO StockOutDetails ... WHERE ID='" + txtbatchid.Text + "'";

// Becomes:
// INSERT INTO StockOutDetails ... WHERE ID='' OR '1'='1 '
// ? This bypasses security! ??
```

### AFTER - Secure (Parameterized Query)
```csharp
// Parameterized queries treat input as DATA, not code
SqlCommand com = new SqlCommand(query, con);
com.Parameters.AddWithValue("@batchID", item.BatchID);

// Whatever user enters is treated as literal data:
// Even if user enters: " OR '1'='1 "
// It's inserted as: @batchID = " OR '1'='1 " (literal string, not SQL code)
// ? Safe and secure
```

---

## Calculation Flow

### THE BIG FIX: TotalCost Calculation

```
BEFORE:
  Quantity Selected: 50
  Cost from Product: 100.00
  Inserted TotalCost: '0'  ? WRONG! ??

AFTER:
  Quantity Selected: 50
  Cost from Product: 100.00
  Calculate: TotalCost = 50 × 100.00 = 5000.00
  Inserted TotalCost: 5000.00 ? CORRECT!

  Method:
  void CalculateTotalCost()
  {
      TotalCost = Quantity * Cost;  ? Simple, automatic, correct
  }
```

---

## Error Handling

### BEFORE
```csharp
try
{
    Database.ExecuteQuery("INSERT ...");
}
catch (SqlException ex)
{
    XtraMessageBox.Show(ex.Message);  // Generic error, hard to debug
}
```

### AFTER
```csharp
string errorMessage;
if (StockOutService.InsertStockOutItem(_currentItem, out errorMessage))
{
    XtraMessageBox.Show("Successfully Added", "Success");
}
else
{
    // Meaningful error:
    // "Failed to add item: Database error: Primary key violation"
    XtraMessageBox.Show("Failed to add item: " + errorMessage, "Error");
}
```

---

## Classes Relationship Diagram

```
???????????????????????????????????????????????????
?           InventoryOut.cs (Form)                ?
?  - Handles user interaction                     ?
?  - Populates _currentItem                       ?
?  - Calls validation & service methods           ?
???????????????????????????????????????????????????
                     ?
                     ? Uses
                     ?
???????????????????????????????????????????????????
?        StockOutItem.cs (Data Model)             ?
?  - BatchID: string                              ?
?  - BranchCode: string                           ?
?  - ProductCode: string                          ?
?  - Quantity: decimal                            ?
?  - Cost: decimal                                ?
?  - TotalCost: decimal ? CALCULATED ?           ?
?  - IsVat: bool                                  ?
?                                                 ?
?  Methods:                                       ?
?  + CalculateTotalCost(): void                   ?
?  + IsValid(): bool                              ?
???????????????????????????????????????????????????
                     ?
                     ? Uses
                     ?
???????????????????????????????????????????????????
?      StockOutService.cs (Business Logic)        ?
?  - Handles all database operations              ?
?  - Uses parameterized queries (SQL injection    ?
?    prevention)                                  ?
?  - Provides error handling                      ?
?                                                 ?
?  Methods:                                       ?
?  + InsertStockOutItem(): bool                   ?
?  + DeleteStockOutItem(): bool                   ?
???????????????????????????????????????????????????
                     ?
                     ? Uses
                     ?
???????????????????????????????????????????????????
?        SQL Server Database                      ?
?  - StockOutDetails table                        ?
?  - All inserts use @parameters (safe)           ?
?  - TotalCost calculated before insert           ?
???????????????????????????????????????????????????
```

---

## Summary: Why This Is Better

| Feature | Before | After |
|---------|--------|-------|
| **TotalCost Value** | Always 0 ? | Correctly Calculated ? |
| **SQL Injection** | Vulnerable ?? | Protected ? |
| **Type Safety** | None ? | Full ? |
| **Code Organization** | Mixed ? | Separated ? |
| **Error Messages** | Generic ? | Detailed ? |
| **Testability** | Hard ? | Easy ? |
| **Maintainability** | Poor ? | Good ? |

