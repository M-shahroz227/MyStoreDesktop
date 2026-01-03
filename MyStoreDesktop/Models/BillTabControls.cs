using System.Windows.Forms;

namespace MyStoreDesktop.Models
{
    public class BillTabControls
    {
        public TextBox SearchBox { get; set; }
        public Button SearchButton { get; set; }
        public ListBox SuggestionList { get; set; }
        public DataGridView CartGrid { get; set; }
        public BillSessionState Session { get; set; }

        public BillTabControls()
        {
        }
    }
}
