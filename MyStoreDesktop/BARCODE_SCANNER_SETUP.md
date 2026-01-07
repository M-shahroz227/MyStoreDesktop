# Symbol Barcode Scanner Integration Guide

## Hardware Information

**Barcode Scanner:** Symbol (Zebra Technologies)
**Connection Type:** USB HID (Keyboard Wedge)
**Operating Mode:** Sends keystrokes to focused input control

---

## Features Implemented

### 1. Home Form (Sales/POS)
- **Automatic Product Search:** Scan a barcode to instantly search for products
- **Auto-Add to Cart:** Product is automatically added to the cart when barcode is scanned
- **Visual Feedback:** Green flash on search box when scan is successful
- **Audio Feedback:** Beep sound on successful scan, error sound on failed scan
- **Error Handling:** Shows message if barcode is not found

### 2. Product Form (Product Management)
- **Quick Product Lookup:** Scan a barcode to find and select products in the grid
- **Auto-Selection:** Product is automatically selected and loaded into the form
- **Visual Feedback:** Green flash on selected row when scan is successful
- **Audio Feedback:** Beep sound on successful scan, error sound on failed scan

---

## Scanner Configuration

### Step 1: Connect the Scanner
1. Plug the Symbol barcode scanner into a USB port
2. Windows will automatically install the HID keyboard driver
3. The scanner should be ready to use immediately

### Step 2: Configure Scanner Settings (Recommended)
The scanner should be configured to:
- **Append Enter Key:** YES (sends Enter after each scan)
- **Append Prefix:** None (optional)
- **Append Suffix:** None (optional)
- **Code Type:** Support CODE_128, EAN, UPC, QR codes

To configure your Symbol scanner, scan the configuration barcodes in the Symbol scanner manual or use the Symbol Scanner Configuration Tool.

### Step 3: Test the Scanner
1. Open Notepad
2. Scan a barcode
3. Verify that the barcode value appears followed by a new line (Enter key)

---

## How to Use

### In Home Form (POS/Sales)
1. **Navigate to Home Form** (Sales/Billing screen)
2. **Focus on the Search Box** (or any tab's search box)
3. **Scan a Product Barcode:**
   - The scanner will send the barcode value to the search box
   - The product will be automatically found and added to the cart
   - You'll see a green flash and hear a beep
4. **If Product Not Found:**
   - You'll hear an error sound
   - A message box will show "Product with barcode 'XXX' not found!"

### In Product Form
1. **Open Product Form** (Product Management screen)
2. **Make sure no textbox is focused** (click on the grid or elsewhere)
3. **Scan a Product Barcode:**
   - The scanner will find the product in the grid
   - The product row will be selected and highlighted
   - Product details will be loaded into the form
   - You'll see a green flash on the row and hear a beep
4. **If Product Not Found:**
   - You'll hear an error sound
   - A message box will show "Product with barcode 'XXX' not found!"

---

## Technical Details

### How It Works
The Symbol barcode scanner operates as a **"keyboard wedge"** device:
1. When you scan a barcode, the scanner sends keystrokes to Windows
2. Windows sends these keystrokes to the currently focused control
3. The `BarcodeReaderService` detects the rapid sequence of keystrokes
4. It identifies this as a barcode scan (not manual typing)
5. When Enter key is detected, it triggers the `BarcodeScanned` event
6. The application processes the barcode and performs the appropriate action

### Scanner Detection Logic
- **Minimum Barcode Length:** 4 characters
- **Maximum Barcode Length:** 50 characters
- **Scan Timeout:** 100ms between keystrokes
- If keystrokes arrive faster than 100ms apart, they're considered a scan
- If Enter key is pressed, the scan is complete

### Code Structure
```
Services/BarcodeReaderService/
  └─ BarcodeReaderService.cs    - Core barcode detection logic

Home.cs
  └─ BarcodeReader_BarcodeScanned()  - Handles scans in POS
  └─ txtSearch_KeyPress()             - Processes scanner input
  └─ FlashSuccessFeedback()           - Visual feedback

ProductForm.cs
  └─ BarcodeReader_BarcodeScanned()  - Handles scans in product management
  └─ ProductForm_KeyPress()           - Processes scanner input
  └─ FlashGridSuccessFeedback()       - Visual feedback
```

---

## Supported Barcode Types

The application currently supports:
- **QR Codes** (CodeType = 1)
- **Barcodes** (CodeType = 2) - CODE_128, EAN, UPC, etc.

Products must have:
- `ProductCode` field populated with the barcode value
- `CodeType` field set to 1 (QR) or 2 (Barcode)

---

## Troubleshooting

### Scanner Not Working
1. **Check USB Connection:** Ensure scanner is properly connected
2. **Check Windows Driver:** Open Device Manager → Keyboards → Verify "Symbol Scanner" or "HID Keyboard Device" is listed
3. **Test in Notepad:** Open Notepad and scan - barcode should appear
4. **Check Scanner Configuration:** Scanner must be set to "Keyboard Wedge" mode

### Barcode Not Found
1. **Verify Product Exists:** Check if product is in database with that barcode
2. **Check CodeType:** Ensure product has CodeType = 1 or 2
3. **Check ProductCode:** Ensure ProductCode matches exactly (case-insensitive)
4. **Check Barcode Format:** Scanner might be adding prefix/suffix characters

### Scanner Interfering with Typing
- The scanner should only process when:
  - **Home Form:** txtSearch has focus
  - **Product Form:** No textbox has focus
- If you're typing in a textbox, scanner input will be ignored

### Multiple Scans
- If the scanner is sending the barcode multiple times:
  - Check scanner configuration
  - Disable "Repeat Scan" or "Continuous Scan" mode
  - Set scan mode to "Single Scan"

---

## Scanner Configuration Barcodes

### Enter Key Suffix (REQUIRED)
Scan this barcode to enable "Send Enter after scan":
```
*Enable Enter Key Suffix*
(Scan the configuration barcode from your Symbol scanner manual)
```

### Disable Prefix/Suffix
If your scanner is adding extra characters before/after the barcode:
```
*Disable Prefix*
*Disable Suffix*
(Scan the configuration barcodes from your Symbol scanner manual)
```

---

## Future Enhancements

Potential improvements for future versions:
1. **Batch Scanning:** Scan multiple products in quick succession
2. **Scanner Settings UI:** Configure scanner behavior from within the app
3. **Barcode History:** Track all scanned barcodes
4. **Multi-Scanner Support:** Support multiple scanners simultaneously
5. **Custom Scan Actions:** Configure different actions per barcode type
6. **Scanner LED Control:** Control scanner LED for feedback
7. **Barcode Validation:** Validate barcode format before processing

---

## Support

For issues or questions:
1. Check scanner manufacturer's manual (Symbol/Zebra Technologies)
2. Verify scanner is in "Keyboard Wedge" mode
3. Test scanner in Notepad to confirm functionality
4. Check application logs for error messages

---

## License & Credits

**Application:** MyStoreDesktop
**Barcode Scanner:** Symbol (Zebra Technologies)
**Integration:** Custom BarcodeReaderService
**Date:** January 2026
