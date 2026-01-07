# Barcode Product Entry Guide

## Overview
This guide explains how to use the Symbol barcode scanner to add products with barcodes from physical product boxes into your inventory system.

---

## Adding a New Product with Barcode

### Step-by-Step Process:

#### 1. Open Product Form
- Navigate to **Product Management** form in the application
- Click the "Product Form" or equivalent menu option

#### 2. Fill in Product Details
Enter the basic product information:
- **Title**: Product name
- **Category**: Select from dropdown (or add new category)
- **Company**: Select from dropdown (or add new company)
- **Quantity**: Stock quantity
- **Sale Price**: Selling price
- **Purchase Price**: Cost price
- **Discount**: Discount amount (if any)
- **Model**: Product model number
- **Description**: Additional product details
- **Image**: Browse and upload product image (optional)

#### 3. Select Code Type
- From the **Code Type** dropdown, select **"Bar Code"**
- This will show the barcode input panel

#### 4. Scan the Barcode
- **Focus on the barcode textbox** (it should automatically have focus when "Bar Code" is selected)
- **Scan the barcode** from the physical product box using your Symbol scanner
- The barcode value will appear in the textbox
- You'll see a **green flash** and hear a **beep** to confirm the scan
- Alternatively, you can manually type the barcode value

#### 5. Add the Product
- Click the **"Add"** button
- The system will:
  - Validate that all required fields are filled
  - Check if the barcode already exists (prevents duplicates)
  - Save the product with the scanned barcode
  - Display a success message

#### 6. Verify the Product
- The new product will appear in the products grid
- The barcode is saved and can be used for:
  - Quick product search in POS (Home form)
  - Quick product lookup in Product Management

---

## Editing a Product's Barcode

### Step-by-Step Process:

#### 1. Select the Product
- In the Product Form, click on a product row in the grid
- Or scan a product's barcode to quickly find it (see "Finding Products by Barcode" below)

#### 2. The Form Will Auto-Fill
- All product details will load, including the existing barcode
- The Code Type dropdown will show "Bar Code"
- The barcode textbox will display the current barcode value

#### 3. Update the Barcode
- **Clear the barcode textbox** if you want to change it
- **Scan the new barcode** from the product box
- Or manually type the new barcode value

#### 4. Update the Product
- Click the **"Update"** button
- The system will:
  - Validate the new barcode
  - Check for duplicates (excluding the current product)
  - Save the updated barcode
  - Display a success message

---

## Finding Products by Barcode

### In Product Form (Management):

#### Option 1: Scan Anywhere
1. Make sure **no textbox has focus** (click on the product grid or elsewhere)
2. **Scan the product barcode** with your Symbol scanner
3. The product will be:
   - **Automatically found** in the products grid
   - **Selected and highlighted** with a green flash
   - **Loaded into the form** for editing

#### Option 2: Manual Search
1. Use the search functionality in the grid
2. Filter products by typing in the search box

### In Home Form (POS/Sales):

1. Click in the **Search Box**
2. **Scan the product barcode**
3. The product will be:
   - **Automatically added to the cart**
   - Displayed with a green flash
   - Ready for sale

---

## Important Features

### Duplicate Barcode Prevention
- The system **prevents duplicate barcodes**
- If you try to add/update a product with an existing barcode, you'll see:
  - Error message: "This barcode already exists for product: [Product Name]"
  - The operation will be cancelled
  - You can choose a different barcode or update the existing product

### Barcode Validation
- Barcode is **required** when "Bar Code" code type is selected
- If you try to save without a barcode, you'll see:
  - Warning message: "Please scan or enter a barcode value!"
  - The barcode textbox will receive focus
  - You must enter a barcode to proceed

### Visual & Audio Feedback
- **Green flash** on textbox when barcode is scanned
- **Beep sound** confirms successful scan
- **Error sound** if product not found (in search mode)

---

## Best Practices

### 1. Scan from Product Box
- Always scan the **manufacturer's barcode** from the physical product packaging
- This ensures consistency with your suppliers and industry standards
- Makes inventory management easier

### 2. Verify Scanned Barcode
- After scanning, **verify the barcode value** appears correctly in the textbox
- Some scanners may add prefix/suffix characters
- If incorrect, clear and re-scan, or manually adjust

### 3. Check for Duplicates
- Before adding a new product, check if it already exists
- You can search by product name or barcode
- Update existing products instead of creating duplicates

### 4. Use Consistent Code Types
- Don't mix code types for the same product
- If a product has a barcode, select "Bar Code" code type
- Keep QR codes for internal use only (if needed)

### 5. Test Scanner Configuration
- Ensure your Symbol scanner is configured correctly
- It should send the barcode followed by Enter key
- Test in Notepad first if unsure

---

## Troubleshooting

### Barcode Not Scanning
**Problem:** Scanner doesn't respond when I scan

**Solutions:**
1. Check USB connection
2. Ensure scanner LED lights up when you pull the trigger
3. Test scanner in Notepad to confirm it's working
4. Make sure the barcode textbox has focus (click on it)

### Barcode Appears in Wrong Field
**Problem:** Barcode value appears in a different textbox

**Solutions:**
1. Make sure the Code Type is set to "Bar Code"
2. Click directly in the barcode textbox before scanning
3. The textbox label should say "Scan Barcode from Product Box"

### Duplicate Barcode Error
**Problem:** "This barcode already exists..." error message

**Solutions:**
1. **Option A:** Update the existing product instead
   - Use search to find the existing product
   - Update its details
2. **Option B:** Use a different barcode
   - Some products have multiple barcodes
   - Scan an alternative barcode from the packaging

### Scanner Adds Extra Characters
**Problem:** Barcode has extra characters at start or end

**Solutions:**
1. Check scanner configuration (see Symbol scanner manual)
2. Disable prefix/suffix in scanner settings
3. Manually remove extra characters before saving

### Can't Find Product After Adding
**Problem:** Product doesn't appear in grid or search

**Solutions:**
1. Refresh the products grid (should happen automatically)
2. Check if product was actually saved (look for success message)
3. Search by product name instead of barcode
4. Verify the Code Type is set to "Bar Code" (CodeType = 2)

---

## Technical Details

### Database Fields
When you add a product with a barcode, these fields are saved:
- **ProductCode**: The scanned barcode value (e.g., "012345678905")
- **CodeType**: Set to `2` (indicates Barcode type)
- **Other fields**: Title, Category, Company, Prices, etc.

### Supported Barcode Types
The Symbol scanner supports multiple barcode formats:
- **CODE_128** (most common)
- **EAN-13** (European Article Number)
- **UPC-A** (Universal Product Code)
- **Code 39**
- **Code 93**
- And many more...

The application accepts any barcode the scanner can read.

### Barcode Length
- **Minimum**: 4 characters (configurable in `BarcodeReaderService`)
- **Maximum**: 50 characters (configurable in `BarcodeReaderService`)
- **Most common**: 8-14 digits

---

## Workflow Example

### Complete Example: Adding a New Product

**Scenario:** You received a shipment of "Coca-Cola 1L bottles" and need to add them to inventory.

**Steps:**

1. **Open Product Form**
   - Click "Product Management" in the menu

2. **Enter Product Details**
   - Title: `Coca-Cola 1L Bottle`
   - Category: `Beverages` (select from dropdown)
   - Company: `Coca-Cola Company` (select from dropdown)
   - Quantity: `24` (2 cases of 12)
   - Sale Price: `2.50`
   - Purchase Price: `1.75`
   - Discount: `0.00`
   - Model: `COKE1L`
   - Description: `Coca-Cola 1 Liter Bottle - Classic`

3. **Select Code Type**
   - Code Type: `Bar Code` (from dropdown)
   - The barcode panel appears

4. **Scan the Barcode**
   - Pick up the bottle
   - Aim the Symbol scanner at the barcode
   - Pull the trigger
   - You hear a beep and see the barcode: `0049000028904`
   - Green flash confirms the scan

5. **Add Product Image** (optional)
   - Click "Browse Image"
   - Select a product photo

6. **Save the Product**
   - Click "Add" button
   - Success message: "Product added successfully with Bar Code!"
   - Form clears and new product appears in grid

7. **Verify**
   - Scroll through the products grid
   - Find "Coca-Cola 1L Bottle"
   - Barcode column (hidden) contains: `0049000028904`

**Done!** The product is now in your inventory and can be scanned at POS.

---

## Integration with POS System

### How Barcodes Work in the Sales Flow:

1. **Customer brings product to checkout**
2. **Cashier scans barcode** with Symbol scanner
3. **System searches** for product by barcode
4. **Product automatically added to cart**
5. **Price, discount, and total calculated**
6. **Customer pays** and transaction completes

This creates a fast, efficient checkout experience!

---

## FAQs

**Q: Can I add products without barcodes?**
A: Yes! Select "QR Code" or don't select a code type. The barcode is only required when "Bar Code" is selected.

**Q: Can I manually type the barcode instead of scanning?**
A: Yes! The textbox accepts manual input. Just type the barcode value and press Enter or click Add.

**Q: What happens if I scan the wrong barcode?**
A: Simply clear the textbox (Ctrl+A, Delete) and re-scan the correct barcode before clicking Add.

**Q: Can one product have multiple barcodes?**
A: Currently, each product can have one ProductCode. If you need multiple barcodes, consider creating product variants.

**Q: Do barcodes work with the receipt printer?**
A: Yes! Barcodes are used for product identification. The product details (including barcode) can be printed on receipts.

**Q: Can I export products with barcodes?**
A: Yes! Use the export functionality to export product data including barcodes to Excel or CSV.

---

## Support

For additional help:
1. Check the **BARCODE_SCANNER_SETUP.md** guide for scanner configuration
2. Test the scanner in Notepad to verify it's working
3. Ensure barcode panel is visible (Code Type = "Bar Code")
4. Contact your system administrator for database issues

---

**Last Updated:** January 2026
**Version:** 1.0
**Compatible Scanners:** Symbol/Zebra USB HID Barcode Scanners
