using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace MyStoreDesktop.Services.BarcodeReaderService
{
    /// <summary>
    /// Service to handle barcode scanner input from Symbol/Zebra USB HID scanners.
    /// Symbol scanners work as keyboard wedge devices, sending keystrokes to focused controls.
    /// </summary>
    public class BarcodeReaderService
    {
        private StringBuilder _barcodeBuffer = new StringBuilder();
        private Stopwatch _scanTimer = new Stopwatch();
        private const int SCAN_TIMEOUT_MS = 100; // Time between keystrokes in a scan

        public event EventHandler<BarcodeScannedEventArgs> BarcodeScanned;

        /// <summary>
        /// Minimum barcode length to be considered valid (prevents accidental short inputs)
        /// </summary>
        public int MinBarcodeLength { get; set; } = 4;

        /// <summary>
        /// Maximum barcode length
        /// </summary>
        public int MaxBarcodeLength { get; set; } = 50;

        /// <summary>
        /// Process KeyPress events from TextBox to detect barcode scanner input
        /// </summary>
        public void ProcessKeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if timer has elapsed (new scan started)
            if (_scanTimer.ElapsedMilliseconds > SCAN_TIMEOUT_MS || !_scanTimer.IsRunning)
            {
                _barcodeBuffer.Clear();
                _scanTimer.Restart();
            }

            // Enter key signals end of barcode scan
            if (e.KeyChar == (char)Keys.Return || e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                _scanTimer.Stop();

                string barcode = _barcodeBuffer.ToString().Trim();

                // Validate barcode length
                if (barcode.Length >= MinBarcodeLength && barcode.Length <= MaxBarcodeLength)
                {
                    OnBarcodeScanned(barcode);
                }

                _barcodeBuffer.Clear();
                e.Handled = true; // Prevent beep sound
                return;
            }

            // Accumulate barcode characters
            if (!char.IsControl(e.KeyChar))
            {
                _barcodeBuffer.Append(e.KeyChar);
                _scanTimer.Restart();
            }
        }

        /// <summary>
        /// Process KeyDown events for better control handling
        /// </summary>
        public void ProcessKeyDown(object sender, KeyEventArgs e)
        {
            // Handle Enter key
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                _scanTimer.Stop();

                string barcode = _barcodeBuffer.ToString().Trim();

                if (barcode.Length >= MinBarcodeLength && barcode.Length <= MaxBarcodeLength)
                {
                    OnBarcodeScanned(barcode);
                }

                _barcodeBuffer.Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Manually trigger a barcode scan (for testing or manual input)
        /// </summary>
        public void TriggerScan(string barcode)
        {
            if (!string.IsNullOrWhiteSpace(barcode))
            {
                OnBarcodeScanned(barcode);
            }
        }

        /// <summary>
        /// Reset the scanner buffer
        /// </summary>
        public void Reset()
        {
            _barcodeBuffer.Clear();
            _scanTimer.Reset();
        }

        protected virtual void OnBarcodeScanned(string barcode)
        {
            BarcodeScanned?.Invoke(this, new BarcodeScannedEventArgs
            {
                Barcode = barcode,
                Timestamp = DateTime.Now
            });
        }
    }

    /// <summary>
    /// Event arguments for barcode scanned event
    /// </summary>
    public class BarcodeScannedEventArgs : EventArgs
    {
        public string Barcode { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
